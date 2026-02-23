using System;
using System.Collections.Generic;
using System.Linq;

namespace GameMaker.Core.Runtime
{
    public static class TypeUtils
    {
        public static IReadOnlyList<Type> GetAllDerivedNonAbstractTypes(Type baseType)
        {
            return TypeCache.Instance.GetAllDerivedNonAbstractTypes(baseType);
        }
#if UNITY_EDITOR
        public static IReadOnlyList<Type> GetAllDerivedNonAbstractTypes_Editor(Type baseType)
        {
            return AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => baseType.IsAssignableFrom(t)
                            && t != baseType
                            && !t.IsAbstract)
                .ToArray();
        }
#endif
        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseType"></param>
        /// <returns></returns> <summary>
        /// 
        /// </summary>
        /// <param name="baseType"></param>
        /// Not include param type
        /// <returns></returns>
        public static IReadOnlyList<Type> GetAllConcreteDerivedTypes(Type baseType)
        {
            return TypeCache.Instance.GetAllConcreteDerivedTypes(baseType);
        }
#if UNITY_EDITOR
        public static IReadOnlyList<Type> GetAllConcreteDerivedTypes_Editor(Type baseType)
        {
            var allTypes = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(a =>
                {
                    try { return a.GetTypes(); }
                    catch { return Array.Empty<Type>(); }
                })
                .Where(t => baseType.IsAssignableFrom(t)
                            && t != baseType
                            && !t.IsAbstract)
                .ToArray();

            return allTypes;
        }
#endif
        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseType"></param>
        /// <returns></returns> <summary>
        /// 
        /// </summary>
        /// <param name="baseType"></param>
        /// Include param type if not abstract
        /// <returns></returns>
        public static IReadOnlyList<Type> GetAllConcreteAssignableTypes(Type baseType)
        {
            return TypeCache.Instance.GetAllConcreteAssignableTypes(baseType);
        }
#if UNITY_EDITOR
        public static IReadOnlyList<Type> GetAllConcreteAssignableTypes_Editor(Type baseType)
        {
            return AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(a =>
                {
                    try { return a.GetTypes(); }
                    catch { return Array.Empty<Type>(); }
                })
                .Where(t => baseType.IsAssignableFrom(t)
                            && !t.IsAbstract)
                .ToArray();
        }
#endif
    }
}