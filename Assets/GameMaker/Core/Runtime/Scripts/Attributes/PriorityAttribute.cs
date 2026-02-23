using System;

namespace GameMaker.Core.Runtime
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PriorityAttribute : Attribute
    {
        public int Value { get; }
        public PriorityAttribute(int value)
        {
            Value = value;
        }
    }
}