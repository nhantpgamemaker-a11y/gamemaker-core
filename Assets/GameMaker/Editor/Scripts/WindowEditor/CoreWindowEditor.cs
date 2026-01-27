using System;
using System.Collections.Generic;
using System.Linq;
using GameMaker.Core.Runtime;
using UnityEditor;
using UnityEngine;

namespace GameMaker.Core.Editor
{
    public class CoreWindowEditor : EditorWindow
    {
        private int _selectedTabIndex = 0;
        private List<ITabContentEditor> _baseTabContentEditors;
        private List<GUIContent> _gUIContents;
        [MenuItem("GameMaker/Setting")]
        public static CoreWindowEditor ShowExample()
        {
            CoreWindowEditor wnd = GetWindow<CoreWindowEditor>();
            wnd.titleContent = new GUIContent("GameMaker Setting");
            wnd.Init();
            wnd.Show();
            return wnd;
        }
        public void Init()
        {
            var baseTabContentEditorTypes = TypeUtils.GetAllDerivedNonAbstractTypes(typeof(ITabContentEditor));
            _baseTabContentEditors = new();
            _gUIContents = new();
            foreach (var baseTabContentEditorType in baseTabContentEditorTypes)
            {
                var baseTabContentEditor = Activator.CreateInstance(baseTabContentEditorType) as ITabContentEditor;
                _baseTabContentEditors.Add(baseTabContentEditor);
                _gUIContents.Add(new GUIContent(baseTabContentEditor.GetTitle(), baseTabContentEditor.GetTooltip()));
            }
            
            
        }
        #region  GUI Drawing
        void OnGUI()
        {
            
            EditorGUILayout.BeginVertical();
            {
                EditorGUILayout.BeginHorizontal();
                {
                    int index = GUILayout.Toolbar(_selectedTabIndex, _gUIContents.ToArray());
                    if (index != _selectedTabIndex)
                    {
                        _baseTabContentEditors[index].OnSelected();
                        _baseTabContentEditors[_selectedTabIndex].OnDeselected();
                        _selectedTabIndex = index;
                    }
                }
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space(10);
                _baseTabContentEditors[_selectedTabIndex].OnGUIDrawer();
            }
            EditorGUILayout.EndVertical();
           
        }
        #endregion
    }
}
