using Newtonsoft.Json;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class PlayerDataModel : IDefinition
    {
        [JsonProperty("Id")]
        protected string id;
        [JsonProperty("Name")]
        protected string name;

        public PlayerDataModel(string id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public string GetID()
        {
            return id;
        }

        public string GetName()
        {
            return name;
        }

        public void SetID(string id)
        {
            this.id = id;
        }

        public void SetName(string name)
        {
            this.name = name;
        }
    }
}