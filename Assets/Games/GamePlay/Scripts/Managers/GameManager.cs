using System;
using System.Collections.Generic;
using System.Linq;
using GameMaker.Core.Runtime;
using GamePlay;
using Unity.Cinemachine;
using UnityEngine;

namespace Game.GamePlay
{
    public class GameManager : ManualMonoSingleton<GameManager>
    {
        [SerializeField] private PlayerController _playerControllerPrefab;
        [SerializeField] private PolygonCollider2D _cameraBoundsCollider;
        [SerializeField] private PolygonCollider2D _victoryCollider;
        [SerializeField] private float _cameraPositionY = 0.48f;
        [SerializeField] private Vector2 _scenePosition = Vector2.zero;
        [SerializeField] private CinemachineCamera _cinemachineCamera;
        [SerializeField] private List<MapData> _maps = new();
        [SerializeField] private DefinitionId[] _environmentControllerEditorPrefab;
        [SerializeField] private DefinitionId[] _monsterControllerEditorPrefabs;
        [SerializeField] private LayerControllerEditor _layerControllerEditorPrefab;
        [SerializeField] private Transform _monsterLayers;
        [SerializeField] private Transform _mapContainer;
        private MapData _currentMap;
        private PlayerController _playerControllerInstance;
        public void Start()
        {
            LoadLevel();
        }
        public void LoadMap(string mapID)
        {
            MapData mapData = _maps.Find(map => map.Id == mapID);
            _currentMap = mapData;
            if (mapData != null)
            {
                foreach (var environmentLayerData in mapData.EnvironmentLayers)
                {
                    var layerControllerEditor = Instantiate(_layerControllerEditorPrefab, this.transform);
                    layerControllerEditor.transform.localScale = environmentLayerData.Scale;
                    layerControllerEditor.transform.position = new Vector3(0, 0, environmentLayerData.ZIndex);
                    layerControllerEditor.SetSortingOrder(environmentLayerData.GetSortingOrder());

                    foreach (var environmentPositionData in mapData.EnvironmentPositionDatas)
                    {
                        if (environmentPositionData.LayerIndex != environmentLayerData.ZIndex) continue;
                        var environmentDefinitionId = environmentPositionData.ReferenceID;
                        var environmentPrefab = System.Array.Find(_environmentControllerEditorPrefab,
                            prefab => prefab.Id == environmentDefinitionId);
                        if (environmentPrefab == null) continue;

                        var environmentControllerEditor = Instantiate(environmentPrefab.gameObject,
                            layerControllerEditor.transform);
                        environmentControllerEditor.transform.position = new Vector3(
                            environmentPositionData.Position.x,
                            environmentPositionData.Position.y,
                            environmentLayerData.ZIndex);
                        environmentControllerEditor.transform.localScale = environmentPositionData.Scale;
                    }
                }
                foreach(var monster in mapData.MonsterPositionData)
                {
                    var environmentPrefab = System.Array.Find(_monsterControllerEditorPrefabs,
                            prefab => prefab.Id == monster.ReferenceID);
                    var go = Instantiate(environmentPrefab.gameObject, monster.Position, Quaternion.identity,_monsterLayers);
                }
            }
            else
            {
                Debug.LogError($"Map with ID {mapID} not found!");
            }

        }
        public void LoadNextMap()
        {
            if (_currentMap == null) return;
            int currentIndex = _maps.IndexOf(_currentMap);
            int nextIndex = (currentIndex + 1) % _maps.Count;
            LoadMap(_maps[nextIndex].Id);
        }
        public void ClearMap()
        {
            var layerControllerEditors = _mapContainer.GetComponentsInChildren<LayerControllerEditor>().ToList();
            foreach (var tran in layerControllerEditors)
            {
                Destroy(tran.gameObject);
            }
            var monsters = _monsterLayers.GetComponentsInChildren<DefinitionId>();
            foreach(var monster in monsters)
            {
                Destroy(monster.gameObject);
            }

        }
        [ContextMenu("Load Level 1")]
        public void LoadLevel()
        {
            LoadMap(_maps[0].Id);
            LoadPlayer(_maps[0].PlayerSpawnPoint);
            SetupCamera();
            SetupVictoryCollider();
        }

        public void LoadPlayer(Vector3 position)
        {
            _playerControllerInstance = Instantiate(_playerControllerPrefab, position, Quaternion.identity, this.transform);
        }
        public void ClearPlayer()
        {
            if (_playerControllerInstance != null)
            {
                Destroy(_playerControllerInstance.gameObject);
                _playerControllerInstance = null;
            }
        }
        public void SetupCamera()
        {
            if (_playerControllerInstance != null)
            {
                _cinemachineCamera.Follow = _playerControllerInstance.transform;
                _cameraBoundsCollider.transform.position = _currentMap.CameraData.Position;
                _cameraBoundsCollider.pathCount = _currentMap.CameraData.CameraPaths.Count;
                foreach (var cameraPath in _currentMap.CameraData.CameraPaths)
                {
                    _cameraBoundsCollider.SetPath(cameraPath.Index, cameraPath.PathPoints.ToArray());
                }
                var confiner2D = _cinemachineCamera.GetComponent<CinemachineConfiner2D>();
                confiner2D.InvalidateBoundingShapeCache();
            }
        }
        public void SetupVictoryCollider()
        {
            _victoryCollider.gameObject.SetActive(_currentMap.VictoryData.CameraPaths.Count > 0);
            foreach (var cameraPath in _currentMap.VictoryData.CameraPaths)
            {
                _victoryCollider.SetPath(cameraPath.Index, cameraPath.PathPoints.ToArray());
            }
        }
        public void ClearCamera()
        {
            _cinemachineCamera.Follow = null;
        }
        public void HandleVictory()
        {
            ClearPlayer();
            ClearCamera();
            ClearMap();
            LoadNextMap();
            LoadPlayer(_currentMap.PlayerSpawnPoint);
            SetupCamera();
        }
    }
}
