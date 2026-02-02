using System;
using System.Collections.Generic;
using System.Linq;

namespace GameMaker.Core.Runtime
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class RuntimeDataManagerAttribute : Attribute
    {
        private Type[] _dataProviderTypes;
        private Type[] _dataManagers;
        public Type[] DataProviderTypes { get => _dataProviderTypes; }
        public Type[] DataManagers { get => _dataManagers; }
        public RuntimeDataManagerAttribute(Type[] dataProviderTypes, Type[] dataManagers)
        {
            _dataProviderTypes = dataProviderTypes;
            _dataManagers = dataManagers;
        }
    }
}