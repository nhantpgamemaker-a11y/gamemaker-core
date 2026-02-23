using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using LitMotion;
using UnityEngine;
using UnityEngine.Pool;

namespace GameMaker.Core.Runtime
{
    public class BaseCollectionAnimationController : MonoBehaviour
    {
        [Header("Spawner")]       
        [SerializeField] public float maxNumberSpawn = 10;
        [SerializeField] public RectTransform poolingContainer;
        [SerializeField] public BaseItemCollection baseItemCollectionPrefab;
        [SerializeField] public int defaultCapacity = 10;
        [SerializeField] public int maxSize = 100;
        [Header("Animation")]
        [SerializeField] public Vector2 durationFlyToTarget = Vector2.one;
        [SerializeField] public AnimationCurve flyEase;
        private ObjectPool<BaseItemCollection> pool;
        public ObjectPool<BaseItemCollection> Pool
        {
            get
            {
                if (pool == null)
                {
                    pool = new ObjectPool<BaseItemCollection>(createFunc: () =>
                    {
                        var obj = Instantiate<BaseItemCollection>(baseItemCollectionPrefab, poolingContainer);
                        return obj;
                    }, actionOnGet: (obj) =>
                    {
                        obj.gameObject.SetActive(true);
                    }, actionOnRelease: (obj) =>
                    {
                        obj.gameObject.SetActive(false);
                        obj.OnClear();

                    }, defaultCapacity: defaultCapacity,
                    maxSize: maxSize);
                    baseItemCollectionPrefab.gameObject.SetActive(false);
                }
                return pool;
            }
        }

        public async UniTask PlayAsync(Sprite sprite, float amount, Vector3 initPosition, Transform target, Action<float> OnCollectAction = null)
        {
            float maxAmount = Mathf.Min(maxNumberSpawn, amount);
            int spawnAmount = Mathf.RoundToInt(maxAmount);
            float amountPerItem = amount / spawnAmount;
            var baseItemCollections = new List<BaseItemCollection>();
            for (int i = 0; i < spawnAmount; i++)
            {
                BaseItemCollection baseItemCollection = Pool.Get();
                baseItemCollection.InitData(sprite);
                baseItemCollection.transform.position = initPosition;
                baseItemCollections.Add(baseItemCollection);
            }
            await SpawnAnimation(initPosition, baseItemCollections);

            var flyTask = new List<UniTask>();
            foreach (var item in baseItemCollections)
            {
                float duration = UnityEngine.Random.Range(durationFlyToTarget.x, durationFlyToTarget.y);
                flyTask.Add(FlyToTarget(item, target, duration, amountPerItem, OnCollectAction));
            }
            await UniTask.WhenAll(flyTask);
        }
        public virtual async UniTask SpawnAnimation(Vector3 initPosition, List<BaseItemCollection> baseItemCollections)
        {
            await UniTask.CompletedTask;
        }
        public virtual async UniTask FlyToTarget(BaseItemCollection baseItemCollection, Transform target,float duration,float amountPerItem ,Action<float> OnCollectionAction = null)
        {
            var firstPosition = baseItemCollection.transform.position;
            await LMotion.Create(0f, 1f, duration).WithEase(flyEase).Bind(t =>
            {
                Vector3 position = Vector3.Lerp(firstPosition, target.position, t);
                baseItemCollection.transform.position = position;
            });
            OnCollectionAction?.Invoke(amountPerItem);
            Pool.Release(baseItemCollection);
        }
    }
}
