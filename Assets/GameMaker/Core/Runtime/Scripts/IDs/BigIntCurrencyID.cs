using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Cysharp.Threading.Tasks;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class BigIntCurrencyID : CurrencyID
    {
        public override List<IDefinition> GetDefinitions()
        {
            return CurrencyManager.Instance.GetDefinitions().Where(x=>x.GetType() ==typeof(BigIntCurrencyDefinition)).Cast<IDefinition>().ToList();
        }
        public async UniTask<bool> AddCurrencyAsync(BigInteger amount, IExtendData extendData)
        {
            return await CurrencyGateway.Manager.AddPlayerCurrencyAsync(this.ID, amount, extendData);
        }
    }
}