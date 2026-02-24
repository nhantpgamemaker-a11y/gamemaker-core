using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class LongCurrencyID : CurrencyID
    {
        public override List<IDefinition> GetDefinitions()
        {
            return CurrencyManager.Instance.GetDefinitions().Where(x=>x.GetType() ==typeof(LongCurrencyDefinition)).Cast<IDefinition>().ToList();
        }
        public async UniTask<bool> AddCurrencyAsync(long amount, IExtendData extendData)
        {
            return await CurrencyGateway.Manager.AddPlayerCurrencyAsync(this.ID, amount, extendData);
        }

        public override UniTask AddCurrencyAsync(object value, IExtendData extendData)
        {
            if (value is long longValue)
            {
                return AddCurrencyAsync(longValue, extendData);
            }
            else
            {
                throw new System.ArgumentException($"Value must be of type long. Provided value: {value}");
            }
        }
    }
}