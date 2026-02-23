
using System;

namespace CatAdventure.GamePlay
{
    [System.Serializable]
    public abstract class GameMechanicData:ICloneable
    {
        public abstract object Clone();
    }
}