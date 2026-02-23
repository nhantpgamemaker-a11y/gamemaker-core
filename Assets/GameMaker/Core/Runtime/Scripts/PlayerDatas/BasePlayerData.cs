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
        [UnityEngine.SerializeField]
        private string _id;
        [UnityEngine.SerializeReference]
        protected IDefinition definition;
        private List<Core.Runtime.IObserver<BasePlayerData>> _observers;
        public BasePlayerData(string id,IDefinition definition) : base()
        {
            _id = id;
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
        public virtual void CopyFrom(BasePlayerData basePlayerData)
        {
            _id = basePlayerData._id;
            definition = basePlayerData.definition;
            _observers = basePlayerData._observers;
        }

        public virtual bool Equals(BasePlayerData other)
        {
            return definition.GetID() == other.GetID();
        }

        public string GetReferenceID()
        {
            return definition.GetID();
        }

        public IDefinition GetDefinition()
        {
            return definition;
        }
        public string GetID()
        {
            return _id;
        }
    }
}