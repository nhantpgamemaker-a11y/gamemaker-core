using System;
using Newtonsoft.Json;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "GameMaker/LevelEditor/LevelData", order = 0)]
[Serializable]
[JsonObject(MemberSerialization.OptIn)]
public class LevelData : ScriptableObject, IDisposable, ISerializationCallbackReceiver
{
    public void Dispose()
    {
        
    }

    public void OnAfterDeserialize()
    {
        
    }

    public void OnBeforeSerialize()
    {
        
    }
}
