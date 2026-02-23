namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class BasePlayerCurrency : BasePlayerData
    {
        private string _value;
        public string Value { get => _value; set => _value = value; }
        public BasePlayerCurrency(string id,IDefinition definition, string value) : base(id, definition)
        {
            _value = value;
        }

        public override object Clone()
        {
            return new BasePlayerCurrency(GetID(),definition, _value);
        }

        public void AddValue(string amount)
        {
            _value  = (float.Parse(_value) + float.Parse(amount)).ToString();
            NotifyObserver(this);
        }

        public override void CopyFrom(BasePlayerData basePlayerData)
        {
            _value = (basePlayerData as BasePlayerCurrency).Value;
            NotifyObserver(this);
        }
    }
}