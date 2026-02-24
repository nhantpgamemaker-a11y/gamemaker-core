using System.Collections.Generic;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class TimedActionData : BaseActionData
    {
        public const string ADD_TIMED_ACTION_DEFINITION = "ADD_TIMED_ACTION_DEFINITION";
        private string _timedId;
        private long _value;

        public string TimedId { get => _timedId;}
        public long Value { get => _value; }
        public TimedActionData() : base()
        {

        }
        public TimedActionData(string timedId, long value, IExtendData extendData) : base(extendData)
        {
            _timedId = timedId;
            _value = value;
        }
        public override List<ActionDefinition> GetGenerateActionDefinitions()
        {
            var actionParamManager = new BaseDefinitionManager<BaseActionParamDefinition>();
            actionParamManager.AddDefinition(new TimedActionParamDefinition("TimedID", "TimedID", "_timedId"));
            actionParamManager.AddDefinition(new LongActionParamDefinition("Value", "Value", "_value"));
            ActionDefinition actionDefinition = new(ADD_TIMED_ACTION_DEFINITION, ADD_TIMED_ACTION_DEFINITION, actionParamManager);
            return new List<ActionDefinition>() { actionDefinition };
        }
        public override bool Equals(BaseActionData other)
        {
            if (other is not TimedActionData o)
                return false;

            return _timedId == o._timedId
                && _value == o._value;
        }
    }
}