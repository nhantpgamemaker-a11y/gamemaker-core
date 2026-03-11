using Cysharp.Threading.Tasks;
using GameMaker.Core.Runtime;
using GameMaker.UI.Runtime;
using TMPro;
using UnityEngine;



namespace GameMaker.UI.Runtime
{
    public class BaseAlert : BaseUI, IObjectPooling
    {
        public const string BASE_ALERT_NAME = "BaseAlert";
        [UnityEngine.SerializeField]
        private TMP_Text _txtText;
        [UnityEngine.SerializeField]
        private string _alertName = BASE_ALERT_NAME;
        
        #region  IObjectPooling
        public string GetName()
        {
            return _alertName;
        }

        public GameObject GetObject()
        {
            return this.gameObject;
        }

        public void OnCreateHandler()
        {
            
        }

        public void OnDestroyHandler()
        {
           
        }

        public void OnGetHandler()
        {
            
        }

        public void OnReleaseHandler()
        {
        }
        #endregion

        protected override void OnShow()
        {
            _txtText.text = data as string;
        }
    }
}