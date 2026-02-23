using System.ComponentModel;
using System.Linq;
using Cysharp.Threading.Tasks;
using GameMaker.Core.Runtime;
using GameMaker.UI.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CatAdventure.GamePlay
{
    public class UILevelItemController : MonoBehaviour,IObjectPooling
    {
        public const string POOLING_NAME = "UILevelItemController";
        [SerializeField] private TMP_Text _txtLevel;
        [SerializeField] private RectTransform _lockRect;
        [SerializeField] private RectTransform _statRect;

        [SerializeField] private RectTransform _flagRect;
        [SerializeField] private RectTransform[] _statRects;
        [SerializeField] private RectTransform[] _statFillRects;
        [SerializeField] private Button _button;
        [SerializeField] private PropertyID _levelPropertyID;
        private int _level;
        private UIController _uiController;
        public void OnInit(int level, UIController uIController)
        {
            _uiController = uIController;
            _level = level;
            _txtLevel.text = level.ToString();
            var playerLevel = (int)_levelPropertyID.GetPlayerStat().Value;
            _flagRect.gameObject.SetActive(playerLevel == level);
            if (playerLevel == level)
            {
                _lockRect.gameObject.SetActive(false);
                _statRect.gameObject.SetActive(false);
            }
            else
            {
                _lockRect.gameObject.SetActive(playerLevel < level);
                _statRect.gameObject.SetActive(playerLevel > level);
            }
            int random = Random.Range(0, _statFillRects.Length);
            for (int i = 0; i < _statFillRects.Length; i++)
            {
                _statFillRects[i].gameObject.SetActive(i <= random);
            }
            _button.onClick.AddListener(OnClickButton);
        }

       

        public void OnClear()
        {
            _button.onClick.RemoveListener(OnClickButton);
        }
        public string GetName()
        {
            return POOLING_NAME;
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
        private void OnClickButton()
        {
            var playerLevel = (int)_levelPropertyID.GetPlayerStat().Value;
            if(playerLevel >= _level)
            {
                _uiController.PopupController.ShowAsync<PlayPopup>(PlayPopup.POPUP_NAME, new PlayPopupData(_level)).Forget();
            }
        }
    }
}
