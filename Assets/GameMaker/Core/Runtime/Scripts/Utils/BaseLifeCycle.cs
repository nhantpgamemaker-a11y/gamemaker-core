using UnityEngine;

namespace GameMaker.Core.Runtime
{
    public abstract class BaseLifeCycle : MonoBehaviour
    {
        public abstract void Init();
        public abstract void Clear();
    }
}
