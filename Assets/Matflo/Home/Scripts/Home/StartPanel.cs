using Audio.Matflo;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

public class StartPanel : MonoSingleton<StartPanel>
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Button btnStart, btnClose;
    private float _fadeDuration = 0.2f;
    void Start()
    {
        _canvasGroup.UpdateState(true, 0);
        btnStart.onClick.AddListener(()=> StartCoroutine(OnStartButtonPressed()));
        btnClose.onClick.AddListener(() => StartCoroutine(OnCloseButtonPressed()));
    }

    private void OnDestroy()
    {
        btnStart.onClick.RemoveAllListeners();
        btnClose.onClick.RemoveAllListeners();
    }
    private IEnumerator OnStartButtonPressed()
    {
        GenericAudioManager.Instance.PlaySound(AudioName.ButtonClick);
        yield return new WaitForSeconds(GenericAudioManager.Instance.GetAudioLength(AudioName.ButtonClick));
        _canvasGroup.UpdateState(false, _fadeDuration, () =>
        {

            MatfloIntroPanel.Instance.BringIn();
        });
    }

    //private IEnumerator OnStartButtonPressed()
    //{
    //    GenericAudioManager.Instance.PlaySound(AudioName.ButtonClick);
    //    yield return new WaitForSeconds(GenericAudioManager.Instance.GetAudioLength(AudioName.ButtonClick));
    //    _canvasGroup.UpdateState(false, _fadeDuration, () => {

    //        LevelPanel.Instance.BringIn();
    //    });
    //}
    private IEnumerator OnCloseButtonPressed()
    {
        GenericAudioManager.Instance.PlaySound(AudioName.ButtonClick);
        yield return new WaitForSeconds(GenericAudioManager.Instance.GetAudioLength(AudioName.ButtonClick));
        Application.Quit();
    }
    internal void BringIn()
    {
        _canvasGroup.UpdateState(true, _fadeDuration);
    }
}
