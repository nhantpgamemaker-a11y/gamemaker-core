using System.Collections.Generic;
using GameMaker.Core.Runtime;
using GameMaker.UI.Runtime;
using UnityEngine;

namespace GameMaker.IAP.Runtime
{
    [DefaultExecutionOrder(98)]
    public class UIIAPGroup : MonoBehaviour
    {
        [SerializeField] private BaseUI _rootUI;
        [SerializeField] private IAPGroupDefinitionID _iAPGroupDefinitionID;
        [SerializeField] private UIIAP _uiIAPPrefab;
        [SerializeField] private RectTransform _contentRect;
        private List<UIIAP> _uiIAPs = new List<UIIAP>();
        protected virtual void OnEnable()
        {
            var iaps = _iAPGroupDefinitionID.GetIAPDefinitionsInGroup();
            foreach (var iap in iaps)
            {
                var uiIAP = Instantiate(_uiIAPPrefab, _contentRect);
                uiIAP.SetIAPDefinition(iap);
                uiIAP.gameObject.SetActive(true);
                _uiIAPs.Add(uiIAP);
            }
        }
        protected virtual void OnDisable()
        {
            foreach (var uiIAP in _uiIAPs)            
            {
                Destroy(uiIAP.gameObject);
            }
            _uiIAPs.Clear();
        }
    }
}
