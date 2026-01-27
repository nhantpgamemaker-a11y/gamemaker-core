using System;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public abstract class Definition : IDefinition, ICloneable, IEquatable<BaseDefinition>
    {
        [SerializeField]
        private string _id = "";
        [SerializeField]
        private string _name = "";
        public Definition()
        {
            
        }
        public Definition(string id, string name)
        {
            _id = id;
            _name = name;
        }
        public string GetID()
        {
            return _id;
        }

        public string GetName()
        {
            return _name;
        }
        public void SetName(string value)
        {
            _name = value;
        }
        public abstract object Clone();
        
        public bool Equals(BaseDefinition other)
        {
            return other.GetID() == GetID();
        }
    }
}
