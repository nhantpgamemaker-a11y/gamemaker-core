using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameMaker.UI.Runtime
{
    public class UIClickableAnimationEffect : MonoBehaviour,IPointerDownHandler, IPointerUpHandler
    {
        [UnityEngine.SerializeField]
        private BaseUIAnimation _uiAnimation;

        public void OnPointerDown(PointerEventData eventData)
        {
            _uiAnimation.ShowAsync().Forget();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _uiAnimation.HideAsync().Forget();
        }
    }
}
