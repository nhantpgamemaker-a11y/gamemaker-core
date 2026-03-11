using UnityEngine;

namespace GameMaker.Core.Runtime
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
