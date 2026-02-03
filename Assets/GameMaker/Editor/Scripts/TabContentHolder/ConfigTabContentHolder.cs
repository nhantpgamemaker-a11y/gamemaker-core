using GameMaker.Core.Runtime;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    [CoreTabContext]
    public class ConfigTabContentHolder : BaseTabContentHolder
    {
        private TemplateContainer _templateContainer;
        private ConfigDataManagerHolder _itemDataManagerHolder;
        public ConfigTabContentHolder(VisualElement root) : base(root)
        {
            var asset = UIToolkitLoaderUtils.LoadUXML("DataManagerElement");
            _templateContainer = asset.CloneTree();
            _templateContainer.style.height = StyleKeyword.Auto;
            _templateContainer.style.flexGrow = 1;

            var serializedObject = new UnityEditor.SerializedObject(ConfigManager.Instance);
            _itemDataManagerHolder = new ConfigDataManagerHolder(_templateContainer);
            var serializedProperty = serializedObject.FindProperty("dataManager");
            _itemDataManagerHolder.Bind(serializedProperty);
        }

        public override int GetIndex()
        {
            return 0;
        }

        public override VisualElement GetTabView()
        {
            return _templateContainer;
        }

        public override string GetTitle()
        {
            return "CONFIG";
        }
    }
}
