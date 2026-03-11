using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Feature.Shop.Runtime
{
    public class ShopExtendData : IExtendData
    {
        private string _name;
        public ShopExtendData(string name)
        {
            _name = name;
        }
        public string GetName()
        {
            return _name;
        }
    }
}