using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameMaker.Core.Runtime;
using GameMaker.UI.Runtime;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
using GameMaker.Sound.Runtime;

namespace CatAdventure.GamePlay
{
    public class MapView : BaseView
    {
        public const string VIEW_NAME = "MapView";
        [SerializeField] private Button _backButton;
        [SerializeField] private ObjectPoolManager _poolingManager;
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private RectTransform _scrollRectTransform;
        [SerializeField] private RectTransform _rectContent;
        [SerializeField] private PropertyID _levelID;
        [SerializeField] private SoundID _soundID;
        [SerializeField] private Button _btnPreChapter;
        [SerializeField] private Button _btnNextChapter;
        private ChapterDefinition _chapterDefinition;
        private List<UIMapController> _mapControllers;
        private Dictionary<UIMapController, List<UILevelItemController>> _uILevelItemControllerDict;
        private Dictionary<Transform, int> _pointDict;

        protected override void OnInit(ViewController viewController)
        {
            base.OnInit(viewController);
            _poolingManager.Init();
            _mapControllers = new();
            _pointDict = new();
            _uILevelItemControllerDict = new();
        }
        protected override void OnShow()
        {

            base.OnShow();
            var chapterDefinition = ChapterConfig.Instance.GetChapter((int)_levelID.GetPlayerStat().Value);
            SetChapterAsync(chapterDefinition).Forget();
            _scrollRect.onValueChanged.AddListener(OnScrollValueChanged);
            _backButton.onClick.AddListener(OnClickBack);
            _btnPreChapter.onClick.AddListener(OnClickPreChapter);
            _btnNextChapter.onClick.AddListener(OnClickNextChapter);

            SoundRuntimeManager.Instance.PlayLoopFade(_soundID.GetSoundDefinition());
        }
        private async UniTask SetChapterAsync(ChapterDefinition chapterDefinition)
        {
            var loadingPopup = await viewController.UIController.PopupController.ShowAsync<LoadingPopup>(LoadingPopup.POPUP_NAME);
            SetChapter(chapterDefinition);
            await UniTask.WaitForEndOfFrame();
            ScrollToCurrent();
            ValidMapView();
            await viewController.UIController.PopupController.HideAsync(loadingPopup);
        }

        protected override void OnHide()
        {
            base.OnHide();

            _scrollRect.onValueChanged.RemoveListener(OnScrollValueChanged);

            _backButton.onClick.RemoveListener(OnClickBack);
            _btnPreChapter.onClick.RemoveListener(OnClickPreChapter);
            _btnNextChapter.onClick.RemoveListener(OnClickNextChapter);

            SoundRuntimeManager.Instance.StopLoopFade(_soundID.GetSoundDefinition());
        }

        protected override void OnHidden()
        {
            base.OnHidden();

            foreach (var mapController in _mapControllers)
            {
                _poolingManager.Release(mapController);
            }
            _mapControllers.Clear();

            foreach (var kvp in _uILevelItemControllerDict.Values)
            {
                foreach (var item in kvp)
                {
                    _poolingManager.Release(item);
                }
            }
            _uILevelItemControllerDict.Clear();
        }

