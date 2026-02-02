using System;
using Cysharp.Threading.Tasks;

namespace GameMaker.Core.Runtime
{
    public interface IDataSpaceProvider :IDisposable
    {
        public UniTask<bool> InitAsync(BaseDataSpaceSetting baseDataSpaceSetting);
    }
}