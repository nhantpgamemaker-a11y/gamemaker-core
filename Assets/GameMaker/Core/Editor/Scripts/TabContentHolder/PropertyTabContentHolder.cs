using GameMaker.Core.Runtime;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    [CoreTabContext]
    public class PropertyTabContentHolder : BaseTabContentHolder
    {
        private TemplateContainer _templateContainer;
        private PropertyDataManagerHolder _propertyDataManagerHolder;
        public PropertyTabContentHolder(VisualElement root) : base(root)
        {
            var asset = UIToolkitLoaderUtils.LoadUXML("DataManagerElement");
            _templateContainer = asset.CloneTree();
            _templateContainer.style.height = StyleKeyword.Auto;
            _templateContainer.style.flexGrow = 1;

            var serializedObject = new UnityEditor.SerializedObject(PropertyManager.Instance);
            _propertyDataManagerHolder = new PropertyDataManagerHolder(_templateContainer);
            var serializedProperty = serializedObject.FindProperty("dataManager");
            _propertyDataManagerHolder.Bind(serializedProperty);
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
            return "PROPERTY";
        }
    }
}
