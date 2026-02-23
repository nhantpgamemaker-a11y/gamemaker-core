using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using LitMotion;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    public class ExplosionCollectionAnimationController : BaseCollectionAnimationController
    {
        [Header("Explosion Config")]
        [SerializeField] private Vector2 explosionRadius = Vector2.one;
        [SerializeField] private Vector2 explosionDuration = Vector2.one;
        [SerializeField] private AnimationCurve explosionEase;
        public async override UniTask SpawnAnimation(Vector3 initPosition, List<BaseItemCollection> baseItemCollections)
        {
            var tasks = new List<UniTask>();
            foreach (var visual in baseItemCollections)
            {
                Vector2 randomDir = UnityEngine.Random.insideUnitCircle.normalized;
                float radius = UnityEngine.Random.Range(explosionRadius.x, explosionRadius.y);
                Vector3 targetPos = initPosition + (Vector3)(randomDir * radius);
                float duration = UnityEngine.Random.Range(explosionDuration.x, explosionDuration.y);
                tasks.Add(LMotion.Create(visual.transform.position, targetPos, duration).WithEase(explosionEase).Bind(p =>
                {
                    visual.transform.position = p;
                }).ToUniTask());
            }
            await UniTask.WhenAll(tasks);
        }
    }
}
