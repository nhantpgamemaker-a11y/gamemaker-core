using GameMaker.Core.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameMaker.Extension.Runtime
{
    [DefaultExecutionOrder(101)]
    public class UIReward : MonoBehaviour
    {
        [UnityEngine.SerializeField]
        private Image _imgIcon;
        [UnityEngine.SerializeField]
        private TMPro.TMP_Text _txtTitle;
        [UnityEngine.SerializeField]
        private TMPro.TMP_Text _txtDescription;
        [UnityEngine.SerializeField]
        private TMPro.TMP_Text _txtAmount;
        
        private BaseRewardDefinition _rewardDefinition;

        protected Image imgIcon => _imgIcon;
        protected TMPro.TMP_Text txtTitle => _txtTitle;
        protected TMPro.TMP_Text txtDescription => _txtDescription;
        protected TMPro.TMP_Text txtAmount => _txtAmount;   
        protected BaseRewardDefinition rewardDefinition => _rewardDefinition;

        public void OnInit(BaseRewardDefinition rewardDefinition)
        {
            _rewardDefinition = rewardDefinition;
            UpdateUI();
        }
        public void OnClear()
        {
            _rewardDefinition = null;
        }
        protected virtual void UpdateUI()
        {
            if (_rewardDefinition == null) return;
            if (_imgIcon != null)
            {
                _imgIcon.sprite = _rewardDefinition.GetIcon();
            }
            if (_txtTitle != null)
            {
                _txtTitle.text = _rewardDefinition.GetTitle();
            }
            if (_txtDescription != null)
            {
                _txtDescription.text = _rewardDefinition.GetDescription();
            }
            if (_txtAmount != null)
            {
                _txtAmount.text = _rewardDefinition.GetAmount().ToString();
            }
        }
    }
}