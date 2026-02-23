using GameMaker.Core.Runtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    [CustomPropertyDrawer(typeof(BaseDefinitionManager<BundleDefinition>))]
    public class BundleDataManagerPropertyDrawer : Core.Editor.BaseDataManagerPropertyDrawer<BundleDefinition>
    {
        protected override BaseDataManagerHolder<BundleDefinition> CreateBaseDataManagerHolder()
        {
            var asset = UIToolkitLoaderUtils.LoadUXML("DataManagerElement");
            var templateContainer = asset.CloneTree();
            templateContainer.style.height = StyleKeyword.Auto;
            templateContainer.style.flexGrow = 1;
            return new BundleDataManagerHolder(templateContainer);
        }
    }
    public class BundleDataManagerHolder : BaseDataManagerHolder<BundleDefinition>
    {
        public BundleDataManagerHolder(VisualElement root) : base(root)
        {

        }
        protected override string GetTitle()
        {
            return "Bundle";
        }

        protected override BaseHolder CreateHolder()
        {
            var asset = Resources.Load<VisualTreeAsset>(
            "BundleDefinitionElement");
            var templateContainer = asset.CloneTree();
            templateContainer.style.height = StyleKeyword.Auto;
            templateContainer.style.flexGrow = 1;
            return new BundleDefinitionHolder(asset.CloneTree());
        }
    }
}
