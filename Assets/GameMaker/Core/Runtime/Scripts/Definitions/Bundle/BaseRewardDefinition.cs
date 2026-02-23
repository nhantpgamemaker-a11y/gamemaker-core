
using System;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public abstract class BaseRewardDefinition : BaseDefinition, IReferenceDefinition
    {
        [UnityEngine.SerializeField]
        private string _referenceId;
        public BaseRewardDefinition() : base()
        {
            
        }
        public BaseRewardDefinition(string id,
        string name,
        string title,
        string description,
        Sprite icon,
        BaseMetaData metaData,
        string referenceId) : base(id, name, title, description, icon, metaData )
        {
            _referenceId = referenceId;
        }
        public abstract IDefinition GetDefinition();

        public  string GetReferenceID()
        {
            return _referenceId;
        }
    }
}