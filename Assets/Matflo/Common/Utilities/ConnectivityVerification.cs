using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Utilities
{
    public class ConnectivityVerification : MonoSingleton<ConnectivityVerification>
    {
        [SerializeField] private float timeToWait = 5;

        private Action _onConnectionSuccessfull;
        private Action _onConnectionfailed;
        private Coroutine _routine;
        private bool _isRoutineRunning;

        public void CheckInternetConnection(Action onConnectionSuccessfull, Action onConnectionfailed = null)
        {
            if (_isRoutineRunning == false)
            {
                _onConnectionSuccessfull = onConnectionSuccessfull;
                _onConnectionfailed = onConnectionfailed;
                _routine = StartCoroutine(CheckRoutine());
            }
            else
            {
                _onConnectionSuccessfull += onConnectionSuccessfull;
                _onConnectionfailed += onConnectionfailed;
            }
        }

        protected override void Init()
        {
            base.Init();
            _isRoutineRunning = false;
        }

        private IEnumerator CheckRoutine()
        {
            _isRoutineRunning = true;
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                _onConnectionfailed?.Invoke();
                _isRoutineRunning = false;
                yield break;
            }

            UnityWebRequest request = new UnityWebRequest("trueformgames.com");
            request.SendWebRequest();

            float elapsedTime = 0.0f;

            WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
            while (!request.isDone)
            {
                elapsedTime += Time.deltaTime;
                if (elapsedTime >= timeToWait && request.downloadProgress <= 0.5)
                    break;

                yield return waitForEndOfFrame;
            }
            if (!request.isDone || !string.IsNullOrEmpty(request.error))
                _onConnectionfailed?.Invoke();
            else
                _onConnectionSuccessfull?.Invoke();

            _isRoutineRunning = false;
        }
    }
}