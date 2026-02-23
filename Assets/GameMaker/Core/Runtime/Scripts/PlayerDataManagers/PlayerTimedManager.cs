namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class PlayerTimedManager : PlayerDataManager
    {
        public void CopyFrom(PlayerTimed playerTimed)
        {
            var runtimePlayerTimed = GetPlayerData(playerTimed.GetID()) as PlayerTimed;
            runtimePlayerTimed.From(playerTimed);
        }
    }
}