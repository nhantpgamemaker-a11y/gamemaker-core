using Cysharp.Threading.Tasks;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class PropertyID: BaseID
    {
        public PlayerProperty GetPlayerProperty()
        {
            return PropertyGateway.Manager.GetPlayerProperty(this.ID);
        }
        public PlayerStat GetPlayerStat()
        {
            return GetPlayerProperty() as PlayerStat;
        }
        public PlayerAttribute GetPlayerAttribute()
        {
            return GetPlayerProperty() as PlayerAttribute;
        }
        public async UniTask<bool> AddStatAsync(long amount, IExtendData extendData)
        {
            return await PropertyGateway.Manager.AddPlayerStatAsync(this.ID, amount, extendData);
        }
        public async UniTask<bool> SetAttributeAsync(string value, IExtendData extendData)
        {
            return await PropertyGateway.Manager.SetAttributeAsync(this.ID, value, extendData);
        }
        public async UniTask<bool> SetStatAsync(long value, IExtendData extendData)
        {
            return await PropertyGateway.Manager.SetStatAsync(this.ID, value, extendData);
        }
    }
}