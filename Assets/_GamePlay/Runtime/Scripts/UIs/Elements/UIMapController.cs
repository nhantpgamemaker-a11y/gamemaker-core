using System;
using System.Linq;
using GameMaker.Core.Runtime;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace CatAdventure.GamePlay
{
    public class UIMapController : MonoBehaviour,IObjectPooling
    {
        [SerializeField] private string _mapName = "";
        [SerializeField] private RectTransform _mapContainer;
        [SerializeField] private RectTransform[] _checkPoints;
        [SerializeField] private RectTransform[] _points;

        public RectTransform[] CheckPoints => _checkPoints;
        public RectTransform[] Points => _points;

        private bool _showStatus = false;
        public string GetName()
        {
            return _mapName;
        }

        public GameObject GetObject()
        {
            return this.gameObject;
        }

        public int GetPointAmount() => _points.Length;

        public void OnCreateHandler()
        {
            _mapContainer.gameObject.SetActive(false);
        }

        public void OnDestroyHandler()
        {
            
        }

        public void OnGetHandler()
        {
        }

        public void OnReleaseHandler()
        {
            _showStatus = false;
        }
        public void SetMapControllerStatus(bool status)
        {
            if (status != _mapContainer.gameObject.activeInHierarchy)
            {
                _mapContainer.gameObject.SetActive(status);
            }
            _showStatus = status;
        }
        public bool GetShowStatus()
        {
            return _showStatus;
        }
    }
}
