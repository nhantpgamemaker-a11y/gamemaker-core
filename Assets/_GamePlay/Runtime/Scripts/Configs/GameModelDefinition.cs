using GameMaker.Core.Runtime;
using UnityEngine;

namespace CatAdventure.GamePlay
{
    public class GameModelDefinition : MonoBehaviour, IDefinition
    {
        public string GetID()
        {
            return this.gameObject.name;
        }
        public string GetName()
        {
            return this.gameObject.name;
        }

        public bool Equals(IDefinition other)
        {
            return other.GetID() == GetID();
        }
    }
}
