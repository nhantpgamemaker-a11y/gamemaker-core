using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using GameMaker.Core.Runtime;
using Newtonsoft.Json;

namespace GameMaker.IAP.Runtime
{
    [System.Serializable]
    public class LocalIAPSaveData : BaseLocalData
    {
        [JsonProperty("PlayerIAPs")]
        private List<PlayerIAPModel> _playerIAPModels;
        protected override void OnCreate()
        {
            base.OnCreate();
            _playerIAPModels = new List<PlayerIAPModel>();
        }
        public async UniTask AddPlayerIAPAsync(PlayerIAPModel playerIAPModel, bool isSave = true)
        {
            _playerIAPModels.Add(playerIAPModel);
            if (isSave)
                await SaveAsync();
        }
        
        protected override void OnLoad()
        {
            base.OnLoad();
            foreach (var playerIAPModel in _playerIAPModels)
            {
                playerIAPModel.SetIAPDefinition(IAPManager.Instance.GetDefinition(playerIAPModel.ProductId) as IAPDefinition);
            }
        }
        public List<PlayerIAP> GetPlayerIAPs()
        {
            return  _playerIAPModels.Where(x=>x.GetDefinition() != null).Select(x => x.ToPlayerIAP()).ToList();
        }
        public PlayerIAP GetPlayerIAPByTransactionId(string transactionId)
        {
            return _playerIAPModels.Find(x => x.TransactionId == transactionId)?.ToPlayerIAP();
        }

        public async UniTask UpdatePlayerIAPActiveStatusAsync(string transactionIds,bool status, bool isSave = true)
        {
            var playerIAPModel = _playerIAPModels.FirstOrDefault(x => x.TransactionId == transactionIds);
            if (playerIAPModel != null)
            {
                playerIAPModel.SetStatus(status);
            }
            if (isSave)
                await SaveAsync();
        }
    }
    [System.Serializable]
    public class PlayerIAPModel : PlayerDataModel
    {
        [JsonProperty("ProductId")]
        private string _productId;
        [JsonProperty("TransactionId")]
        private string _transactionId;
        [JsonProperty("Receipt")]
        private string _receipt;
        [JsonProperty("IsActive")]
        private bool _isActive;
        [JsonProperty("ReceiverProducts")]

        private List<BaseReceiverProduct> _receiverProducts;
        [JsonIgnore]
        public string ProductId { get => _productId; }
        [JsonIgnore]
        public string TransactionId { get => _transactionId; }
        [JsonIgnore]
        public string Receipt { get => _receipt; }
        [JsonIgnore]
        public bool IsActive { get => _isActive; }
        [JsonIgnore]
        private IAPDefinition _definition;
        [JsonIgnore]
        public List<BaseReceiverProduct> ReceiverProducts { get => _receiverProducts; }
        public void SetIAPDefinition(IAPDefinition definition)
        {
            _definition = definition;
        }
        public PlayerIAPModel(string id, string name, string productId, string transactionId, string receipt, bool isActive, List<BaseReceiverProduct> receiverProducts) : base(id, name)
        {
            _productId = productId;
            _transactionId = transactionId;
            _receipt = receipt;
            _isActive = isActive;
            _receiverProducts = receiverProducts;
        }

        public PlayerIAP ToPlayerIAP()
        {
            return new PlayerIAP(GetID(), _definition, _productId, _transactionId, _receipt, _isActive, _receiverProducts);
        }

        public IAPDefinition GetDefinition()
        {
            return _definition;
        }

        public void SetStatus(bool status)
        {
            _isActive = status;
        }
    }
}