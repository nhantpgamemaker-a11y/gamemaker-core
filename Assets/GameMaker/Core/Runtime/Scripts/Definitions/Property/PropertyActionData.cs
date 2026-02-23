using System;
using System.Collections.Generic;
using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class PropertyActionData : BaseActionData
    {
        public const string SET_PROPERTY_ACTION_DEFINITION = "SET_PROPERTY_ACTION_DEFINITION";
        private string _propertyId;
        private string _value;
        public string Value { get => _value; }
        public string PropertyId { get => _propertyId; }
        public  PropertyActionData():base(){}
        public PropertyActionData(string propertyId,string value ,IExtendData extendData) : base(extendData)
        {
            _value = value;
            _propertyId = propertyId;
        }
        public override bool Equals(BaseActionData other)
        {
            if (other == null)
                return false;

            if (GetType() != other.GetType())
                return false;

            if (other is not PropertyActionData o)
                return false;

            return _propertyId == o._propertyId;
        }
        public override List<ActionDefinition> GetGenerateActionDefinitions()
        {
            List<ActionDefinition> actionDefinitions = new();
            var actionParamManager = new BaseDefinitionManager<BaseActionParamDefinition>();

            actionParamManager = new BaseDefinitionManager<BaseActionParamDefinition>();
            actionParamManager.AddDefinition(new StatActionParamDefinition("PropertyID", "PropertyID", "_propertyId"));
            actionParamManager.AddDefinition(new FloatActionParamDefinition("Value", "Value", "_value"));
            ActionDefinition actionDefinition = new(SET_PROPERTY_ACTION_DEFINITION, SET_PROPERTY_ACTION_DEFINITION, actionParamManager);
            actionDefinitions.Add(actionDefinition);

            return actionDefinitions;
        }
    }
}