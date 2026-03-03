using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class LocalTimedSaveData : BaseLocalData
    {
        [JsonProperty("PlayerTimeds")]
        private List<PlayerTimedModel> _playerTimeds;
        protected internal override void OnCreate()
        {
            base.OnCreate();
            _playerTimeds = new();
            var currentTime = TimeManager.Instance.UnixTimestamp;
            foreach(var timedDefinition in TimedManager.Instance.GetDefinitions())
            {
                var playerTimed = new PlayerTimedModel(timedDefinition.GetID(), timedDefinition.GetName(), timedDefinition.DefaultValue, currentTime);
                playerTimed.SetTimedDefinition(timedDefinition);
                _playerTimeds.Add(playerTimed);
            }
        }
        protected internal override void OnLoad()
        {
            base.OnLoad();
            var currentTime = TimeManager.Instance.UnixTimestamp;
            foreach(var timedDefinition in TimedManager.Instance.GetDefinitions())
            {
                var playerTimedModel = _playerTimeds.FirstOrDefault(x => x.GetID() == timedDefinition.GetID());
                if (playerTimedModel != null)                {
                    playerTimedModel.SetTimedDefinition(timedDefinition);
                    continue;
                }
                else
                {
                    var playerTimed = new PlayerTimedModel(timedDefinition.GetID(), timedDefinition.GetName(), timedDefinition.DefaultValue, currentTime);
                    playerTimed.SetTimedDefinition(timedDefinition);
                    _playerTimeds.Add(playerTimed);
                }
            }
        }
        public List<PlayerTimed> GetPlayerTimeds()
        {
            return _playerTimeds.Where(x=>x.GetTimedDefinition()!=null).Select(x => x.ToPlayerTimed()).ToList();
        }
        public PlayerTimed GetPlayerTimed(string refID)
        {
            return _playerTimeds.FirstOrDefault(x => x.GetID() == refID).ToPlayerTimed();
        }
        public async UniTask<PlayerTimed> AddTimedAsync(string refId, long amount, bool isSave = true)
        {
            var playerTimed = _playerTimeds.FirstOrDefault(x => x.GetID() == refId);
            playerTimed.AddTime(amount);
            if (isSave)
                await SaveAsync();
            return playerTimed.ToPlayerTimed();
        }
    }
    [System.Serializable]
    public class PlayerTimedModel: PlayerDataModel
    {
        [JsonProperty("Remain")]
        private long _remain;
        [JsonProperty("StartTime")]
        private long _startTime;
        public long Remain { get => _remain; }
        public long StartTime { get => _startTime; }
        [JsonIgnore]
        private TimedDefinition _timedDefinition;
        public PlayerTimedModel() : base()
        {

        }
        public PlayerTimedModel(string id, string name, long remain, long startTime) : base(id, name)
        {
            _remain = remain;
            _startTime = startTime;
        }
        public void AddTime(long amount)
        {
            var currentTime = TimeManager.Instance.UnixTimestamp;
            if(_startTime + _remain < currentTime)
            {
                _startTime = currentTime;
                _remain = amount;
            }
            else
            {
                _remain += amount;
            }
        }
        public PlayerTimed ToPlayerTimed()
        {
             var timedDefinition =  TimedManager.Instance.GetDefinition(id);
            return new PlayerTimed(id, timedDefinition, _remain, _startTime);
        }

        public void SetTimedDefinition(TimedDefinition timedDefinition)
        {
            _timedDefinition = timedDefinition;
        }

        public TimedDefinition GetTimedDefinition()
        {
            return _timedDefinition;
        }
    }
}