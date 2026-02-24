using GameMaker.Core.Editor;
using GameMaker.Core.Runtime;
using GameMaker.IAP.Runtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;


namespace GameMaker.IAP.Editor
{
    [CustomPropertyDrawer(typeof(BaseDefinitionManager<IAPDefinition>))]
    public class IAPDataManagerPropertyDrawer : Core.Editor.BaseDataManagerPropertyDrawer<IAPDefinition>
    {
        protected override BaseDataManagerHolder<IAPDefinition> CreateBaseDataManagerHolder()
        {
            var asset = UIToolkitLoaderUtils.LoadUXML("DataManagerElement");
            var templateContainer = asset.CloneTree();
            templateContainer.style.height = StyleKeyword.Auto;
            templateContainer.style.flexGrow = 1;
            return new IAPDataManagerHolder(templateContainer);
        }
    }
    public class IAPDataManagerHolder : BaseDataManagerHolder<IAPDefinition>
    {
        public IAPDataManagerHolder(VisualElement root) : base(root)
        {
        }
        protected override string GetTitle()
        {
            return "IAP";
        }

        protected override BaseHolder CreateHolder()
        {
            var asset = Resources.Load<VisualTreeAsset>(
            "IAPDefinitionElement");
            var templateContainer = asset.CloneTree();
            templateContainer.style.height = StyleKeyword.Auto;
            templateContainer.style.flexGrow = 1;
            return new IAPDefinitionHolder(asset.CloneTree());
        }
    }
}
