using GameMaker.Core.Runtime;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    [CoreTabContext]
    public class BundleTabContentHolder : BaseTabContentHolder
    {
        private TemplateContainer _templateContainer;
        private BundleDataManagerHolder _bundleDataManagerHolder;
        public BundleTabContentHolder(VisualElement root) : base(root)
        {
             var asset = UIToolkitLoaderUtils.LoadUXML("DataManagerElement");
            _templateContainer = asset.CloneTree();
            _templateContainer.style.height = StyleKeyword.Auto;
            _templateContainer.style.flexGrow = 1;

            var serializedObject = new UnityEditor.SerializedObject(BundleManager.Instance);
            _bundleDataManagerHolder = new BundleDataManagerHolder(_templateContainer);
            var serializedProperty = serializedObject.FindProperty("dataManager");
            _bundleDataManagerHolder.Bind(serializedProperty);
        }

        public override int GetIndex()
        {
            return int.MaxValue - 10;
        }

        public override VisualElement GetTabView()
        {
           return _templateContainer;
        }

        public override string GetTitle()
        {
            return "BUNDLE";
        }
    }
}