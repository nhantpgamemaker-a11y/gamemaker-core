using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    [Serializable]
    public class ActionData
    {
        protected string title;
        protected Action action;

        public string Title { get => title; set => title = value; }
        public Action Action { get => action; set => action = value; }

        public ActionData(string title, Action action)
        {
            this.title = title;
            this.action = action;
        }
    }
    public class BaseActionHolder : BaseHolder
    {
        private Label _titleLabel;
        private ListView _listView;

        List<ActionData> _actionDatas;
        public BaseActionHolder(VisualElement root, string title,
        List<ActionData> actionDatas) : base(root)
        {
            _titleLabel = root.Q<Label>("TitleLabel");
            _listView = root.Q<ListView>("ItemListView");
            _titleLabel.text = title;
            _actionDatas = actionDatas;
            _listView.itemsSource = _actionDatas;
            _listView.showFoldoutHeader = false;
            _listView.makeItem = MakeActionItem;
            _listView.bindItem = BindActionItem;
            _listView.RefreshItems();
            _listView.Rebuild();
        }

        private void BindActionItem(VisualElement element, int index)
        {
            var holder = element.userData as BaseActionItemHolder;
            holder.Bind(_actionDatas[index]);
        }

        private VisualElement MakeActionItem()
        {
            var holder = CreateActionItemVisualElementHolder();
            return holder.Root;
        }

        public override void Bind(SerializedProperty elementProperty)
        {
            base.Bind(elementProperty);
        }

        protected virtual BaseActionItemHolder CreateActionItemVisualElementHolder()
        {
            var asset = UIToolkitLoaderUtils.LoadUXML("BaseActionItemElement");
            var root = asset.CloneTree();
            var holder = new BaseActionItemHolder(root);
            root.userData = holder;
            return holder;
        }
    }
}