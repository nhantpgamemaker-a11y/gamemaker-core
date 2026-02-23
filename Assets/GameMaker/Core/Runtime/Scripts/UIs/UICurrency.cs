using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameMaker.Core.Runtime
{
    public class UICurrency : BaseLifeCycle,IObserver<BasePlayerData>
    {
        [Header("Config")]
        [UnityEngine.SerializeField] protected CurrencyID currencyID;
        [Header("Ref")]
        [UnityEngine.SerializeField] protected TMP_Text txtAmount;
        [UnityEngine.SerializeField] protected Image imgIcon;
        
        protected BasePlayerCurrency playerCurrency;
        protected virtual void OnValidate() 
        {
            this.gameObject.name = $"UICurrency-{currencyID.GetBaseCurrencyDefinition().GetName()}";
        }
        public override void Init()
        {
            playerCurrency = currencyID.GetPlayerCurrency();
            playerCurrency.AddObserver(this);
            InitUI(currencyID.GetPlayerCurrency());
        }
        public override void Clear()
        {
            playerCurrency.RemoveObserver(this);
            playerCurrency = null;
        }
        protected virtual void InitUI(BasePlayerCurrency playerCurrency)
        {
            imgIcon.sprite = (currencyID.GetPlayerCurrency().GetDefinition() as BaseCurrencyDefinition).GetIcon();
            
            UpdateUI(playerCurrency);
        }
        protected virtual void UpdateUI(BasePlayerCurrency playerCurrency)
        {
            txtAmount.text = playerCurrency.Value.ToString();
        }

        #region IObserver<BasePlayerData>
        public virtual void OnNotify(ISubject<BasePlayerData> subject, BasePlayerData data)
        {
            UpdateUI(data as BasePlayerCurrency);
        }
        #endregion
    }
}
