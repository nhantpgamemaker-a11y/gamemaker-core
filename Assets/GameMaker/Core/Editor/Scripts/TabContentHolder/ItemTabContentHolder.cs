using GameMaker.Core.Runtime;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    [CoreTabContext]
    public class ItemTabContentHolder : BaseTabContentHolder
    {
        private TemplateContainer _templateContainer;
        private ItemDataManagerHolder _itemDataManagerHolder;
        public ItemTabContentHolder(VisualElement root) : base(root)
        {
            var asset = UIToolkitLoaderUtils.LoadUXML("DataManagerElement");
            _templateContainer = asset.CloneTree();
            _templateContainer.style.height = StyleKeyword.Auto;
            _templateContainer.style.flexGrow = 1;

            var serializedObject = new UnityEditor.SerializedObject(ItemManager.Instance);
            _itemDataManagerHolder = new ItemDataManagerHolder(_templateContainer);
            var serializedProperty = serializedObject.FindProperty("dataManager");
            _itemDataManagerHolder.Bind(serializedProperty);
        }

        public override int GetIndex()
        {
            return 3;
        }

        public override VisualElement GetTabView()
        {
            return _templateContainer;
        }

        public override string GetTitle()
        {
            return "ITEM";
        }
    }
}
