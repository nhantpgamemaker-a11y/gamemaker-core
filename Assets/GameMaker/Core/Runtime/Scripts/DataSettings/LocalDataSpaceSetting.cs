using System;
using System.Linq;
using System.Reflection;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [CreateAssetMenu(fileName ="LocalDataSpaceSetting",menuName = "GameMaker/Core/LocalDataSpaceSetting")]
    public class LocalDataSpaceSetting: BaseDataSpaceSetting
    {
        public const string LOCAL_SPACE = "";

        public override async UniTask<bool> InitAsync()
        {
           await base.InitAsync();
            var providerTypes = TypeUtils.GetAllConcreteDerivedTypes(typeof(IDataSpaceProvider))
            .Where(x => x.GetCustomAttribute<DataSpaceAttribute>().Name == nameof(LOCAL_SPACE)).ToList();

            foreach(var type in providerTypes)
            {
                var provider = Activator.CreateInstance(type) as IDataSpaceProvider;
                await provider.InitAsync(this);
                RegisterProvider(type,provider);
            }

            return true;
        }
    }
}