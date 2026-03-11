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
    }
}