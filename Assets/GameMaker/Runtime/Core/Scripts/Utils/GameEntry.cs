using UnityEngine;

namespace GameMaker.Core.Runtime
{
    public class GameEntry : MonoBehaviour
    {
        [SerializeField]
        private DataBootstrapper _gameModuleBootstrapper;
        async void Awake()
        {
            bool status = await _gameModuleBootstrapper.BuildAsync();
        }
    }
}
