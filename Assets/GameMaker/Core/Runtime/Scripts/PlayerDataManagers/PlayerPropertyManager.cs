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
        public void Set(string id, string value)
        {
            var playerProperty = GetPlayerData(id) as PlayerProperty;
            playerProperty.Set(value);
        }
    }
}