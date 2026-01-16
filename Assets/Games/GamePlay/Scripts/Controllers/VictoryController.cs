using UnityEngine;

namespace Game.GamePlay
{
    public class VictoryController : MonoBehaviour
    {
        [UnityEngine.SerializeField]
        private GameManager _gameManager;
        public void Initialize(GameManager gameManager)
        {
            _gameManager = gameManager;
        }
        void OnTriggerEnter2D(Collider2D collision)
        {
            var playerController = collision.GetComponentInParent<PlayerController>();
            if (playerController != null)
            {
                _gameManager.HandleVictory();
            }
        }
    }
}
