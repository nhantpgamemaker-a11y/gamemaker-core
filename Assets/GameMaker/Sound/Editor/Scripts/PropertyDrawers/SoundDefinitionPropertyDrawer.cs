using GameMaker.Core.Editor;
using GameMaker.Sound.Runtime;
using UnityEditor;
using UnityEngine.UIElements;

namespace GameMaker.Sound.Editor
{
    [CustomPropertyDrawer(typeof(SoundDefinition))]
    public class SoundDefinitionPropertyDrawer : BaseDefinitionPropertyDrawer
    {
        protected override BaseDefinitionHolder GetBaseDefinitionHolder()
        {
           var asset = UIToolkitLoaderUtils.LoadUXML("SoundDefinitionElement");
            TemplateContainer templateContainer = asset.CloneTree();
            return new SoundDefinitionHolder(templateContainer);
        }
    }
}