using Cysharp.Threading.Tasks;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class CurrencyID: BaseID
    {
        public BasePlayerCurrency GetPlayerCurrency()
        {
            return CurrencyGateway.Manager.GetPlayerCurrency(ID);
        }
        public async UniTask<bool> AddCurrencyAsync(float amount, IExtendData extendData)
        {
            return await CurrencyGateway.Manager.AddPlayerCurrencyAsync(this.ID, amount, extendData);
        }
    }
}