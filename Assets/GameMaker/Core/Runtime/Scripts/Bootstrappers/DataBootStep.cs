using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class DataBootStep : BaseBootStep
    {
        [UnityEngine.SerializeField]
        private BaseDataSpaceSetting _baseDataSpaceSetting;
        private List<PlayerDataManager> _playerDataManagers;
        
        [UnityEngine.SerializeReference]
        private List<BaseRuntimeDataManager> _runtimeDataManagers;

        protected override UniTask<bool> PerformBootAsync()
        {
            return BuildAsync();
        }
        
        public async UniTask<bool> BuildAsync()
        {
            float startBuildTime = Time.realtimeSinceStartup;
            GameMaker.Core.Runtime.Logger.Log($"[DataBootstrapper] Start Build {startBuildTime}");
            
            await TimeManager.Instance.InitializeAsync(CoreConfig.Instance.TimePolicyConfig.TimeMode);
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
            var playerDataManager = _playerDataManagers.ToArray();
            foreach (var type in runtimeDataManagerTypes)
            {
                var runtimeDataManager = Activator.CreateInstance(type) as BaseRuntimeDataManager;
                var attribute = type.GetCustomAttribute<RuntimeDataManagerAttribute>();
                if (attribute == null) continue;

                var dataSpaceProviders = attribute.DataProviderTypes.Select(x => _baseDataSpaceSetting.Resolve<IDataSpaceProvider>(x)).ToArray();

                status = await runtimeDataManager.InitializeAsync(dataSpaceProviders, playerDataManager);
                if (!status) return status;

                _runtimeDataManagers.Add(runtimeDataManager);
            }
            GameMaker.Core.Runtime.Logger.Log($"[DataBootstrapper] Finish Build {Time.realtimeSinceStartup}");
            GameMaker.Core.Runtime.Logger.Log($"[DataBootstrapper] Time take {Time.realtimeSinceStartup - startBuildTime}");
            return status;
        }


        [ContextMenu("SAVE")]
        private void Save()
        {
            _ = _baseDataSpaceSetting.LocalDataManager.SaveAllAsync();
        }
    }
}
