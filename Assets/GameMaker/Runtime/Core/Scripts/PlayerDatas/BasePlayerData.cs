using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public abstract class BasePlayerData : IEquatable<BasePlayerData>,IReferenceDefinition, IObserverData,
                                            ICloneable,ISubject<BasePlayerData>
    {
        [UnityEngine.SerializeReference]
        protected IDefinition definition;
        private List<Core.Runtime.IObserver<BasePlayerData>> _observers;
        public BasePlayerData(IDefinition definition) : base()
        {
            this.definition = definition;
            _observers = new();
        }
        public void AddObserver(Core.Runtime.IObserver<BasePlayerData> observer)
        {
            _observers.Add(observer);
        }
        public void RemoveObserver(Core.Runtime.IObserver<BasePlayerData> observer)
        {
            _observers.Remove(observer);
        }
        public void NotifyObserver(BasePlayerData observerData)
        {
            foreach (var observer in _observers)
            {
                observer.OnNotify(this, observerData);
            }
        }

        public abstract object Clone();
        public abstract void CopyFrom(BasePlayerData basePlayerData);

        public bool Equals(BasePlayerData other)
        {
            return definition.GetID() == other.GetReferenceID();
        }

        public string GetReferenceID()
        {
            return definition.GetID();
        }

        public IDefinition GetDefinition()
        {
            return definition;
        }
    }
}