using System;
using System.Collections.Generic;
using System.Linq;
using Codice.Client.Commands;
using Cysharp.Threading.Tasks;
using GameMaker.Core.Runtime;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Purchasing;

namespace GameMaker.IAP.Runtime
{
    public enum IAPProcessResult
    {
        Success,
        Fail
    }
    public class IAPPurchaseExtentData : IExtendData
    {
        private string _name;
        public IAPPurchaseExtentData(string name)
        {
            _name = name;
        }
        public string GetName()
        {
            return _name;
        }
    }
    public class IAPRuntimeManager : AutomaticMonoSingleton<IAPRuntimeManager>
    {
        private StoreController _storeController;
        private List<Product> _products;
        private bool _isInit = false;
        private UniTaskCompletionSource<IAPProcessResult> _currentPurchaseTcs;
        public bool IsInit => _isInit;

        public async UniTask<bool> InitAsync()
        {
            if (_isInit) return true;

            _storeController = UnityIAPServices.StoreController();
            _storeController.OnPurchasePending += OnPurchasePending;
            _storeController.OnPurchaseFailed += OnPurchaseFailed;
            bool status = await ConnectAsync();
            if (status == false) return false;

            var productDefinitions = new List<ProductDefinition>();
            foreach (var iapDefinition in IAPManager.Instance.GetDefinitions())
            {
                productDefinitions.Add(new ProductDefinition(iapDefinition.ProductID, iapDefinition.ProductType));
            }

            var (fetchProductStatus, products) = await FetchProductsAsync(productDefinitions);
            if (fetchProductStatus == false) return false;
            _products = products;

            var (fetchPurchaseStatus, orders) = await FetchPurchasesAsync();
            if (fetchPurchaseStatus == false) return false;
            await DetectRefundAsync(orders);

            _isInit = true;
            return true;
        }

        private UniTask<bool> ConnectAsync()
        {
            var tcs = new UniTaskCompletionSource<bool>();

            void OnDisconnected(StoreConnectionFailureDescription failure)
            {
                _storeController.OnStoreDisconnected -= OnDisconnected;
                tcs.TrySetResult(false);
            }

            async void StartConnect()
            {
                try
                {
                    await _storeController.Connect();
                    _storeController.OnStoreDisconnected -= OnDisconnected;
                    tcs.TrySetResult(true);
                }
                catch (Exception)
                {
                    _storeController.OnStoreDisconnected -= OnDisconnected;
                    tcs.TrySetResult(false);
                }
            }

            _storeController.OnStoreDisconnected += OnDisconnected;

            StartConnect();

            return tcs.Task;
        }

        private UniTask<(bool status, List<Product> products)> FetchProductsAsync(List<ProductDefinition> defs)
        {
            var tcs = new UniTaskCompletionSource<(bool, List<Product>)>();

            void Success(List<Product> products)
            {
                Cleanup();
                tcs.TrySetResult((true, products));
            }

            void Fail(ProductFetchFailed failure)
            {
                Cleanup();
                tcs.TrySetResult((false, null));
            }

            void Cleanup()
            {
                _storeController.OnProductsFetched -= Success;
                _storeController.OnProductsFetchFailed -= Fail;
            }

            _storeController.OnProductsFetched += Success;
            _storeController.OnProductsFetchFailed += Fail;

            _storeController.FetchProducts(defs);

            return tcs.Task;
        }

        private UniTask<(bool status, Orders orders)> FetchPurchasesAsync()
        {
            var tcs = new UniTaskCompletionSource<(bool, Orders)>();

            void Success(Orders orders)
            {
                Cleanup();
                tcs.TrySetResult((true, orders));
            }

            void Fail(PurchasesFetchFailureDescription failure)
            {
                Cleanup();
                tcs.TrySetResult((false, null));
            }

            void Cleanup()
            {
                _storeController.OnPurchasesFetched -= Success;
                _storeController.OnPurchasesFetchFailed -= Fail;
            }

            _storeController.OnPurchasesFetched += Success;
            _storeController.OnPurchasesFetchFailed += Fail;

            _storeController.FetchPurchases();

            return tcs.Task;
        }

        private async UniTask<bool> DetectRefundAsync(Orders orders)
        {
            var confirmedIds = orders.ConfirmedOrders
                .Select(o => o.CartOrdered.Items().FirstOrDefault()?.Product?.definition.id)
                .Where(id => id != null)
                .ToHashSet();

            List<string> refundTransactionIds = new();

            foreach (var record in IAPGateway.Manager.GetPlayerIAPs())
            {
                if (record.IsActive && !confirmedIds.Contains(record.ProductId))
                {
                    refundTransactionIds.Add(record.TransactionId);
                }
            }
            bool status = await IAPGateway.Manager.RecallPlayerIAPsAsync(refundTransactionIds);
            return status;
        }

        private void OnPurchasePending(PendingOrder order)
        {
            OnPurchasePendingAsync(order).Forget();
        }

        private async UniTask OnPurchasePendingAsync(PendingOrder order)
        {
            var product = GetFirstProductInOrder(order);
            var definition = IAPManager.Instance.GetIAPDefinitionByProductId(product.definition.id);
            var playerIAP = new PlayerIAP(definition.GetID(),
                definition,
                product.definition.id,
                order.Info.TransactionID,
                order.Info.Receipt,
                true,
                new());
            var (status, receiverProducts) = await IAPGateway.Manager.PurchaseAsync(playerIAP);
            if (status)
            {
                _storeController.ConfirmPurchase(order);
                _currentPurchaseTcs?.TrySetResult(IAPProcessResult.Success);
            }
            else
            {
                _currentPurchaseTcs?.TrySetResult(IAPProcessResult.Fail);
            }
            _currentPurchaseTcs = null;
        }

        Product GetFirstProductInOrder(Order order)
        {
            return order.CartOrdered.Items().First()?.Product;
        }

        private void OnPurchaseFailed(FailedOrder order)
        {
            _currentPurchaseTcs?.TrySetResult(IAPProcessResult.Fail);
            _currentPurchaseTcs = null;
        }

        public UniTask<IAPProcessResult> PurchaseAsync(string definitionId)
        {
            if (_currentPurchaseTcs != null)
                throw new Exception("Purchase already in progress");

            _currentPurchaseTcs = new UniTaskCompletionSource<IAPProcessResult>();
            var productId = IAPManager.Instance.GetDefinition(definitionId).ProductID;
            _storeController.PurchaseProduct(productId);
            return _currentPurchaseTcs.Task;
        }

        public async UniTask<bool> RestorePurchases()
        {
            bool status = false;
            bool restoreStatus = false;
            _storeController.RestoreTransactions((bool s, string error) =>
            {
                restoreStatus = s;
                status = true;
                GameMaker.Core.Runtime.Logger.Log($"[IAPRuntimeManager] Restore completed with status: {s}, error: {error}");
            });
            await UniTask.WaitUntil(() => !status);
            return restoreStatus;
        }

        public Product GetProductByDefinitionID(string definitionId)
        {
            var productId = IAPManager.Instance.GetDefinition(definitionId).ProductID;
            return _products.FirstOrDefault(p => p.definition.id == productId);
        }
    }
}