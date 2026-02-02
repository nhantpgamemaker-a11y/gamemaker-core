using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace GameMaker.Core.Runtime
{
    public abstract class BaseBundleDataSpaceProvider : IDataSpaceProvider
    {
        public abstract UniTask<bool> InitAsync(BaseDataSpaceSetting baseDataSpaceSetting);
        public virtual void Dispose()
        {
        }
        public abstract UniTask<(bool, List<BaseReceiverProduct>)> ConsumeAsync(BundleDefinition bundleDefinition);

        
    }
}