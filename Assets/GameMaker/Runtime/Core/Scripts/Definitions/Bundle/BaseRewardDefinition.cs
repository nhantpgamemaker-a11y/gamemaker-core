
using System;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public abstract class BaseRewardDefinition : BaseDefinition, IReferenceDefinition
    {   
        public BaseRewardDefinition(string id, string name, string title, string description, Sprite icon, BaseMetaData metaData): base(id, name, title, description, icon, metaData)
        {
            
        }
        public abstract IDefinition GetDefinition();

        public  string GetReferenceID()
        {
            return GetID();
        }
    }
}