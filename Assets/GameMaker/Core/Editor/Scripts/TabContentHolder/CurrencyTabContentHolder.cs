using GameMaker.Core.Runtime;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    [CoreTabContext]
    public class CurrencyTabContentHolder : BaseTabContentHolder
    {
        private TemplateContainer _templateContainer;
        private CurrencyDataManagerHolder _currencyDataManagerHolder;
        public CurrencyTabContentHolder(VisualElement root) : base(root)
        {
            var asset = UIToolkitLoaderUtils.LoadUXML("DataManagerElement");
            _templateContainer = asset.CloneTree();
            _templateContainer.style.height = StyleKeyword.Auto;
            _templateContainer.style.flexGrow = 1;

            var serializedObject = new UnityEditor.SerializedObject(CurrencyManager.Instance);
            _currencyDataManagerHolder = new CurrencyDataManagerHolder(_templateContainer);
            var serializedProperty = serializedObject.FindProperty("dataManager");
            _currencyDataManagerHolder.Bind(serializedProperty);
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
            return "CURRENCY";
        }
    }
}
