using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Game.GamePlay;
using GameMaker.Core.Runtime;
using UnityEngine;

namespace Game.GamePlay
{
    [System.Serializable]
    public class DameStateData:BaseStateData
    {
        private float _amount;
        private Vector3 _direction;
        private Vector3 _position;
        public DameStateData(float amount,Vector3 direction, Vector3 position) : base()
        {
            _amount = amount;
            _direction = direction;
            _position = position;
        }
        public float Amount => _amount;
        public Vector3 Direction => _direction;
        public Vector3 Position => _position;
    }
    public class TakeDameMonsterRootState : BaseMonsterRootState
    {
        [UnityEngine.SerializeField]
        private ParticleSystem _bloodParticleSystem;
        public RenderedController _renderController;
        private CancellationTokenSource _cancellationTokenSource;
        private DameStateData _monsterStateData;
        public override MonsterRootStateType GetStateType()
        {
            return MonsterRootStateType.TakeDame;
        }
        public override void OnEnterState(BaseStateData baseStateData = null)
        {
            base.OnEnterState(baseStateData);
            _monsterStateData = baseStateData as DameStateData;
            _ = TakeDame(_monsterStateData.Amount, _monsterStateData.Direction, _monsterStateData.Position);
        }
        public async UniTask TakeDame(float amount, Vector3 direction, Vector3 position)
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = new();
            _ = PlayBlood(direction, position);
            await _renderController.SetTakeDameAsync(_cancellationTokenSource.Token);
            monsterRootStateMachine.MonsterReusableData.Hp -= amount;
            if (monsterRootStateMachine.MonsterReusableData.Hp <= 0)
            {
                Destroy(this.gameObject);
            }
            else
            {
                monsterRootStateMachine.ChangeState(MonsterRootStateType.Idling);
            }

        }
        public async UniTask PlayBlood(Vector3 direction, Vector3 position)
        {
            var rotation = Quaternion.LookRotation(direction);
            var pa = Instantiate(_bloodParticleSystem, position, rotation);
            pa.Play();
            Destroy(pa.gameObject,pa.main.duration);
        }
    }
}