using GameMaker.Core.Editor;
using GameMaker.Feature.Shop.Runtime;
using UnityEngine.UIElements;

namespace GameMaker.Feature.Shop.Editor
{
    [FeatureTabContent]
    public class ShopTabContentHolder : BaseFeatureTabContentHolder
    {
        private TemplateContainer _templateContainer;
        private ShopDataManagerHolder _shopDataManagerHolder;
        public ShopTabContentHolder(VisualElement root) : base(root)
        {
             var asset = UIToolkitLoaderUtils.LoadUXML("DataManagerElement");
            _templateContainer = asset.CloneTree();
            _templateContainer.style.height = StyleKeyword.Auto;
            _templateContainer.style.flexGrow = 1;

            var serializedObject = new UnityEditor.SerializedObject(ShopManager.Instance);
            _shopDataManagerHolder = new ShopDataManagerHolder(_templateContainer);
            var serializedProperty = serializedObject.FindProperty("dataManager");
            _shopDataManagerHolder.Bind(serializedProperty);
        }

        public override int GetIndex()
        {
            return 1;
        }

        public override VisualElement GetTabView()
        {
            return _templateContainer;
        }

        public override string GetTitle()
        {
            return "SHOP";
        }
    }
}