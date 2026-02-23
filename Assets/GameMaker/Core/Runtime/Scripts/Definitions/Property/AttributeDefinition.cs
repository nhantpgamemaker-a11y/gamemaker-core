using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class AttributeDefinition : PropertyDefinition
    {
        [SerializeField]
        private string _defaultValue;
        public string DefaultValue { get => _defaultValue; set => _defaultValue = value; }
        public AttributeDefinition() : base()
        {
            
        }
        public AttributeDefinition(string id, string name, string title,string description, Sprite icon, BaseMetaData metaData,string defaultValue) : base(id, name, title,description, icon,metaData)
        {
            DefaultValue = defaultValue;
        }
        public override object Clone()
        {
            return new AttributeDefinition(GetID(), GetName(), GetTitle(), GetDescription(), GetIcon(),GetMetaData(), DefaultValue);
        }

        public override string GetStringValue()
        {
            return _defaultValue;
        }
#if UNITY_EDITOR
        public override string GetGenClassCode()
        {
            var sb = new System.Text.StringBuilder();
            string className = GetName().Replace(" ", "");
            sb.AppendLine($"public class {className}");
            sb.AppendLine("{");
            sb.AppendLine($"    public const string ID = \"{GetID()}\";");
            sb.AppendLine($"    public {this.GetType().Name} GetDefinition()");
            sb.AppendLine("     {");
            sb.AppendLine($"        return PropertyManager.Instance.GetDefinition(ID) as {this.GetType().Name};");
            sb.AppendLine("     }");
            sb.AppendLine();
            sb.AppendLine("     public PlayerAttribute GetPlayerAttribute()");
            sb.AppendLine("     {");
            sb.AppendLine($"        return PropertyGateway.Manager.GetPlayerProperty(\"{GetID()}\") as PlayerAttribute;");
            sb.AppendLine("     }");
            sb.AppendLine("}");
            sb.AppendLine();
            return sb.ToString();
        }
#endif
    }
}