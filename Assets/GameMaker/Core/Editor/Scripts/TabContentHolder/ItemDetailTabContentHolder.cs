using System.Collections.Generic;
using GameMaker.Core.Runtime;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    [CoreTabContext]
    public class ItemDetailTabContentHolder : BaseTabContentHolder
    {
        private TemplateContainer _templateContainer;
        private ItemDetailDataManagerHolder _itemDataManagerHolder;
        public ItemDetailTabContentHolder(VisualElement root) : base(root)
        {
            var asset = UIToolkitLoaderUtils.LoadUXML("FilterDataManagerElement");
            _templateContainer = asset.CloneTree();
            _templateContainer.style.height = StyleKeyword.Auto;
            _templateContainer.style.flexGrow = 1;

            var serializedObject = new UnityEditor.SerializedObject(ItemDetailManager.Instance);
            List<BaseFilterHolder> filters = new();

            var stringAsset = UIToolkitLoaderUtils.LoadUXML("StringFilterElement");
            var idFilter = new StringFilter("Id", "_id");
            var idFilterHolder = new StringFilterHolder(stringAsset.CloneTree(),idFilter);
            var nameFilter = new StringFilter("Name", "_name");
            var nameFilterHolder = new StringFilterHolder(stringAsset.CloneTree(),nameFilter);
            var titleFilter = new StringFilter("Title", "_title");
            var titleFilterHolder = new StringFilterHolder(stringAsset.CloneTree(), titleFilter);


            var dropDownAsset = UIToolkitLoaderUtils.LoadUXML("DropdownFilterElement");
            var itemRefFilter = new DropdownFilter("Item", "_itemDefinitionId");

            var data = new List<DropdownFilterHolder.DropdownData>();
            foreach(var item in ItemManager.Instance.GetDefinitions())
            {
                data.Add(new DropdownFilterHolder.DropdownData(item.GetID(), item.GetName()));
            }
            var itemRefFilterHolder = new DropdownFilterHolder(dropDownAsset.CloneTree(), itemRefFilter,data);
            filters.Add(idFilterHolder);
            filters.Add(nameFilterHolder);
            filters.Add(titleFilterHolder);
            filters.Add(itemRefFilterHolder);
            
            _itemDataManagerHolder = new ItemDetailDataManagerHolder(_templateContainer,filters);
            var serializedProperty = serializedObject.FindProperty("dataManager");
            _itemDataManagerHolder.Bind(serializedProperty);
        }

        public override int GetIndex()
        {
            return 4;
        }

        public override VisualElement GetTabView()
        {
            return _templateContainer;
        }

        public override string GetTitle()
        {
            return "ITEM DETAIL";
        }
    }
}
