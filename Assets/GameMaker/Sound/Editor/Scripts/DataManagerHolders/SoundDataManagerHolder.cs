using GameMaker.Core.Editor;
using GameMaker.Core.Runtime;
using GameMaker.Sound.Runtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameMaker.Sound.Editor
{
    [CustomPropertyDrawer(typeof(BaseDefinitionManager<SoundDefinition>))]
    public class SoundDataManagerPropertyHolder : Core.Editor.BaseDataManagerPropertyDrawer<SoundDefinition>
    {

        protected override BaseDataManagerHolder<SoundDefinition> CreateBaseDataManagerHolder()
        {
            var asset = UIToolkitLoaderUtils.LoadUXML("DataManagerElement");
            var templateContainer = asset.CloneTree();
            templateContainer.style.height = StyleKeyword.Auto;
            templateContainer.style.flexGrow = 1;
            return new SoundDataManagerHolder(templateContainer);
        }
    }
    public class SoundDataManagerHolder : BaseDataManagerHolder<SoundDefinition>
    {
        public SoundDataManagerHolder(VisualElement root) : base(root)
        {
        }
        protected override string GetTitle()
        {
            return "Sound";
        }

        protected override BaseHolder CreateHolder()
        {
            var asset = Resources.Load<VisualTreeAsset>(
            "SoundDefinitionElement");
            var templateContainer = asset.CloneTree();
            templateContainer.style.height = StyleKeyword.Auto;
            templateContainer.style.flexGrow = 1;
            return new SoundDefinitionHolder(asset.CloneTree());
        }
    }
}