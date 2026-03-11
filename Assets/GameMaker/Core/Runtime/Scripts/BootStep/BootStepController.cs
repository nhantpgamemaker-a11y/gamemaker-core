using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    public class BootStepController : MonoBehaviour
    {
        [SerializeField] private BaseBootStep[] _bootSteps;
        private event Action<float> OnProgressEvent;
        private List<Func<FailBootStepData, UniTask>> OnFailBootStepEvent = new();
        public void Init()
        {
            for(int i=0;i< _bootSteps.Length; i++)
            {
                _bootSteps[i].Init(PlayFailBootStepFunc);
            }
        }
        public async UniTask StartBootAsync()
        {
            for (int i = 0; i < _bootSteps.Length; i++)
            {
                OnProgressEvent.Invoke(((float)i + 1) / _bootSteps.Length);
                await _bootSteps[i].StartBootAsync();
            }
            if(_bootSteps.Count() == 0)
            {
                OnProgressEvent.Invoke(1f);
            }
        }
        private async UniTask PlayFailBootStepFunc(FailBootStepData failBootStepData)
        {
            for(int i=0;i< OnFailBootStepEvent.Count; i++)
            {
                await OnFailBootStepEvent[i].Invoke(failBootStepData);
            }
        }
        public void AddProgressEvent(Action<float> action)
        {
            OnProgressEvent += action;
        }
        public void RemoveProgressEvent(Action<float> action)
        {
            OnProgressEvent -= action;
        }
        public void AddFailBootStepEvent(Func<FailBootStepData, UniTask> func)
        {
            OnFailBootStepEvent.Add(func);
        }
        public void RemoveFailBootStepEvent(Func<FailBootStepData, UniTask> func)
        {
            OnFailBootStepEvent.Remove(func);
        }
    }
}
