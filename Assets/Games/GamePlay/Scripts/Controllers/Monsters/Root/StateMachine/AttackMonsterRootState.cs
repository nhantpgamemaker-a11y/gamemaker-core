using Game.GamePlay;
using UnityEngine;

namespace GamePlay.Game
{
    public abstract class AttackMonsterRootState: BaseMonsterRootState
    {
        [SerializeField] private AudioClip _attackSfx;
        public abstract Collider2D GetCollider2D();
        protected internal override void OnAnimationTransitionEventHandle()
        {
            ContactFilter2D filter = new ContactFilter2D();
            filter.useLayerMask = true;
            filter.layerMask = monsterRootStateMachine.RootMonsterData.PlayerLayerMask;
            monsterRootStateMachine.AudioSource.PlayOneShot(_attackSfx);
            Collider2D[] contactColliders = new Collider2D[1];
            int contactAmount = Physics2D.OverlapCollider(GetCollider2D(), filter, contactColliders);
            if (contactAmount != 0)
            {
                foreach(var collider in contactColliders)
                {
                    var takeDame = collider.transform.GetComponentInParent<ITakeDame>();
                    if (takeDame != null)
                    {
                        var center = collider.bounds.center;
                        Vector3 direction = center - this.transform.position;
                        direction.y = 0f;
                        direction.z = 0f;
                        takeDame.TakeDame(30f,direction, center);
                    }
                }
            }

        }
    }
}