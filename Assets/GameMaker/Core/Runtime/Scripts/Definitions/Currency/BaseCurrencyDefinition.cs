using System.Threading;
using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    [TypeCache]
    public abstract class BaseCurrencyDefinition: BaseDefinition
    {
        public BaseCurrencyDefinition() : base()
        {
            
        }
        public BaseCurrencyDefinition(
        string id,
        string name,
        string title,
        string description,
        Sprite icon,
        BaseMetaData metaData) :
        base(id, name, title, description, icon, metaData)
        {
        }
        public abstract object GetDefaultValue();
        
#if UNITY_EDITOR
        public virtual string GetGenClassCode()
        {
            var sb = new System.Text.StringBuilder();
            string className = GetName().Replace(" ", "");
            sb.AppendLine($"public class {className}");
            sb.AppendLine("{");
            sb.AppendLine($"    public const string ID = \"{GetID()}\";");
            sb.AppendLine($"    public {this.GetType().Name} GetDefinition()");
            sb.AppendLine("     {");
            sb.AppendLine($"        return CurrencyManager.Instance.GetDefinition(ID) as {this.GetType().Name};");
            sb.AppendLine("     }");
            sb.AppendLine();
            sb.AppendLine("     public BasePlayerCurrency GetPlayerCurrency()");
            sb.AppendLine("     {");
            sb.AppendLine($"        return CurrencyGateway.Manager.GetPlayerCurrency(\"{GetID()}\");");
            sb.AppendLine("     }");
            sb.AppendLine("}");
            sb.AppendLine();
            return sb.ToString();
        }
#endif
    }
}
