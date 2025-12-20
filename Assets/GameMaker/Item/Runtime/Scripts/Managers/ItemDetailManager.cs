using System.Collections.Generic;
using System.Linq;
using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Item.Runtime
{
    [ScriptableObjectSingletonPathAttribute("Assets/Resources/Items")]
    [CreateAssetMenu(fileName ="ItemDetailManager",menuName = "GameMaker/Items/ItemDetailManager")]
    public class ItemDetailManager : BaseScriptableObjectDataManager<ItemDetailManager, ItemDetailDefinition>
    {
        protected override void OnLoad()
        {
            base.OnLoad();
            ValidItemDetails();

        }
        public List<ItemDetailDefinition> GetItemDetailDefinitions(string itemDefinitionId)
        {
            return GetDefinitions().Where(x => x.ItemDefinitionId == itemDefinitionId).ToList();
        }

        [ContextMenu("Valid ItemDetails")]
        private void ValidItemDetails()
        {
#if UNITY_EDITOR
            var itemManager = ItemManager.Instance;
            var itemDefinitions = ItemManager.Instance.GetDefinitions().Cast<ItemDefinition>();
            foreach (var itemDefinition in itemDefinitions)
            {
                var itemDetailDefinitions = GetItemDetailDefinitions(itemDefinition.GetID());
                foreach (var itemDetail in itemDetailDefinitions)
                {
                    itemDetail.ValidItem(itemDefinition);
                }
            }
            UnityEditor.EditorUtility.SetDirty(Instance);
#endif
        }
    }
}