using GameMaker.Core.Editor;
using GameMaker.IAP.Runtime;
using UnityEngine.UIElements;

namespace GameMaker.IAP.Editor
{
    [FeatureTabContent]
    public class IAPTabContentHolder : BaseFeatureTabContentHolder
    {
        private TemplateContainer _templateContainer;
        private IAPDataManagerHolder _iapDataManagerHolder;
        public IAPTabContentHolder(VisualElement root) : base(root)
        {
            var asset = UIToolkitLoaderUtils.LoadUXML("DataManagerElement");
            _templateContainer = asset.CloneTree();
            _templateContainer.style.height = StyleKeyword.Auto;
            _templateContainer.style.flexGrow = 1;

            var serializedObject = new UnityEditor.SerializedObject(IAPManager.Instance);
            _iapDataManagerHolder = new IAPDataManagerHolder(_templateContainer);
            var serializedProperty = serializedObject.FindProperty("dataManager");
            _iapDataManagerHolder.Bind(serializedProperty);
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
            return "IAP";
        }
    }
}