using System;

namespace GameMaker.Core.Runtime
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DataSpaceAttribute: Attribute
    {
        public const string INIT_ANY = "";
        private string _name;
        public string Name => _name;
        public DataSpaceAttribute(string name): base()
        {
            _name = name;
        }
    }
}