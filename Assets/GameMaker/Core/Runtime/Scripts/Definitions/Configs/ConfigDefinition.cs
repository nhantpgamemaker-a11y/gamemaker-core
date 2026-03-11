namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class ConfigDefinition : Definition
    {
        [UnityEngine.SerializeField]
        private string _value;
        public string Value { get => _value; set => _value = value; }
        public ConfigDefinition() : base()
        {
            
        }
        public ConfigDefinition(string id, string name, string value): base(id, name)
        {
            _value = value;
        }
        public override object Clone()
        {
            return new ConfigDefinition(GetID(), GetName(), _value);
        }
        public string GetString()
        {
            return _value;
        }
        public bool TryGetInt(out int value)
        {
            return int.TryParse(_value, out value);
        }
        public bool TryGetFloat(out float value)
        {
            return float.TryParse(_value, out value);
        }
        public bool TryGetLong(out long value)
        {
            return long.TryParse(_value, out value);
        }
    }
}
