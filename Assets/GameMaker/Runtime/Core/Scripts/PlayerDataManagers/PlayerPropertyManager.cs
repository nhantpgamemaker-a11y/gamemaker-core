namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class PlayerPropertyManager : PlayerDataManager
    {
        public PlayerProperty GetProperty(string referenceId)
        {
            return GetPlayerData(referenceId) as PlayerProperty;
        }
        public void AddObserver(IObserverWithScope<PlayerProperty, string> observer, string[] scopes)
        {
            AddObserver((IObserverWithScope<BasePlayerData, string>)observer, scopes);
        }
        public void RemoveObserver(IObserverWithScope<PlayerProperty, string> observer, string[] scopes)
        {
            RemoveObserver((IObserverWithScope<BasePlayerData, string>)observer, scopes);
        }
        public void AddStat(string id, long value, IExtendData extendData)
        {
            var playerProperty = GetPlayerData(id);
            var playerStat = playerProperty as PlayerStat;
            playerStat.AddValue(value);
            RuntimeActionManager.Instance.NotifyAction(PropertyActionDefinition.ADD_STAT_ACTION_DEFINITION_ID,
            new StatActionData(id, value, extendData));
        }
        public void SetStat(string id, long value, IExtendData extendData)
        {
            var playerProperty = GetPlayerData(id);
            var playerStat = playerProperty as PlayerStat;
            playerStat.SetValue(value);
            RuntimeActionManager.Instance.NotifyAction(PropertyActionDefinition.SET_STAT_ACTION_DEFINITION_ID,
            new StatActionData(id, value, extendData));
        }
        public void SetAttribute(string id, string value, IExtendData extendData)
        {
            var playerProperty = GetPlayerData(id);
            var playerAttribute = playerProperty as PlayerAttribute;
            playerAttribute.SetValue(value);
            RuntimeActionManager.Instance.NotifyAction(PropertyActionDefinition.SET_ATTRIBUTE_ACTION_DEFINITION_ID,
            new AttributeActionData(id, value, extendData));
        }
    }
}