using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Game.GamePlay;
using GameMaker.Core.Runtime;

namespace Game.GamePlay
{
    public class TakeDameMonsterRootState : BaseMonsterRootState
    {
        public RenderedController _renderController;
        private CancellationTokenSource _cancellationTokenSource;
        public override MonsterRootStateType GetStateType()
        {
            return MonsterRootStateType.TakeDame;
        }
        public override void OnEnterState(BaseStateData baseStateData = null)
        {
            base.OnEnterState(baseStateData);
            _ = TakeDame();
        }
        public async UniTask TakeDame()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = new();
            await _renderController.SetTakeDameAsync(_cancellationTokenSource.Token);
            monsterRootStateMachine.ChangeState(MonsterRootStateType.Idling);
        }
    }
}