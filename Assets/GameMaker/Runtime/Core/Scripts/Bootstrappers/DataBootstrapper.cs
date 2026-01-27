using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    public class DataBootstrapper : MonoBehaviour
    {
        [UnityEngine.SerializeField]
        private BaseDataSpaceSetting _baseDataSpaceSetting;
        private List<PlayerDataManager> _playerDataManagers;
        [UnityEngine.SerializeReference]
        private List<BaseRuntimeDataManager> _runtimeDataManagers;
        public async UniTask<bool> BuildAsync()
        {
            bool status = true;
            status = await _baseDataSpaceSetting.InitAsync();
            if (!status) return status;

            var playerDataManagerTypes = TypeUtils.GetAllDerivedNonAbstractTypes(typeof(PlayerDataManager));
            _playerDataManagers = new();
            foreach (var playerDataManagerType in playerDataManagerTypes)
            {
                _playerDataManagers.Add(Activator.CreateInstance(playerDataManagerType) as PlayerDataManager);
            }

            var runtimeDataManagerTypes = TypeUtils.GetAllDerivedNonAbstractTypes(typeof(BaseRuntimeDataManager));

            _runtimeDataManagers = new();
            foreach (var type in runtimeDataManagerTypes)
            {
                var runtimeDataManager = Activator.CreateInstance(type) as BaseRuntimeDataManager;
                var attribute = type.GetCustomAttribute<RuntimeDataManagerAttribute>();
                if (attribute == null) continue;

                var dataManagers = _playerDataManagers.Where(x => attribute.DataManagers.Contains(x.GetType())).ToArray();
                var dataSpaceProviders = attribute.DataProviderTypes.Select(x => _baseDataSpaceSetting.Resolve<IDataSpaceProvider>(x)).ToArray();

                status = await runtimeDataManager.InitializeAsync(dataSpaceProviders, dataManagers);
                if (!status) return status;
                
                _runtimeDataManagers.Add(runtimeDataManager);
            }

            return status;
        }
    }
}
