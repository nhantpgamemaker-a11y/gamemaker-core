using Cysharp.Threading.Tasks;
using GameMaker.Core.Runtime;
using GameMaker.UI.Runtime;
using UnityEngine;

namespace CatAdventure.GamePlay
{
    public class GameEntry : MonoBehaviour
    {
        [UnityEngine.SerializeField]
        private UIController _uiController;
        [UnityEngine.SerializeField]
        private BootStepController _bootStepController;
        public void Awake()
        {
            StartAsync().Forget();
        }
        public async UniTask StartAsync()
        {
            _uiController.OnInit();
            _bootStepController.Init();
            await _uiController.ViewController.ShowAsync(BootView.VIEW_NAME, data:_bootStepController);
            await _bootStepController.StartBootAsync();
        }
    }
}
