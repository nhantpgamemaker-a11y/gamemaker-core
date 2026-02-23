using System;

namespace GameMaker.Core.Runtime
{
    public class TypeContainAttribute: Attribute
    {
        private Type _type;
        public Type Type => _type;
        public TypeContainAttribute(Type type)
        {
            _type = type;
        }
    }
}