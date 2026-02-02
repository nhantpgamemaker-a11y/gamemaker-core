using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    public static class SingletonUtils
    {
        public static ScriptableObject Get(Type type)
        {
            if (type == null)
                return null;

            if (!typeof(ScriptableObject).IsAssignableFrom(type))
                return null;

            var instanceProp = type.GetProperty(
                "Instance",
                BindingFlags.Public | BindingFlags.Static);

            if (instanceProp == null)
                return null;

            return instanceProp.GetValue(null) as ScriptableObject;
        }

        public static T Get<T>() where T : ScriptableObjectSingleton<T>
        {
            return ScriptableObjectSingleton<T>.Instance;
        }
    }
}