using System.Collections.Generic;

namespace GameMaker.Core.Runtime
{
    public abstract class BaseStateMachine<T> : IStateMachine<T>
    {
        protected Dictionary<T, IState<T>> stateLookup;
        protected T currentStateType;
        protected IState<T> currentState;
        
        public void ChangeState(T stateType, BaseStateData baseStateData = null)
        {
            currentState?.OnExitState();
            currentStateType = stateType;
            currentState = stateLookup[currentStateType];
            currentState?.OnEnterState(baseStateData);
        }

        public virtual void OnInit(List<IState<T>> states)
        {
            stateLookup = new();
            foreach(var state in states)
            {
                stateLookup[state.GetStateType()] = state;
            }
        }

        IState<T> IStateMachine<T>.GetCurrentState()
        {
            return currentState;
        }

        T IStateMachine<T>.GetCurrentStateType()
        {
            return currentStateType;
        }

        Dictionary<T, IState<T>> IStateMachine<T>.GetStatesLookup()
        {
            return stateLookup;
        }
    }
}