using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class ScriptableObjectSingletonPathAttribute : System.Attribute
    {
        public string Path { get; private set; }
        
        public ScriptableObjectSingletonPathAttribute(string path)
        {
            Path = path;
        }
    }
}