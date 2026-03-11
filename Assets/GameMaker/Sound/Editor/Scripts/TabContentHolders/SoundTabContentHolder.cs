using GameMaker.Core.Editor;
using GameMaker.Sound.Runtime;
using UnityEngine.UIElements;

namespace GameMaker.Sound.Editor
{
    [CoreTabContext]
    public class SoundTabContentHolder : BaseTabContentHolder
    {
        private TemplateContainer _templateContainer;
        private SoundDataManagerHolder _soundDataManagerHolder;

        public SoundTabContentHolder(VisualElement root) : base(root)
        {
            var asset = UIToolkitLoaderUtils.LoadUXML("DataManagerElement");
            _templateContainer = asset.CloneTree();
            _templateContainer.style.height = StyleKeyword.Auto;
            _templateContainer.style.flexGrow = 1;

            var serializedObject = new UnityEditor.SerializedObject(SoundManager.Instance);
            _soundDataManagerHolder = new SoundDataManagerHolder(_templateContainer);
            var serializedProperty = serializedObject.FindProperty("dataManager");
            _soundDataManagerHolder.Bind(serializedProperty);
        }

        public override int GetIndex()
        {
            return 100;
        }

        public override VisualElement GetTabView()
        {
            return _templateContainer;
        }

        public override string GetTitle()
        {
            return "SOUND";
        }
    }
}