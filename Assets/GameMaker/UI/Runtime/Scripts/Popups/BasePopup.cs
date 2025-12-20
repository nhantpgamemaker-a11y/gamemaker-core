using System;
using UnityEngine;

namespace GameMaker.UI.Runtime
{
    public abstract class BasePopup : BaseUI
    {
        protected PopupController popupController;

        internal void OnInit(PopupController popupController)
        {
            this.popupController = popupController;
        }
    }
}