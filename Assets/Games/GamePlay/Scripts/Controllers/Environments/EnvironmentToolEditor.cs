using System.Collections.Generic;
using System.Linq;
using GamePlay;
using UnityEngine;

namespace Game.GamePlay
{
    public class EnvironmentToolEditor : MonoBehaviour
    {
        [SerializeField] private MapData _mapData;
        [SerializeField] private DefinitionId[] _environmentControllerEditorPrefab;
        [SerializeField] private DefinitionId[] _monsterEditorPrefabs;
        [SerializeField] private LayerControllerEditor _layerControllerEditorPrefab;
        [SerializeField] private Transform _startSpawnPoint;
        [SerializeField] private PolygonCollider2D _cameraBoundsCollider;
        [SerializeField] private PolygonCollider2D _victoryCollider;
        [SerializeField] private Transform _monsterTransform;

        [ContextMenu("Load Map Data")]
        public void LoadMapData()
        {
            var layerControllerEditors = GetComponentsInChildren<LayerControllerEditor>().ToList();
            foreach (var tran in layerControllerEditors)
            {
                DestroyImmediate(tran.gameObject);
            }
            var monsters = _monsterTransform.GetComponentsInChildren<EnvironmentControllerEditor>();
            foreach(var monster in monsters)
            {
                DestroyImmediate(monster.gameObject);
            }
            foreach (var environmentLayerData in _mapData.EnvironmentLayers)
            {
                var layerControllerEditor = Instantiate(_layerControllerEditorPrefab, this.transform);
                layerControllerEditor.transform.localScale = environmentLayerData.Scale;
                layerControllerEditor.transform.position = new Vector3(0, 0, environmentLayerData.ZIndex);
                layerControllerEditor.SetSortingOrder(environmentLayerData.SortingOrder);

                foreach (var environmentPositionData in _mapData.EnvironmentPositionDatas)
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
            _startSpawnPoint.transform.position = _mapData.PlayerSpawnPoint;
            _cameraBoundsCollider.transform.position = _mapData.CameraData.Position;
            _cameraBoundsCollider.pathCount = _mapData.CameraData.CameraPaths.Count;
            foreach (var cameraPath in _mapData.CameraData.CameraPaths)
            {
                _cameraBoundsCollider.SetPath(cameraPath.Index, cameraPath.PathPoints.ToArray());
            }

            _victoryCollider.transform.position = _mapData.VictoryData.Position;
            _victoryCollider.pathCount = _mapData.VictoryData.CameraPaths.Count;
            foreach (var cameraPath in _mapData.VictoryData.CameraPaths)
            {
                _victoryCollider.SetPath(cameraPath.Index, cameraPath.PathPoints.ToArray());
            }


            foreach (var monster  in _mapData.MonsterPositionData)
            {
                var monsterPrefab = System.Array.Find(_monsterEditorPrefabs,
                        prefab => prefab.Id == monster.ReferenceID);
                var environmentControllerEditor = Instantiate(monsterPrefab.gameObject,
                        monster.Position,Quaternion.identity,
                        _monsterTransform);
            }
        }
        [ContextMenu("Save Map Data")]
        public void SaveMapData()
        {
            var layerControllerEditors = GetComponentsInChildren<LayerControllerEditor>();
            var layerEnvironmentDataList = new List<EnvironmentLayerData>();
            var environmentDataList = new List<EnvironmentPositionData>();
            foreach (var layerControllerEditor in layerControllerEditors)
            {
                layerEnvironmentDataList.Add(new EnvironmentLayerData(
                    layerControllerEditor.GetScale(),
                    layerControllerEditor.GetZIndex(),
                    layerControllerEditor.GetSortingOrder()
                ));

                var environmentControllerEditors = layerControllerEditor.GetComponentsInChildren<EnvironmentControllerEditor>();
                foreach (var environmentControllerEditor in environmentControllerEditors)
                {
                    environmentDataList.Add(environmentControllerEditor.GetEnvironmentData());
                }
            }
            _mapData.EnvironmentLayers = layerEnvironmentDataList;
            _mapData.EnvironmentPositionDatas = environmentDataList;
            _mapData.PlayerSpawnPoint = _startSpawnPoint.position;
            _mapData.CameraData.Position = _cameraBoundsCollider.transform.position;
            _mapData.CameraData.CameraPaths = new List<PathPoint>();
            for (int i = 0; i < _cameraBoundsCollider.pathCount; i++)
            {
                var pathPoints = new List<Vector2>(_cameraBoundsCollider.GetPath(i));
                var cameraPath = new PathPoint(i, pathPoints);
                _mapData.CameraData.CameraPaths.Add(cameraPath);
            }
            for (int i = 0; i < _victoryCollider.pathCount; i++)
            {
                var pathPoints = new List<Vector2>(_victoryCollider.GetPath(i));
                var cameraPath = new PathPoint(i, pathPoints);
                _mapData.VictoryData.CameraPaths.Add(cameraPath);
            }
            var monsters = new List<MonsterPositionData>();
            var monsterEnvironmentControllerEditors = _monsterTransform.GetComponentsInChildren<EnvironmentControllerEditor>();
            foreach (var monsterEnvironmentControllerEditor in monsterEnvironmentControllerEditors)
            {
                monsters.Add(new MonsterPositionData(monsterEnvironmentControllerEditor.GetPosition(),
                                                    monsterEnvironmentControllerEditor.GetScale(),
                                                    monsterEnvironmentControllerEditor.GetDefinitionID()));
            }
            _mapData.MonsterPositionData = monsters;
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(_mapData);
#endif
        }
    }
}
