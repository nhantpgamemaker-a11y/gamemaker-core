using GameMaker.Core.Runtime;
using UnityEngine;

namespace CatAdventure.GamePlay
{
    public class Test1 :  PropertyDefinition
    {
        public Test1() : base()
        {

        }

        public Test1(string id, string name, string title, string description, Sprite icon, BaseMetaData metaData) : base(id, name, title, description, icon, metaData)
        {
        }

        public override object Clone()
        {
            throw new System.NotImplementedException();
        }

        public override string GetGenClassCode()
        {
            throw new System.NotImplementedException();
        }

        public override string GetStringValue()
        {
            return "Test";
        }
    }
    public class Tes2: PropertyDefinition
    {
        public Tes2() : base()
        {

        }

        public Tes2(string id, string name, string title, string description, Sprite icon, BaseMetaData metaData) : base(id, name, title, description, icon, metaData)
        {
        }

        public override object Clone()
        {
            throw new System.NotImplementedException();
        }

        public override string GetGenClassCode()
        {
            throw new System.NotImplementedException();
        }

        public override string GetStringValue()
        {
            return "Test2";
        }
    }
}
