using System;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public abstract class BaseActionParamDefinition: IDefinition, ICloneable
    {
        [UnityEngine.SerializeField]
        private string _id;
        [UnityEngine.SerializeField]
        private string _name;
        [UnityEngine.SerializeField]
        private string _bindingName;
        public string BindingName => _bindingName;
        public BaseActionParamDefinition()
        {
            
        }
        public BaseActionParamDefinition(string id,string name, string bindingName)
        {
            _id = id;
            _name = name;
            _bindingName = bindingName;
        }

        public abstract object Clone();

        public string GetID()
        {
            return _id;
        }

        public string GetName()
        {
            return _name;
        }

        public bool Equals(IDefinition other)
        {
            if (other == null)
                return false;

            if (GetType() != other.GetType())
                return false;

            return GetID() == other.GetID();
        }
    } 
}