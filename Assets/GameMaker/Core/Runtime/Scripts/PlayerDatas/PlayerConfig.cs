namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class PlayerConfig : BasePlayerData
    {
        private string _value;
        public PlayerConfig(string id, IDefinition definition, string value) : base(id, definition)
        {
            _value = value;
        }

        public override object Clone()
        {
            throw new System.NotImplementedException();
        }
        public void SetValue(string value)
        {
            _value = value;
            NotifyObserver(this);
        }
        public string GetStringValue()
        {
            return _value;
        }
        public int GetIntValue()
        {
            int.TryParse(_value, out var result);
            return result;
        }
        public float GetFloatValue()
        {
            float.TryParse(_value, out var result);
            return result;
        }
        public override void CopyFrom(BasePlayerData basePlayerData)
        {
            var reference = basePlayerData as PlayerConfig;
            _value = reference._value;
        }
    }
}