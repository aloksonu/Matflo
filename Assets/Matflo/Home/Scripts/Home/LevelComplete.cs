using System.Collections;
using Matflo.Common.Audio;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utilities;

namespace Matflo.Home.Scripts.Home
{
    public class LevelComplete : MonoSingleton<LevelComplete>
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Button btnNext, btnHome;
        [SerializeField] private TextMeshProUGUI gameCompleteTextMeshProUGUI;
        private float _fadeDuration = 0.2f;

        private string _gameCompleteText = "Congratulation To Complete";
        void Start()
        {
            _canvasGroup.UpdateState(false, 0);
            btnNext.onClick.AddListener(OnNextButtonPressed);
            btnHome.onClick.AddListener(OnHomeButtonPressed);
        }
        private void OnDestroy()
        {
            btnNext.onClick.RemoveAllListeners();
            btnHome.onClick.RemoveAllListeners();
        }
        internal void BringIn(float fadeDuration = 0.2f)
        {
            _fadeDuration = fadeDuration;
            gameCompleteTextMeshProUGUI.text = _gameCompleteText + " " + LevelPanel.Instance.levelName;
            _canvasGroup.UpdateState(true, _fadeDuration);
        }
        internal void BringOut()
        {
            _canvasGroup.UpdateState(false, _fadeDuration);
        }
        internal void OnNextButtonPressed()
        {
            GenericAudioManager.Instance.PlaySound(AudioName.ButtonClick);
            StartCoroutine(LoadNextScene());
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
        IEnumerator LoadNextScene()
        {
    
            yield return SceneManager.UnloadSceneAsync(LevelPanel.Instance.levelName.ToString());

            if (LevelPanel.Instance.levelName == LevelName.NotSet)
            {
                LevelPanel.Instance.levelName = LevelName.NotSet;
            }
            else if (LevelPanel.Instance.levelName == LevelName.NotSet)
            {
                LevelPanel.Instance.levelName = LevelName.NotSet;
            }
            else if (LevelPanel.Instance.levelName == LevelName.NotSet)
            {
                //LevelPanel.Instance.levelName = LevelName.NotSet;
                //StartCoroutine(UnloadScene());
            }
            yield return SceneManager.LoadSceneAsync(LevelPanel.Instance.levelName.ToString(), LoadSceneMode.Additive);
            _canvasGroup.UpdateState(false, 0);
        }

        private void UnlockNextLevel()
        {
            //if (LevelPanel.Instance.levelName == "Receiving")
            //{
            //    DataManager.Instance.UpdateLock(LevelsName.Putaway, false);
            //}
            //else if (LevelPanel.Instance.levelName == "Putaway")
            //{
            //    DataManager.Instance.UpdateLock(LevelsName.InventoryManagement, false);
            //}
            //else if (LevelPanel.Instance.levelName == "InventoryManagement")
            //{
            //    DataManager.Instance.UpdateLock(LevelsName.Picking, false);
            //}
            //else if (LevelPanel.Instance.levelName == "Picking")
            //{
            //    DataManager.Instance.UpdateLock(LevelsName.ItemSortation, false);
            //}
            //else if (LevelPanel.Instance.levelName == "ItemSortation")
            //{
            //    DataManager.Instance.UpdateLock(LevelsName.Packing, false);
            //}
            //else if (LevelPanel.Instance.levelName == "Packing")
            //{
            //    DataManager.Instance.UpdateLock(LevelsName.Despatch, false);
            //}
        }
    }
}
