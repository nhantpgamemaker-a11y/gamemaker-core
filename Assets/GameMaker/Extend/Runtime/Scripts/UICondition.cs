using System;
using Cysharp.Threading.Tasks;
using GameMaker.Core.Runtime;
using GameMaker.UI.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameMaker.Extension.Runtime
{
    public class UICondition : MonoBehaviour, Core.Runtime.IObserver<BasePlayerData>
    {
        [SerializeField] private BaseUI _rootUI;
        [SerializeField] private ConditionID _conditionID;
        [SerializeField] private GameObject _activeObject;
        [SerializeField] private GameObject _inactiveObject;
        [SerializeField] private Button _btnInActive;
        [SerializeField] private TMP_Text _txtLockTitle;
        private void OnEnable()
        {
            _rootUI.OnShowAction += OnRootUIOnShow;
            _btnInActive?.onClick.AddListener(OnBtnInActiveClick);
            _txtLockTitle.text = _conditionID.GetConditionDefinition().GetTitle();
            _conditionID.GetPlayerStat().AddObserver(this);
        }

        private void OnDisable()
        {
            _rootUI.OnShowAction -= OnRootUIOnShow;
            _btnInActive?.onClick.RemoveListener(OnBtnInActiveClick);
            _conditionID.GetPlayerStat().RemoveObserver(this);
        }
        private void OnRootUIOnShow()
        {
            var status = _conditionID.CheckCondition();
            _activeObject?.SetActive(status);
            _inactiveObject?.SetActive(!status);
        }
        private void OnBtnInActiveClick()
        {
            ShowAlert(_conditionID);
        }
        protected virtual void ShowAlert(ConditionID conditionID)
        {
            _rootUI.UIController.AlertController.ShowAsync(conditionID.GetConditionDefinition().GetDescription()).Forget();
        }

        public void OnNotify(ISubject<BasePlayerData> subject, BasePlayerData data)
        {
            OnRootUIOnShow();
        }
    }
}
