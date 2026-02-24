using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public abstract class CurrencyID : BaseID
    {
        public BasePlayerCurrency GetPlayerCurrency()
        {
            return CurrencyGateway.Manager.GetPlayerCurrency(ID);
        }
        public override List<IDefinition> GetDefinitions()
        {
            return CurrencyManager.Instance.GetDefinitions().Cast<IDefinition>().ToList();
        }
        public BaseCurrencyDefinition GetBaseCurrencyDefinition()
        {
            return CurrencyManager.Instance.GetDefinition(ID);
        }
        public abstract UniTask AddCurrencyAsync(object value, IExtendData extendData);
    }
}