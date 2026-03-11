using System.Dynamic;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    public interface IObjectPooling
    {
        public string GetName();
        public GameObject GetObject();
        public void OnCreateHandler();
        public void OnGetHandler();
        public void OnReleaseHandler();
        public void OnDestroyHandler();
    }
}
