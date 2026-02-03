using System;
using System.Collections.Generic;
using System.Linq;

namespace GameMaker.Core.Runtime
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class RuntimeDataManagerAttribute : Attribute
    {
        private Type[] _dataProviderTypes;
        public Type[] DataProviderTypes { get => _dataProviderTypes; }
        public RuntimeDataManagerAttribute(Type[] dataProviderTypes)
        {
            _dataProviderTypes = dataProviderTypes;
        }
    }
}