using System;
using System.Collections.Generic;
using GameMaker.Core.Editor;
using GameMaker.Core.Runtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    [CustomPropertyDrawer(typeof(BaseDefinitionManager<ItemDetailDefinition>))]
    public class ItemDetailDataManagerPropertyHolder : Core.Editor.BaseDataManagerPropertyDrawer<ItemDetailDefinition>
    {
        protected override BaseDataManagerHolder<ItemDetailDefinition> CreateBaseDataManagerHolder()
        {
            var asset = Resources.Load<VisualTreeAsset>("FilterDataManagerElement");
            var templateContainer = asset.CloneTree();
            templateContainer.style.height = StyleKeyword.Auto;
            templateContainer.style.flexGrow = 1;

            List<BaseFilterHolder> filters = new();

            var stringAsset = Resources.Load<VisualTreeAsset>("StringFilterElement");
            var idFilter = new StringFilter("Id", "_id");
            var idFilterHolder = new StringFilterHolder(stringAsset.CloneTree(),idFilter);
            var nameFilter = new StringFilter("Name", "_name");
            var nameFilterHolder = new StringFilterHolder(stringAsset.CloneTree(),nameFilter);
            var titleFilter = new StringFilter("Title", "_title");
            var titleFilterHolder = new StringFilterHolder(stringAsset.CloneTree(), titleFilter);
            var itemFiler = new StringFilter("Item", "_itemDefinitionId");
            var itemFilerHolder = new StringFilterHolder(stringAsset.CloneTree(),itemFiler);
            filters.Add(idFilterHolder);
            filters.Add(nameFilterHolder);
            filters.Add(titleFilterHolder);
            filters.Add(itemFilerHolder);

            return new ItemDetailDataManagerHolder(templateContainer, filters);
        }
    }
    public class ItemDetailDataManagerHolder : FilterDataManagerHolder<ItemDetailDefinition>
    {
        public ItemDetailDataManagerHolder(VisualElement root, List<BaseFilterHolder> filters) : base(root, filters)
        {
        }

        protected override string GetTitle()
        {
            return "Item Detail";
        }

        protected override DefinitionHolder CreateHolder()
        {
            var asset = Resources.Load<VisualTreeAsset>(
            "ItemDetailDefinitionElement");
            var templateContainer = asset.CloneTree();
            templateContainer.style.height = StyleKeyword.Auto;
            templateContainer.style.flexGrow = 1;
            return new ItemDetailDefinitionHolder(asset.CloneTree());
        }
        protected override void OnAddButtonClicked()
        {
            List<ActionData> actionDatas = new();
            EditorWindow window = null;
            foreach(var item in ItemManager.Instance.GetDefinitions())
            {
                var actionData = new ActionData(item.GetName(), () =>
                {
                    var itemDetail = new ItemDetailDefinition(item);
                    int newIndex = definitionProperty.arraySize;
                    definitionProperty.InsertArrayElementAtIndex(newIndex);
                    var newElement = definitionProperty.GetArrayElementAtIndex(newIndex);
                    newElement.managedReferenceValue = itemDetail;
                    MakeItemSource(GetItemSource());
                    definitionProperty.serializedObject.ApplyModifiedProperties();
                    definitionProperty.serializedObject.Update();
                    itemListView.RefreshItems();
                });
                actionDatas.Add(actionData);
            }
            window = ButtonActionWindowEditor.ShowWindow(GetTitle(),actionDatas);
        }
    }
}
