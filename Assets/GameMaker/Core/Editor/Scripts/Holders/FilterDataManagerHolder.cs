using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameMaker.Core.Runtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    public abstract class FilterDataManagerHolder<M> : BaseDataManagerHolder<M> where M : IDefinition
    {
        protected List<BaseFilterHolder> filters;
        private VisualElement _filterContainerVisualElement;
        public FilterDataManagerHolder(VisualElement root, List<BaseFilterHolder> filters) : base(root)
        {
            _filterContainerVisualElement = root.Q<VisualElement>("FilterContainer");
            this.filters = filters;
            foreach (var filter in this.filters)
            {
                _filterContainerVisualElement.Add(filter.Root);
                filter.BaseFilter.OnValueChanged += OnValueChanged;
            }
        }
        ~FilterDataManagerHolder()
        {
            foreach (var filter in filters)
            {
                filter.BaseFilter.OnValueChanged -= OnValueChanged;
            }
        }

        protected void OnValueChanged()
        {
            MakeItemSource(GetItemSource());
            itemListView.Rebuild();
            itemListView.RefreshItems();
        }

        public override void Bind(SerializedProperty elementProperty)
        {
            this.definitionProperty = elementProperty.FindPropertyRelative("definitions");
            
            base.Bind(elementProperty);
            foreach(var filter in filters)
            {
                filter.Bind(elementProperty);
            }
        }
        protected override List<SerializedProperty> GetItemSource()
        {
            var sourceItems = new List<SerializedProperty>();
            for (int i = 0; i < definitionProperty.arraySize; i++)
            {
                var element = definitionProperty.GetArrayElementAtIndex(i);
                if(filters.All(f => f.BaseFilter.Compare(element)))
                {
                    sourceItems.Add(element);
                }
            }
            return sourceItems;
        }
    }
    public abstract class BaseFilterHolder : BaseHolder
    {
        private Toggle _activeToggle;
        private Label _titleLabel;
        protected BaseFilter baseFilter;

        public BaseFilter BaseFilter => baseFilter;
        public BaseFilterHolder(VisualElement root, BaseFilter baseFilter) : base(root)
        {
            _activeToggle = root.Q<Toggle>("ActiveToggle");
            _titleLabel = root.Q<Label>("TitleLabel");
            this.baseFilter = baseFilter;
        }

        public override void Bind(SerializedProperty elementProperty)
        {
            base.Bind(elementProperty);
            _titleLabel.text = baseFilter.Title;
            _activeToggle.RegisterValueChangedCallback((status) =>
            {
                baseFilter.Active = status.newValue;
                baseFilter.OnValueChanged?.Invoke();
            });
            _activeToggle.value = baseFilter.Active;
        }
    }
    public class FloatFilterHolder : BaseFilterHolder
    {
        private FloatField _valueFloatField;
        private DropdownField _compareDropdownField;
        private FloatFilter _floatFilter;
        private List<CompareType> _compareTypes;
        public FloatFilterHolder(VisualElement root,BaseFilter baseFilter) : base(root,baseFilter)
        {
            _valueFloatField = root.Q<FloatField>("ValueFloatField");
            _compareDropdownField = root.Q<DropdownField>("CompareDropdownField");
            CompareType[] values = (CompareType[])Enum.GetValues(typeof(CompareType));
            _compareTypes = values.ToList();
            _compareDropdownField.dataSource = _compareTypes;
        }
       public override void Bind(SerializedProperty elementProperty)
        {
            base.Bind(elementProperty);
            _floatFilter = baseFilter as FloatFilter;

            _valueFloatField.value = _floatFilter.Value;
            _valueFloatField.RegisterValueChangedCallback((value) =>
            {
                _floatFilter.Value = value.newValue;
                _floatFilter.OnValueChanged?.Invoke();
            });
            _compareDropdownField.value = _compareTypes[0].ToString();
            _compareDropdownField.RegisterValueChangedCallback(value =>
            {
                var compareType = (CompareType)Enum.Parse(typeof(CompareType), value.newValue);
                _floatFilter.CompareType = compareType;
                _floatFilter.OnValueChanged?.Invoke();
            });
        }
    }
    public class StringFilterHolder : BaseFilterHolder
    {
        private TextField _valueTextField;
        private StringFilter _stringFilter;
        public StringFilterHolder(VisualElement root,BaseFilter baseFilter) : base(root,baseFilter)
        {
            _valueTextField = root.Q<TextField>("ValueTextField");
        }
        public override void Bind(SerializedProperty elementProperty)
        {
            base.Bind(elementProperty);
            _stringFilter = baseFilter as StringFilter;
            _stringFilter.Value = _valueTextField.text;
            _valueTextField.RegisterValueChangedCallback(value =>
            {
                _stringFilter.Value = value.newValue;
                _stringFilter.OnValueChanged?.Invoke();
            });
        }
    }
    public abstract class BaseFilter
    {
        private string _title;
        private string _binding;
        private bool _active = false;
        public string Title => _title;
        public string Binding => _binding;
        public bool Active { get => _active; set => _active = value; }
        public Action OnValueChanged;
        protected BaseFilterHolder baseFilterHolder;
        public BaseFilterHolder BaseFilterHolder => baseFilterHolder;
        public BaseFilter(string title, string binding)
        {
            _title = title;
            _binding = binding;
        }
        public abstract bool Compare(SerializedProperty serializedProperty);
    }
    public class FloatFilter : BaseFilter
    {
        private CompareType _compareType;
        private float _value;
        public float Value { get => _value; set => _value = value; }
        public CompareType CompareType { get => _compareType;  set { _compareType = value; }}
        public FloatFilter(string title, string binding) : base(title, binding)
        {
        }

        public override bool Compare(SerializedProperty  serializedProperty)
        {
            var prop = serializedProperty.FindPropertyRelative(Binding);
            if (prop.propertyType != SerializedPropertyType.Float) return true;
            float value = prop.floatValue;
            switch (_compareType)
            {
                case CompareType.Less:
                    return _value < value;
                case CompareType.LessEqual:
                    return _value <= value;
                case CompareType.Equal:
                    return _value == value;
                case CompareType.Great:
                    return _value > value;
                case CompareType.GreatEqual:
                    return _value >= value;
                default:
                    return true;
            }
        }
    }
    public class StringFilter: BaseFilter
    {
        private string _value;
        public string Value { get => _value; set => _value = value; }

        public StringFilter(string title, string binding) : base(title, binding)
        {
        }

        public override bool Compare(SerializedProperty serializedProperty)
        {
            var prop = serializedProperty.FindPropertyRelative(Binding);
            if (prop.propertyType != SerializedPropertyType.String) return true;
            var value = prop.stringValue;
            if (string.IsNullOrEmpty(value)) return true;
            if (string.IsNullOrEmpty(_value))
                return true;
            return value.Contains(_value);
        }
    }
    public enum CompareType
    {
        None,
        Less,
        LessEqual,
        Equal,
        GreatEqual,
        Great
    }
}
