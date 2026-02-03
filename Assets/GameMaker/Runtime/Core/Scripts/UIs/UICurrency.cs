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
        

        public override void Init()
        {
            currencyID.GetPlayerCurrency().AddObserver(this);
            InitUI(currencyID.GetPlayerCurrency());
        }
        public override void Clear()
        {
            currencyID.GetPlayerCurrency().RemoveObserver(this);
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
        public void OnNotify(ISubject<BasePlayerData> subject, BasePlayerData data)
        {
            UpdateUI(data as BasePlayerCurrency);
        }
        #endregion
    }
}
