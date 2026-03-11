using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameMaker.Core.Runtime
{
    public class UIProperty : BaseLifeCycle,IObserver<BasePlayerData>
    {
        [Header("Config")]
        [UnityEngine.SerializeField] protected PropertyID propertyID;
        [Header("Ref")]
        [UnityEngine.SerializeField] protected TMP_Text txtAmount;
        [UnityEngine.SerializeField] protected Image imgIcon;
        

        public override void Init()
        {
            propertyID.GetPlayerProperty().AddObserver(this);
            InitUI(propertyID.GetPlayerProperty());
        }
        public override void Clear()
        {
            propertyID.GetPlayerProperty().RemoveObserver(this);
        }

        protected virtual void InitUI(PlayerProperty playerProperty)
        {
            imgIcon.sprite = (propertyID.GetPlayerProperty().GetDefinition() as BaseCurrencyDefinition).GetIcon();
            
            UpdateUI(playerProperty);
        }
        protected virtual void UpdateUI(PlayerProperty playerProperty)
        {
            txtAmount.text = playerProperty.GetStringValue();
        }

        #region IObserver<BasePlayerData>
        public void OnNotify(ISubject<BasePlayerData> subject, BasePlayerData data)
        {
            UpdateUI(data as PlayerProperty);
        }
        #endregion
    }
}
