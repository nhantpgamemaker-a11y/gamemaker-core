using System;
using UnityEngine;

namespace GameMaker.UI.Runtime
{
    public abstract class BaseView : BaseUI
    {
        protected ViewController viewController;

        internal void OnInit(ViewController viewController)
        {
            this.viewController = viewController;
        }
    }
}