using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace GameMaker.Core.Runtime
{
    public class InternetService: AutomaticMonoSingleton<InternetService>
    {
        public event Action<bool> OnInternetStateChanged;
        public bool HasInternet { get; private set; }
        [SerializeField] private float checkInterval = 5f;
        [SerializeField] private int timeoutSeconds = 3;

        private bool _isChecking;
        public bool IsChecking => _isChecking;

        public async UniTaskVoid StartChecking()
        {
            while (true)
            {
                await CheckInternet();
                await UniTask.Delay(TimeSpan.FromSeconds(checkInterval));
            }
        }

        public async UniTask<bool> CheckInternet()
        {
            if (_isChecking) return HasInternet;

            _isChecking = true;

            bool previousState = HasInternet;
            bool currentState = await HasRealInternetAsync();

            HasInternet = currentState;

            if (previousState != currentState)
            {
                OnInternetStateChanged?.Invoke(currentState);
                Logger.Log($"[InternetService] Internet Changed: {currentState}");
            }

            _isChecking = false;
            return HasInternet;
        }

        private async UniTask<bool> HasRealInternetAsync()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
                return false;

            try
            {
                using var request = UnityWebRequest.Get("https://captive.apple.com/");
                request.timeout = timeoutSeconds;

                await request.SendWebRequest();

                if (request.result != UnityWebRequest.Result.Success)
                    return false;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}