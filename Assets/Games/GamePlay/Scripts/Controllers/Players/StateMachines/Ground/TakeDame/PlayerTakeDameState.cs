using System.Threading;
using Cysharp.Threading.Tasks;
using GameMaker.Core.Runtime;
using UnityEngine;

namespace Game.GamePlay
{
    public class PlayerTakeDameState : BasePlayerState
    {
         [UnityEngine.SerializeField]
        private ParticleSystem _bloodParticleSystem;
        [UnityEngine.SerializeField]
        public RenderedController _renderController;
        private CancellationTokenSource _cancellationTokenSource;
        private DameStateData _monsterStateData;
        public override PlayerStateType GetStateType()
        {
            return PlayerStateType.TakeDame;
        }
        public override void OnEnterState(BaseStateData baseStateData = null)
        {
            base.OnEnterState(baseStateData);
            StartAnimation(playerStateMachine.PlayerData.AnimationData.TakeDameAnimationHash);
            _monsterStateData = baseStateData as DameStateData;
            _ = TakeDame(_monsterStateData.Amount, _monsterStateData.Direction, _monsterStateData.Position);
        }
        public override void OnExitState()
        {
            base.OnExitState();
            StopAnimation(playerStateMachine.PlayerData.AnimationData.TakeDameAnimationHash);
        }
        public async UniTask TakeDame(float amount, Vector3 direction, Vector3 position)
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = new();
            _ = PlayBlood(direction, position);
            await _renderController.SetTakeDameAsync(_cancellationTokenSource.Token);
            //playerStateMachine.PlayerReusableData.HP -= amount;
            if (playerStateMachine.PlayerReusableData.GetControlDirection() != Vector2.zero)
            {
                playerStateMachine.ChangeState(PlayerStateType.Running);
            }
            else
            {
                playerStateMachine.ChangeState(PlayerStateType.Idling);
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
