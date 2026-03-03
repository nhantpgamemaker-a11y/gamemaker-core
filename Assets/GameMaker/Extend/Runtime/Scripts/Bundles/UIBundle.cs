using System.Collections.Generic;
using GameMaker.Core.Runtime;
using Unity.Android.Gradle;
using UnityEngine;
using UnityEngine.UI;

namespace GameMaker.Extension.Runtime
{
    [DefaultExecutionOrder(100)]
    public class UIBundle : MonoBehaviour
    {
        [UnityEngine.SerializeField]
        private BundleID _bundleID;
        [UnityEngine.SerializeField]
        private Image _imgIcon;
        [UnityEngine.SerializeField]
        private TMPro.TMP_Text _txtTitle;
        [UnityEngine.SerializeField]
        private TMPro.TMP_Text _txtDescription;
        [UnityEngine.SerializeField]
        private RectTransform _rectContent;
        [UnityEngine.SerializeField]
        private UIReward _bundleItemPrefab;

        private BundleDefinition _bundleDefinition;
        private List<UIReward> _uiRewards = new List<UIReward>();
        protected BundleID bundleID => _bundleID;
        protected BundleDefinition bundleDefinition => _bundleDefinition;
        protected Image imgIcon => _imgIcon;
        protected TMPro.TMP_Text txtTitle => _txtTitle;
        protected TMPro.TMP_Text txtDescription => _txtDescription;
        protected RectTransform rectContent => _rectContent;
        protected UIReward bundleItemPrefab => _bundleItemPrefab;
        protected List<UIReward> uiRewards => _uiRewards;
        public void SetBundleID(BundleID bundleID)
        {
            _bundleID = bundleID;
            _bundleDefinition = _bundleID.GetBundleDefinition();
        }
        public void SetBundleDefinition(BundleDefinition bundleDefinition)
        {
            _bundleDefinition = bundleDefinition;
        }
        protected virtual void OnEnable()
        {
            UpdateUI();
        }
        protected virtual void UpdateUI()
        {
            var definition = _bundleID.GetBundleDefinition();
            if (definition == null) return;
             definition = _bundleDefinition;
             if (definition == null) return;
            if (_imgIcon != null)
            {
                _imgIcon.sprite = definition.GetIcon();
            }
            if (_txtTitle != null)
            {
                _txtTitle.text = definition.GetTitle();
            }
            if (_txtDescription != null)
            {
                _txtDescription.text = definition.GetDescription();
            }
            if (_rectContent != null && _bundleItemPrefab != null)
            {
                foreach (var uiReward in _uiRewards)
                {
                    uiReward.OnClear();
                    Destroy(uiReward.gameObject);
                }
                _uiRewards.Clear();

                foreach (var reward in definition.Rewards)
                {
                    var uiReward = Instantiate(_bundleItemPrefab, _rectContent);
                    uiReward.OnInit(reward);
                    uiReward.gameObject.SetActive(true);
                    _uiRewards.Add(uiReward);
                }
            }
        }
    }
}
