using System;
using System.Collections.Generic;
using System.Linq;
using GameMaker.Core.Runtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    public abstract class BaseDataManagerHolder<M> : BaseHolder where M :IDefinition
    {
        protected SerializedProperty serializedProperty;
        protected SerializedProperty definitionProperty;
        protected List<SerializedProperty> items;
        private Button _addButton;
        private Button _removeButton;
        protected ListView itemListView;

        private SerializedProperty _copySerializedProperty;
        void OnUndoRedo()
        {
            serializedProperty.serializedObject.Update();
            MakeItemSource(GetItemSource());
            itemListView.Rebuild();
            itemListView.RefreshItems();
        }
        public BaseDataManagerHolder(VisualElement root) : base(root)
        {
            items = new List<SerializedProperty>();
            root.RegisterCallback<AttachToPanelEvent>(_ =>
            {
                Undo.undoRedoPerformed += OnUndoRedo;
            });

            root.RegisterCallback<DetachFromPanelEvent>(_ =>
            {
                Undo.undoRedoPerformed -= OnUndoRedo;
            });

            _addButton = Root.Q<Button>("AddButton");
            _removeButton = Root.Q<Button>("RemoveButton");
            _addButton.clicked += OnAddButtonClicked;
            _removeButton.clicked += OnRemoveButtonClicked;

            itemListView = Root.Q<ListView>("ItemListView");
            itemListView.selectionType = SelectionType.Multiple;
            itemListView.makeItem = MakeItem;
            itemListView.bindItem = BindItem;
            itemListView.itemIndexChanged += ItemIndexChanged;

            itemListView.AddManipulator(new ContextualMenuManipulator(menuBuilder =>
            {
                menuBuilder.menu.AppendAction("Copy", action =>
                {
                    if (itemListView.selectedIndices.Count() != 1)
                    {
                        ConfirmWindowEditor.ShowWindow("Warning",
                        "Please select one item to COPY to clipboard!",null, null);
                    }
                    else
                    {
                        definitionProperty.serializedObject.Update();
                        var index = itemListView.selectedIndices
                        .OrderByDescending(i => i).First();
                        var item = definitionProperty.GetArrayElementAtIndex(index);
                        var obj = item.managedReferenceValue;
                        if (obj != null)
                        {
                            var clone = System.Activator.CreateInstance(obj.GetType());
                            EditorClipboard.SetData(EditorJsonUtility.ToJson(obj),obj.GetType());            
                        }
                    }
                });

                menuBuilder.menu.AppendAction("Paste", action =>
                {
                    if (itemListView.selectedIndices.Count() != 1)
                    {
                        ConfirmWindowEditor.ShowWindow("Warning",
                        "Please select one item to PASTE from clipboard!", null, null);
                    }
                    else
                    {
                        var index = itemListView.selectedIndices
                        .OrderByDescending(i => i).First();
                        
                        var item = definitionProperty.GetArrayElementAtIndex(index);

                        if (!item.managedReferenceValue.GetType().IsAssignableFrom(EditorClipboard.CopiedType))
                        {
                            ConfirmWindowEditor.ShowWindow("Warning",
                                "This type of item is NOT EQUAL with type in clipboard!", null, null);
                            return;
                        }
                        definitionProperty.serializedObject.Update();
                        var instance = Activator.CreateInstance(EditorClipboard.CopiedType);

                        EditorJsonUtility.FromJsonOverwrite(
                            EditorClipboard.CopiedJson,
                            instance
                        );

                        item.managedReferenceValue = instance;
                        definitionProperty.serializedObject.ApplyModifiedProperties();
                    }
                    
                });

                menuBuilder.menu.AppendAction("Clone", action =>
                {
                    if (itemListView.selectedIndices.Count() != 1)
                    {
                        ConfirmWindowEditor.ShowWindow("Warning",
                        "Please select one item to CLONE!",null, null);
                    }
                    else
                    {
                        definitionProperty.serializedObject.Update();
                        var index = itemListView.selectedIndices
                        .OrderByDescending(i => i).First();
                        var item = definitionProperty.GetArrayElementAtIndex(index);
                        var obj = item.managedReferenceValue;
                        if (obj != null)
                        {
                            var instance = Activator.CreateInstance(obj.GetType());

                            EditorJsonUtility.FromJsonOverwrite(
                                EditorJsonUtility.ToJson(obj),
                                instance
                            );

                            definitionProperty.InsertArrayElementAtIndex(items.Count);
                            var insertItem = definitionProperty.GetArrayElementAtIndex(items.Count());
                            insertItem.managedReferenceValue = instance;
                            items.Add(insertItem);
                            itemListView.Rebuild();
                            itemListView.RefreshItems();
                            definitionProperty.serializedObject.ApplyModifiedProperties();           
                        }
                    }
                });
            }));
        }
        public override void Bind(SerializedProperty elementProperty)
        {
            base.Bind(elementProperty);
            serializedProperty = elementProperty;
            definitionProperty = elementProperty.FindPropertyRelative("_definitions");
            
            itemListView.itemsSource = items;
            MakeItemSource(GetItemSource());
            itemListView.Rebuild();
            itemListView.RefreshItems();
        }
        protected void MakeItemSource(List<SerializedProperty> itemSource)
        {
            items.Clear();
            items.AddRange(itemSource);
        }
        protected virtual List<SerializedProperty> GetItemSource()
        {
            var items = new List<SerializedProperty>();
            for (var i = 0; i < definitionProperty.arraySize; i++)
                items.Add(definitionProperty.GetArrayElementAtIndex(i));
            return items;
        }
        protected virtual void ItemIndexChanged(int oldIndex, int newIndex)
        {
            definitionProperty.MoveArrayElement(oldIndex, newIndex);
            definitionProperty.serializedObject.ApplyModifiedProperties();
        }
        protected abstract BaseHolder CreateHolder();
        protected virtual string GetTitle() => "Title";
        protected virtual VisualElement MakeItem()
        {
            var holder = CreateHolder();
            var root = holder.Root;
            root.userData = holder;
            return root;
        }
        protected virtual void BindItem(VisualElement element, int index)
        {
            var holder = (BaseHolder)element.userData;
            var itemProperty = items[index];
            holder.Bind(itemProperty);
        }
        protected virtual void OnRemoveButtonClicked()
        {
            var selectedIndices = itemListView.selectedIndices;
            if (selectedIndices.Count() == 0) return;
            ConfirmWindowEditor.ShowWindow("Delete Items",
            "Are you sure you want to delete there item?",
            () =>
            {
                var indices = selectedIndices
                .OrderByDescending(i => i)
                .ToList();
                foreach (var index in indices)
                {
                    definitionProperty.DeleteArrayElementAtIndex(index);
                }

                serializedProperty.serializedObject.ApplyModifiedProperties();
                MakeItemSource(GetItemSource());
                itemListView.RefreshItems();
            },
            () =>
            {

            });
        }
        protected virtual void OnAddButtonClicked()
        {
            var inheritanceClassTypes = new List<Type>();
            var definitionType = typeof(M);
            if (definitionType.IsAbstract)
                inheritanceClassTypes = TypeUtils.GetAllDerivedNonAbstractTypes(typeof(M)).ToList();
            else
            {
                inheritanceClassTypes.Add(typeof(M));
            }
            List<ActionData> actionDatas = new();
            EditorWindow window = null;
            foreach(var type in inheritanceClassTypes)
            {
                var actionData = new ActionData(type.Name, () =>
                {
                    var item = (M)Activator.CreateInstance(type);
                    int newIndex = definitionProperty.arraySize;
                    definitionProperty.InsertArrayElementAtIndex(newIndex);
                    var newElement = definitionProperty.GetArrayElementAtIndex(newIndex);
                    newElement.managedReferenceValue = item;
                    MakeItemSource(GetItemSource());
                    definitionProperty.serializedObject.ApplyModifiedProperties();
                    definitionProperty.serializedObject.Update();
                    itemListView.RefreshItems();
                });
                actionDatas.Add(actionData);
            }
            window = ButtonActionWindowEditor.ShowWindow(GetTitle(),actionDatas);
        }
    }
}
