using UnityEngine;

namespace GameMaker.Core.Runtime
{
    public interface IMetaData
    {
        public BaseMetaData GetMetaData();
        public T GetMetaData<T>() where T : BaseMetaData;
    }
}