        private void OnScrollValueChanged(Vector2 scrollValue)
        {
            ValidMapView();
        }
        private void SetChapter(ChapterDefinition chapterDefinition)
        {
            _pointDict.Clear();
            int levelIndex = chapterDefinition.FromLevel;
            foreach (var mapController in _mapControllers)
            {
                _poolingManager.Release(mapController);
            }
            _mapControllers.Clear();
            foreach (var mapItems in _uILevelItemControllerDict.Values)
            {
                foreach (var mapItem in mapItems)
                {
                    mapItem.OnClear();
                    _poolingManager.Release(mapItem);
                }
            }
            _uILevelItemControllerDict.Clear();


            _chapterDefinition = chapterDefinition;
            var mapIds = _chapterDefinition.MapIds;

            for (int i = 0; i < mapIds.Count; i++)
            {
                var uiMapController = _poolingManager.Get<UIMapController>(mapIds[i]);
                uiMapController.transform.SetParent(_rectContent.transform);
                _mapControllers.Add(uiMapController);
                var points = uiMapController.Points;
                foreach (var point in points)
                {
                    _pointDict[point] = levelIndex;
                    levelIndex++;
                }
            }
            var preChapter = ChapterConfig.Instance.GetChapter((int)chapterDefinition.FromLevel - 1);
            _btnPreChapter.gameObject.SetActive(preChapter != null);

            var nextChapter = ChapterConfig.Instance.GetChapter((int)chapterDefinition.ToLevel + 1);
            _btnNextChapter.gameObject.SetActive(nextChapter != null);
        }
        private async UniTask SetChapterDefinitionAndShowLoadingAsync(ChapterDefinition chapterDefinition)
        {

        }
        private void ValidMapView()
        {
            foreach (var mapController in _mapControllers)
            {
                var checkPoints = mapController.CheckPoints;
                bool status = false;
                for (int i = 0; i < checkPoints.Length; i++)
                {
                    var screenPoint = RectTransformUtility.WorldToScreenPoint(viewController.UIController.UICamera, checkPoints[i].transform.position);
                    if (RectTransformUtility.RectangleContainsScreenPoint(
                            _scrollRectTransform,
                            screenPoint,
                            viewController.UIController.UICamera))
                    {
                        status = true;
                        break;
                    }
                }
                bool showStatus = mapController.GetShowStatus();
                mapController.SetMapControllerStatus(status);
                if (showStatus != status)
                {
                    if (!status)
                    {
                        if (!_uILevelItemControllerDict.ContainsKey(mapController)) continue;
                        _uILevelItemControllerDict[mapController].ForEach(x =>
                        {
                            _poolingManager.Release(x);
                        });
                        _uILevelItemControllerDict.Remove(mapController);
                    }
                    else
                    {
                        var points = mapController.Points;
                        var listItems = new List<UILevelItemController>();
                        foreach (var point in points)
                        {
                            var item = _poolingManager.Get<UILevelItemController>(UILevelItemController.POOLING_NAME);
                            item.transform.SetParent(point);
                            item.transform.localPosition = Vector3.zero;
                            item.OnInit(_pointDict[point], viewController.UIController);
                            listItems.Add(item);
                        }
                        _uILevelItemControllerDict[mapController] = listItems;
                    }
                }
            }
        }
        private void ScrollToCurrent()
        {
            var currentLevel = _levelID.GetPlayerStat().Value;
            Transform result = _pointDict
            .FirstOrDefault(kv => kv.Value == currentLevel)
            .Key;
            if(result != null)
            {
                SnapTo(result.GetComponent<RectTransform>());
            }
        }
        private void OnClickBack()
        {
            viewController.ShowAsync(HomeView.VIEW_NAME).Forget();
        }
        private void OnClickPreChapter()
        {
            OnClickPreChapterAsync().Forget();
        }
        private async UniTask OnClickPreChapterAsync()
        {
            var preChapter = ChapterConfig.Instance.GetChapter((int)_chapterDefinition.FromLevel - 1);
            if (preChapter != null)
            {
                var loadingPopup = await viewController.UIController.PopupController.ShowAsync<LoadingPopup>(LoadingPopup.POPUP_NAME);
                SetChapter(preChapter);
                await UniTask.WaitForEndOfFrame();
                ValidMapView();
                await viewController.UIController.PopupController.HideAsync(loadingPopup);
            }
        }
        private void OnClickNextChapter()
        {
            OnClickNextChapterAsync().Forget();
        }
        private async UniTask OnClickNextChapterAsync()
        {
            var nextChapter = ChapterConfig.Instance.GetChapter((int)_chapterDefinition.ToLevel + 1);
            if (nextChapter != null)
            {
                var loadingPopup = await viewController.UIController.PopupController.ShowAsync<LoadingPopup>(LoadingPopup.POPUP_NAME);
                SetChapter(nextChapter);
                await UniTask.WaitForEndOfFrame();
                ValidMapView();
                await viewController.UIController.PopupController.HideAsync(loadingPopup);
            }
        }
        public void SnapTo(RectTransform target)
        {
            Canvas.ForceUpdateCanvases();

            var viewport = _scrollRect.viewport != null
                ? _scrollRect.viewport
                : _scrollRectTransform;

            Vector2 contentPos =
                (Vector2)_scrollRectTransform.InverseTransformPoint(_rectContent.position);

            Vector2 targetCenter =
                (Vector2)_scrollRectTransform.InverseTransformPoint(
                    target.TransformPoint(target.rect.center)
                );

            Vector2 viewportCenter =
                (Vector2)_scrollRectTransform.InverseTransformPoint(
                    viewport.TransformPoint(viewport.rect.center)
                );

            _rectContent.anchoredPosition += viewportCenter - targetCenter;
        }
    }
}
