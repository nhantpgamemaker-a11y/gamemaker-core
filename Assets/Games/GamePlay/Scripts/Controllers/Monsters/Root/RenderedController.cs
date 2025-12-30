using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using LitMotion;
using UnityEngine;

namespace Game.GamePlay
{
    public class RenderedController : MonoBehaviour
    {
        [SerializeField] private Renderer _rendered;
        [SerializeField] private float _takeDameDuration = 0.05f;
        [SerializeField] private AnimationCurve _takeDameAnimationCurve;
        private MaterialPropertyBlock _materialPropertyBlock;

        public MaterialPropertyBlock MaterialPropertyBlock
        {
            get
            {
                if (_materialPropertyBlock == null) _materialPropertyBlock = new();
                return _materialPropertyBlock;
            }
        }
        public async UniTask SetTakeDameAsync(CancellationToken cancellationToken)
        {
            await LMotion.Create(1f, 0f, _takeDameDuration).WithEase(_takeDameAnimationCurve).Bind(t =>
            {
                MaterialPropertyBlock.SetFloat("_StrongTintFade", t);
                _rendered.SetPropertyBlock(MaterialPropertyBlock);
            }).ToUniTask(cancellationToken);
        }
    }
}
