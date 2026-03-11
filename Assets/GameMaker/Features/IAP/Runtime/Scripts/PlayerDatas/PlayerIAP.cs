using System.Collections.Generic;
using System.Linq;
using GameMaker.Core.Runtime;

namespace GameMaker.IAP.Runtime
{
    [System.Serializable]
    public class PlayerIAP : BasePlayerData
    {
        [UnityEngine.SerializeField]
        private string _productId;
        [UnityEngine.SerializeField]
        private string _transactionId;
        [UnityEngine.SerializeField]
        private string _receipt;
        [UnityEngine.SerializeField]
        private bool _isActive;
        [UnityEngine.SerializeReference]
        private List<BaseReceiverProduct> _receiverProducts;
        public string ProductId { get => _productId; }
        public string TransactionId { get => _transactionId; }
        public string Receipt { get => _receipt; }
        public bool IsActive { get => _isActive; }
        public List<BaseReceiverProduct> ReceiverProducts { get => _receiverProducts;}
        public PlayerIAP(string id,
        IDefinition definition,
        string productId,
        string transactionId,
        string receipt,
        bool isActive,
        List<BaseReceiverProduct> receiverProducts)
        : base(id, definition)
        {
            _productId = productId;
            _transactionId = transactionId;
            _receipt = receipt;
            _isActive = isActive;
            _receiverProducts = receiverProducts;
        }
        public void SetActive(bool status)
        {
            _isActive = status;
            NotifyObserver(this);
        }
        public void SetProducts(List<BaseReceiverProduct> baseReceiverProducts)
        {
            _receiverProducts = baseReceiverProducts;
            NotifyObserver(this);
        }

        public override object Clone()
        {
            return new PlayerIAP(GetID(), GetDefinition(), _productId, _transactionId, _receipt, _isActive, _receiverProducts.Select(x=>x.Clone() as BaseReceiverProduct).ToList());
        }

        public override void CopyFrom(BasePlayerData basePlayerData)
        {
            base.CopyFrom(basePlayerData);
        }
    }
}