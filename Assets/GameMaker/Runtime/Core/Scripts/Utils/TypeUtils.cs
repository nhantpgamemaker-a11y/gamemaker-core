using System;
using System.Linq;

namespace GameMaker.Core.Runtime
{
    public static class TypeUtils
    {
        public static Type[] GetAllDerivedNonAbstractTypes(Type baseType)
        {
            return AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => baseType.IsAssignableFrom(t)
                            && t != baseType
                            && !t.IsAbstract)
                .ToArray();
        }
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
        public static Type[] GetAllConcreteDerivedTypes(Type baseType)
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
        public static Type[] GetAllConcreteAssignableTypes(Type baseType)
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
    }
}