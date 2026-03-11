using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace GameMaker.Core.Runtime
{
    [CreateAssetMenu(fileName = "BaseTemplate", menuName = "GameMaker/Items/Templates/BaseCreateItemTemplate", order = 0)]
    public class BaseCreateItemTemplate : ScriptableObject
    {
        public virtual List<ItemPropertyDefinitionRef> GetItemPropertyDefinitionRefs(List<ItemPropertyDefinitionRef> itemStatDefinitionRefs)
        {
            return itemStatDefinitionRefs.Select(x => x.Clone() as ItemPropertyDefinitionRef).ToList();
        }
    }
}