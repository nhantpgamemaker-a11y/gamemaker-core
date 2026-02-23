using GameMaker.Core.Runtime;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    [CoreTabContext]
    public class ActionTabContentHolder : BaseTabContentHolder
    {
        private TemplateContainer _templateContainer;
        private ActionDataManagerHolder _actionDataManagerHolder;
        public ActionTabContentHolder(VisualElement root) : base(root)
        {
            var asset = UIToolkitLoaderUtils.LoadUXML("DataManagerElement");
            _templateContainer = asset.CloneTree();
            _templateContainer.style.height = StyleKeyword.Auto;
            _templateContainer.style.flexGrow = 1;

            var serializedObject = new UnityEditor.SerializedObject(ActionManager.Instance);
            _actionDataManagerHolder = new ActionDataManagerHolder(_templateContainer);
            var serializedProperty = serializedObject.FindProperty("dataManager");
            _actionDataManagerHolder.Bind(serializedProperty);
        }

        public override int GetIndex()
        {
            return int.MaxValue-1;
        }

        public override VisualElement GetTabView()
        {
            return _templateContainer;
        }

        public override string GetTitle()
        {
            return "ACTION";
        }
    }
}
