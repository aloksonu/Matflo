using System.Collections;
using Matflo.Common.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utilities;

namespace Matflo.Home.Scripts.Home
{
    public class LevelPanel : MonoSingleton<LevelPanel>
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Button btnBack;
        internal LevelName levelName;
        private float _fadeDuration = 0.2f;
        // private string _currentSceneName = "CETinterface";
        void Start()
        {
            _canvasGroup.UpdateState(false, 0);
            btnBack.onClick.AddListener(()=>StartCoroutine(OnBackButtonPressed()));
        }
        private void OnDestroy()
        {
            btnBack.onClick.RemoveAllListeners();
        }
        internal void OnContinueButtonPressed(string currentSceneName)
        {
            _canvasGroup.UpdateState(true, _fadeDuration, () => {
                StartCoroutine(_loadGame(currentSceneName));
            });
        }
        private IEnumerator OnBackButtonPressed()
        {
            GenericAudioManager.Instance.PlaySound(AudioName.ButtonClick);
            yield return new WaitForSeconds(GenericAudioManager.Instance.GetAudioLength(AudioName.ButtonClick));
            _canvasGroup.UpdateState(false, _fadeDuration, () => {
                StartPanel.Instance.BringIn();
            });
        }
        private IEnumerator _loadGame(string currentSceneName)
        {
            GenericAudioManager.Instance.PlaySound(AudioName.ButtonClick);
            yield return new WaitForSeconds(GenericAudioManager.Instance.GetAudioLength(AudioName.ButtonClick));
            LoadingPanel.Instance.BringIn();
            yield return SceneManager.LoadSceneAsync(currentSceneName, LoadSceneMode.Additive);
            //PausePanel.Instance.UpdatPauseButtoneState(true);
            _canvasGroup.UpdateState(false, _fadeDuration);
            LoadingPanel.Instance.BringOut();
        }
        internal void BringIn()
        {
            _canvasGroup.UpdateState(true, _fadeDuration);
        }
    }
}
