using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    public static class EditorClipboard
    {
        private static string _copiedJson;
        private static System.Type _copiedType;

        public static string CopiedJson { get => _copiedJson;}
        public static Type CopiedType { get => _copiedType;}

        public static void SetData(string data, System.Type type)
        {
            _copiedJson = data;
            _copiedType = type;
        }
       
    }
}