using UnityEngine;
using GameMaker.Core.Runtime;

namespace GameMaker.UI.Runtime
{
    public class UIController : MonoBehaviour
    {
        [SerializeField]
        private PopupController _popupController;
        [SerializeField]
        private ViewController _viewController;
        [SerializeField]
        private Camera _uiCamera;
        public PopupController PopupController => _popupController;
        public ViewController ViewController => _viewController;
        public Camera UICamera => _uiCamera;

        public void OnInit()
        {
            _popupController.OnInit(this);
            _viewController.OnInit(this);
        }
    }
}
