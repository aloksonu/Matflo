using System.Collections;
using Matflo.Common.Audio;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utilities;

namespace Matflo.Home.Scripts.Home
{
    public class LevelFail : MonoSingleton<LevelFail>
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Button btnRetry, btnHome;
        [SerializeField] private TextMeshProUGUI levelFailTextMeshProUGUI;
        private float _fadeDuration = 0.2f;
        void Start()
        {
            _canvasGroup.UpdateState(false, 0);
            btnRetry.onClick.AddListener(OnRetryButtonPressed);
            btnHome.onClick.AddListener(OnHomeButtonPressed);
        }
        private void OnDestroy()
        {
            btnRetry.onClick.RemoveAllListeners();
            btnHome.onClick.RemoveAllListeners();
        }
        internal void BringIn()
        {
            //levelFailTextMeshProUGUI.text = _gameCompleteText + " " + LevelPanel.Instance.levelName;
            _canvasGroup.UpdateState(true, _fadeDuration);
        }
        internal void BringOut()
        {
            _canvasGroup.UpdateState(false, _fadeDuration);
        }
        internal void OnRetryButtonPressed()
        {
            GenericAudioManager.Instance.PlaySound(AudioName.ButtonClick);
            //_canvasGroup.UpdateState(false, _fadeDuration, () => {
            //    ScoreManager.Instance.ResetScore();
            //    HealthManager.Instance.ResetHealth();
            //    CETinterfaceManager.Instance.ResetGame();
            //});
            SceneManager.UnloadSceneAsync(LevelPanel.Instance.levelName.ToString());
            SceneManager.LoadSceneAsync(LevelPanel.Instance.levelName.ToString(), LoadSceneMode.Additive);
            _canvasGroup.UpdateState(false, 0);
        }
        internal void OnHomeButtonPressed()
        {
            GenericAudioManager.Instance.PlaySound(AudioName.ButtonClick);
            StartCoroutine(UnloadScene());
        }

        IEnumerator UnloadScene()
        {
            yield return SceneManager.UnloadSceneAsync(LevelPanel.Instance.levelName.ToString());
            _canvasGroup.UpdateState(false, 0);
            LevelPanel.Instance.BringIn();
        }
    }
}
