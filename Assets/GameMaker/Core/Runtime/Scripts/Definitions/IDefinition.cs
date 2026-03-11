using System;

namespace GameMaker.Core.Runtime
{
    public interface IDefinition: IEquatable<IDefinition>
    {
        public string GetID();

        public string GetName();
    }
}
