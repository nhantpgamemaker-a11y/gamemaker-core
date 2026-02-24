using System;
using UnityEngine;

namespace GameMaker.UI.Runtime
{
    public abstract class BaseView : BaseUI
    {
        protected ViewController viewController;

        internal protected virtual void OnInit(ViewController viewController)
        {
            this.viewController = viewController;
            SetUIController(viewController.UIController);
        }
    }
}