using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    public interface IUICollection
    {
        
    }
    public class UICollectionAnimation : AutomaticMonoSingleton<UICollectionAnimation>
    {
        private Dictionary<string, List<IUICollection>> _uiCollectionListDict;
        public override void OnLoad()
        {
            base.OnLoad();
            _uiCollectionListDict = new();
        }
        public void Add(string id, IUICollection uICollection)
        {
            if (_uiCollectionListDict.TryGetValue(id, out var uiCollectionList))
            {
                uiCollectionList.Add(uICollection);
            }
            else
            {
                var list = new List<IUICollection>();
                list.Add(uICollection);
                _uiCollectionListDict.Add(id, list);
            }
        }
        public void Remove(string id, IUICollection uICollection)
        {
            if (_uiCollectionListDict.TryGetValue(id, out var uiCollectionList))
            {
                uiCollectionList.Contains(uICollection);
                uiCollectionList.Remove(uICollection);
                if (uiCollectionList.Count == 0)
                    _uiCollectionListDict.Remove(id);
            }
        }
        public bool IsLast(string id, IUICollection uICollection)
        {
            if (_uiCollectionListDict.TryGetValue(id, out var uiCollectionList))
            {
                return uiCollectionList.Last() == uICollection;
            }
            return false;
        }
    }
}
