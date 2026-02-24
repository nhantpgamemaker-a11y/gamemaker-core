using UnityEditor;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    [GameMaker.Core.Runtime.TypeCache]
    public abstract class BaseRewardDefinitionHolder : BaseDefinitionHolder
    {
        private Foldout _rewardFoldout;
        private TextField _nameField;
        protected TemplateContainer templateContainer;
        public BaseRewardDefinitionHolder(VisualElement root) : base(root)
        {
            var asset = GetVisualTreeAsset();
            templateContainer = asset.CloneTree();
            root.Add(templateContainer);
        }
        public abstract VisualTreeAsset GetVisualTreeAsset();

        public override void Bind(SerializedProperty elementProperty)
        {
            _nameField = Root.Q<TextField>("NameTextField");
            _rewardFoldout = Root.Q<Foldout>("RewardFoldout");
            
            base.Bind(elementProperty);
            _nameField.RegisterValueChangedCallback(value =>
            {
                UpdatePropertyFoldout();
            });
            UpdatePropertyFoldout(); 
        }
        public virtual string GetNameFoldout()
        {
            return $"{serializedProperty.FindPropertyRelative("_name").stringValue}";
        }
        public void UpdatePropertyFoldout()
        {
            _rewardFoldout.text = GetNameFoldout();
        }
    }
}
