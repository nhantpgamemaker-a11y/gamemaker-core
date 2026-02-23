using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CatAdventure.GamePlay
{
    [System.Serializable]
    public class GameModelConfig: ICloneable
    {
        [UnityEngine.SerializeField]
        private string _modelID;
        [UnityEngine.SerializeField]
        private Vector3 _localPosition;
        [UnityEngine.SerializeField]
        private Quaternion _localRotation;
        [UnityEngine.SerializeField]
        private List<GameMechanicData> _gameMechanicDatas;

        public string ModelID { get => _modelID; set => _modelID = value; }
        public Vector3 LocalPosition { get => _localPosition; set => _localPosition = value; }
        public Quaternion LocalRotation { get => _localRotation; set => _localRotation = value; }
        public List<GameMechanicData> GameMechanicDatas { get => _gameMechanicDatas; set => _gameMechanicDatas = value; }
        public GameModelConfig(string modelID, Vector3 localPosition, Quaternion localRotation, List<GameMechanicData> gameMechanicDatas)
        {
            _modelID = modelID;
            _localPosition = localPosition;
            _localRotation = localRotation;
            _gameMechanicDatas = gameMechanicDatas;
        }

        public object Clone()
        {
            return new GameModelConfig(_modelID, _localPosition, _localRotation, GameMechanicDatas.Select(x => x.Clone() as GameMechanicData).ToList());
        }
    }
}