using Cysharp.Threading.Tasks;
using LitMotion.Animation;
using UnityEngine;

namespace GameMaker.UI.Runtime
{
    public class LMotionUIAnimation : BaseUIAnimation
    {
        [SerializeField] protected LitMotionAnimation showAnimation;
        [SerializeField] protected LitMotionAnimation hideAnimation;

        public override async UniTask ShowAsync()
        {
            hideAnimation?.Stop();
            showAnimation?.Play();
            await UniTask.WaitUntil(() => !showAnimation.IsPlaying);
        }
        public override async UniTask HideAsync()
        {
            showAnimation?.Stop();
            hideAnimation?.Play();
            await UniTask.WaitUntil(() => !hideAnimation.IsPlaying);
        }
    }
}