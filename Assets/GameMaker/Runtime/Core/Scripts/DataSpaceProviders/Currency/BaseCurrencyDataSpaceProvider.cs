using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace GameMaker.Core.Runtime
{
    public abstract class BaseCurrencyDataSpaceProvider : IDataSpaceProvider
    {
        public abstract UniTask<bool> InitAsync(BaseDataSpaceSetting baseDataSpaceSetting);
    
        public abstract UniTask<bool> AddCurrencyAsync(string id, float value);

        public abstract UniTask<(bool status, List<PlayerCurrency> playerCurrencies)> GetPlayerCurrenciesAsync();

        public abstract UniTask<(bool status, PlayerCurrency playerCurrency)> GetPlayerCurrencyAsync(string id);

        public virtual void Dispose(){}
    }
}