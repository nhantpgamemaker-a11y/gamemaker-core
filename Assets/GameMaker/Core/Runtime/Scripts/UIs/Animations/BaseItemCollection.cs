using LitMotion.Animation;
using UnityEngine;
using UnityEngine.UI;

namespace GameMaker.Core.Runtime
{
    public class BaseItemCollection : MonoBehaviour
    {
        [SerializeField] protected Image imgIcon;
        [SerializeField] protected LitMotionAnimation selfAnimation;

        public void InitData(Sprite sprite)
        {
            this.imgIcon.sprite = sprite;
            selfAnimation.Play();
        }
        public void OnClear()
        {
            selfAnimation.Stop();
        }
    }
}
