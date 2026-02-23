using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class CurrencyID : BaseID
    {
        public BasePlayerCurrency GetPlayerCurrency()
        {
            return CurrencyGateway.Manager.GetPlayerCurrency(ID);
        }
        public BaseCurrencyDefinition GetBaseCurrencyDefinition()
        {
            return CurrencyManager.Instance.GetDefinition(ID);
        }
        public override List<IDefinition> GetDefinitions()
        {
            return CurrencyManager.Instance.GetDefinitions().Cast<IDefinition>().ToList();
        }
        public async UniTask<bool> AddCurrencyAsync(string amount, IExtendData extendData)
        {
            return await CurrencyGateway.Manager.AddPlayerCurrencyAsync(this.ID, amount, extendData);
        }
    }
}