
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    [ScriptableObjectSingletonPathAttribute("Assets/Resources")]
    [CreateAssetMenu(fileName = "TypeCache", menuName = "GameMaker/Core/TypeCache")]
    public class TypeCache : ScriptableObjectSingleton<TypeCache>
    {
        [UnityEngine.SerializeField]
        private List<TypeData> _allDerivedNonAbstractTypes;
        [UnityEngine.SerializeField]
        private List<TypeData> _allConcreteDerivedTypes;

        [UnityEngine.SerializeField]
        private List<TypeData> _allConcreteAssignableTypes;

        private Dictionary<Type, List<Type>> _allDerivedNonAbstractTypeDict;
        private Dictionary<Type, List<Type>> _allConcreteDerivedTypeDict;
        private Dictionary<Type, List<Type>> _allConcreteAssignableTypeDict;

        protected override void OnLoad()
        {
            base.OnLoad();
            _allDerivedNonAbstractTypeDict =
                BuildDict(_allDerivedNonAbstractTypes);

            _allConcreteDerivedTypeDict =
                BuildDict(_allConcreteDerivedTypes);

            _allConcreteAssignableTypeDict =
                BuildDict(_allConcreteAssignableTypes);

        }
        public IReadOnlyList<Type> GetAllDerivedNonAbstractTypes(Type baseType)
        {
            if (baseType == null)
                return Array.Empty<Type>();

            if (_allDerivedNonAbstractTypeDict != null &&
                _allDerivedNonAbstractTypeDict.TryGetValue(baseType, out var list))
            {
                return list;
            }

            return Array.Empty<Type>();
        }
        
        public IReadOnlyList<Type> GetAllConcreteDerivedTypes(Type baseType)
        {
            if (baseType == null)
                return Array.Empty<Type>();

            if (_allConcreteDerivedTypeDict != null &&
                _allConcreteDerivedTypeDict.TryGetValue(baseType, out var list))
            {
                return list;
            }

            return Array.Empty<Type>();
        }
        public IReadOnlyList<Type> GetAllConcreteAssignableTypes(Type baseType)
        {
            if (baseType == null)
                return Array.Empty<Type>();

            if (_allConcreteAssignableTypeDict != null &&
                _allConcreteAssignableTypeDict.TryGetValue(baseType, out var list))
            {
                return list;
            }

            return Array.Empty<Type>();
        }

        private Dictionary<Type, List<Type>> BuildDict(List<TypeData> source)
        {
            var dict = new Dictionary<Type, List<Type>>(source?.Count ?? 0);

            if (source == null)
                return dict;

            foreach (var entry in source)
            {
                var baseType = Type.GetType(entry.BaseTypeName);

                var list = new List<Type>(entry.DerivedTypeNames.Count);

                foreach (var typeName in entry.DerivedTypeNames)
                {
                    var t = Type.GetType(typeName);
                    if (t != null)
                        list.Add(t);
                }

                dict[baseType] = list;
            }

            return dict;
        }
#if UNITY_EDITOR
        public void RebuildCache()
        {
            var allTypes = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(a =>
                {
                    try { return a.GetTypes(); }
                    catch { return Array.Empty<Type>(); }
                })
                .ToArray();

            var baseTypes = allTypes
                .Where(t =>
                    t.IsAbstract &&
                    t.GetCustomAttribute<TypeCacheAttribute>() != null)
                .ToArray();

            _allDerivedNonAbstractTypes = new();
            _allConcreteDerivedTypes = new();
            _allConcreteAssignableTypes = new();
            
            foreach (var baseType in baseTypes)
            {
                var derived = allTypes
                    .Where(t =>
                        baseType.IsAssignableFrom(t) &&
                        t != baseType)
                    .ToArray();

                // 1️⃣ Derived non-abstract
                var baseTypeAQN = baseType.AssemblyQualifiedName;
                var derivedTypeAQNs = derived
                        .Where(t => !t.IsAbstract)
                        .Select(t => t.AssemblyQualifiedName)
                        .ToList();
                _allDerivedNonAbstractTypes.Add(new TypeData(baseTypeAQN, derivedTypeAQNs));

                // 2️⃣ Concrete derived
                baseTypeAQN = baseType.AssemblyQualifiedName;
                derivedTypeAQNs = derived
                    .Where(t => !t.IsAbstract)
                    .Select(t => t.AssemblyQualifiedName)
                    .ToList();
                _allConcreteDerivedTypes.Add(new TypeData(baseTypeAQN, derivedTypeAQNs));

                // 3️⃣ Concrete assignable (include base if concrete)
                baseTypeAQN = baseType.AssemblyQualifiedName;
                derivedTypeAQNs = derived
                        .Append(baseType)
                        .Where(t => !t.IsAbstract)
                        .Select(t => t.AssemblyQualifiedName)
                        .ToList();
                _allConcreteAssignableTypes.Add(new TypeData(baseTypeAQN, derivedTypeAQNs));
            }

            UnityEditor.EditorUtility.SetDirty(this);
            UnityEditor.AssetDatabase.SaveAssets();
        }
#endif

    }
    [System.Serializable]
    public class TypeData
    {
        [UnityEngine.SerializeField]
        private string _baseTypeName;
        [UnityEngine.SerializeField]
        private List<string> _derivedTypeNames;
         public string BaseTypeName { get => _baseTypeName; }
        public List<string> DerivedTypeNames { get => _derivedTypeNames; }
        public TypeData(string baseTypeName, List<string> derivedTypeNames)
        {
            _baseTypeName = baseTypeName;
            _derivedTypeNames = derivedTypeNames;
        }

       
    }
}