using System;
using Cysharp.Threading.Tasks;

namespace GameMaker.Core.Runtime
{
    public interface IDataSpaceProvider
    {
        public UniTask<bool> InitAsync(BaseDataSpaceSetting baseDataSpaceSetting);
    }
}