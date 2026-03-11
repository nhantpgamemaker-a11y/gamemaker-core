using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class TimedID : BaseID
    {
        public override List<IDefinition> GetDefinitions()
        {
            return TimedManager.Instance.GetDefinitions().Cast<IDefinition>().ToList();
        }
        public TimedDefinition GetTimedDefinition()
        {
            return TimedManager.Instance.GetDefinition(ID);
        }
        public PlayerTimed GetPlayerTimed()
        {
            return TimedGateway.Manager.GetPlayerTimed(ID);
        }
        public async UniTask<bool> AddPlayerTimedAsync(long amount, IExtendData extendData)
        {
            return await TimedGateway.Manager.AddPlayerTimedAsync(ID, amount, extendData);
        }
    }
}