using System;
using Newtonsoft.Json;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public abstract class BaseReceiverProduct: ICloneable
    {
        [JsonProperty("ID")]
        private string _id;
        [JsonIgnore]
        public string ID => _id;
        public BaseReceiverProduct(string id)
        {
            _id = id;
        }

        public virtual void Consume(PlayerDataManager[] playerDataManager, IExtendData extendData)
        {
        }
        public virtual void Recall(PlayerDataManager[] playerDataManager)
        {
            
        }

        public abstract object Clone();
    }
}