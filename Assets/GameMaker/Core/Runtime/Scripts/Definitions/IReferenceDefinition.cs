using UnityEngine;

namespace GameMaker.Core.Runtime
{
    public interface IReferenceDefinition
    {
        public string GetReferenceID();
        public IDefinition GetDefinition();
    }
}
