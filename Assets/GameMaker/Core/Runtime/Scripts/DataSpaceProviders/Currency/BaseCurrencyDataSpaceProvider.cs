using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace GameMaker.Core.Runtime
{
    public abstract class BaseCurrencyDataSpaceProvider : IDataSpaceProvider
    {
        public abstract UniTask<bool> InitAsync(BaseDataSpaceSetting baseDataSpaceSetting);
    
        public abstract UniTask<bool> AddCurrencyAsync(string id, string value);

        public abstract UniTask<(bool status, List<BasePlayerCurrency> playerCurrencies)> GetPlayerCurrenciesAsync();

        public abstract UniTask<(bool status, BasePlayerCurrency playerCurrency)> GetPlayerCurrencyAsync(string id);

        public virtual void Dispose(){}
    }
}