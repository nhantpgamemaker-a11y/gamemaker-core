using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class FailBootStepData
    {
        [UnityEngine.SerializeField]
        private string _title;
        [UnityEngine.SerializeField]
        private string _description;
        public string Title { get => _title;}
        public string Description { get => _description;}
        public FailBootStepData(string title, string description)
        {
            _title = title;
            _description = description;
        }
    }
    public abstract class BaseBootStep : MonoBehaviour
    {
        [UnityEngine.SerializeField]
        private FailBootStepData _failBootStepData;
        private Func<FailBootStepData, UniTask> _onFailFuc;
        public void Init(Func<FailBootStepData, UniTask> OnFailFunc)
        {
            _onFailFuc = OnFailFunc;
        }
        public async UniTask StartBootAsync()
        {
            bool status = false;
            do
            {
                status = await PerformBootAsync();
                if (!status)
                {
                    await _onFailFuc.Invoke(_failBootStepData);
                }
            }
            while (!status);
        }
        protected abstract UniTask<bool> PerformBootAsync();
    }
}
