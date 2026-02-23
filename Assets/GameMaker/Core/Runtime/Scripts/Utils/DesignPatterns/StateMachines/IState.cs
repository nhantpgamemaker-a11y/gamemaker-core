using UnityEngine;

namespace GameMaker.Core.Runtime
{
    public interface IState<T>
    {
        public T GetStateType();
        public void OnEnterState(BaseStateData baseStateData = null);
        public void OnExitState();
    }
}
