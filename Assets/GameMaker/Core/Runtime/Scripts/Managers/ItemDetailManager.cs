using System.Collections.Generic;
using System.Linq;
using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [ScriptableObjectSingletonPathAttribute("Assets/Resources")]
    [CreateAssetMenu(fileName = "ItemDetailManager", menuName = "GameMaker/Items/ItemDetailManager")]
    public class ItemDetailManager : BaseScriptableObjectDataManager<ItemDetailManager, ItemDetailDefinition>
    {
        protected override void OnLoad()
        {
            base.OnLoad();
        }
        public List<ItemDetailDefinition> GetItemDetailDefinitions(string itemDefinitionId)
        {
            return GetDefinitions().Where(x => x.ItemDefinitionId == itemDefinitionId).ToList();
        }
#if UNITY_EDITOR
        [ContextMenu("Valid ItemDetails")]
        private void ValidItemDetails()
        {

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

        }

        [ContextMenu("Add 10K Items")]
        public void Add10kItems()
        {
            for (int i = 0; i < 10000; i++)
            {
                AddDefinition(new ItemDetailDefinition());
            }
            UnityEditor.EditorUtility.SetDirty(Instance);
        }
#endif
    }
}