using Audio.Matflo;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

public class MatfloIntroPanel : MonoSingleton<MatfloIntroPanel>
{
    private static Action _onComplete;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private CanvasGroup _canvasGroupButton;
    [SerializeField] private Button btnSkip,btnIntro,btnBack;
    private float _fadeDuration = 0.1f;
    void Start()
    {
        btnSkip.onClick.AddListener(OnClickSkipButton);
        btnBack.onClick.AddListener(OnClickBackButton);
        btnIntro.onClick.AddListener(OnClickIntroButton);
        _canvasGroup.UpdateState(false, 0);
    }
    private void OnDestroy()
    {
        btnSkip.onClick.RemoveAllListeners();
    }

    private void OnClickSkipButton()
    {
        StartCoroutine(_OnClickSkipButton());
    }

    private void OnClickBackButton()
    {
        StartCoroutine(_OnClickBackButton());
    }
    private void OnClickIntroButton()
    {
        StartCoroutine(_OnClickIntroButton());
    }
    private IEnumerator _OnClickSkipButton()
    {
        GenericAudioManager.Instance.PlaySound(AudioName.ButtonClick);
        yield return new WaitForSeconds(GenericAudioManager.Instance.GetAudioLength(AudioName.ButtonClick));
        _canvasGroup.UpdateState(false, _fadeDuration, () => {

            LevelPanel.Instance.BringIn();
        });
    }
    private IEnumerator _OnClickBackButton()
    {
        GenericAudioManager.Instance.PlaySound(AudioName.ButtonClick);
        yield return new WaitForSeconds(GenericAudioManager.Instance.GetAudioLength(AudioName.ButtonClick));
        _canvasGroup.UpdateState(false, _fadeDuration, () => {

            StartPanel.Instance.BringIn();
        });
    }
    private IEnumerator _OnClickIntroButton()
    {
        GenericAudioManager.Instance.PlaySound(AudioName.ButtonClick);
        yield return new WaitForSeconds(GenericAudioManager.Instance.GetAudioLength(AudioName.ButtonClick));
        _canvasGroupButton.UpdateState(false);
        MatfloIntroNarrator.Instance.BringIn(MatfloIntroNarrator.Instance.str1,()=> {

            StartCoroutine(_MatfloIntro2());
        },AudioName.MatflowIntro1);
    }
    private IEnumerator _MatfloIntro2()
    {
        GenericAudioManager.Instance.PlaySound(AudioName.ButtonClick);
        yield return new WaitForSeconds(GenericAudioManager.Instance.GetAudioLength(AudioName.ButtonClick));
        MatfloIntroNarrator.Instance.BringIn(MatfloIntroNarrator.Instance.str2, () => {

            StartCoroutine(_MatfloIntro3());
        }, AudioName.MatflowIntro2);
    }
    private IEnumerator _MatfloIntro3()
    {
        GenericAudioManager.Instance.PlaySound(AudioName.ButtonClick);
        yield return new WaitForSeconds(GenericAudioManager.Instance.GetAudioLength(AudioName.ButtonClick));
        MatfloIntroNarrator.Instance.BringIn(MatfloIntroNarrator.Instance.str3, () => {

            StartCoroutine(_LevelPanel());
        }, AudioName.MatflowIntro3);
    }
    private IEnumerator _LevelPanel()
    {
        GenericAudioManager.Instance.PlaySound(AudioName.ButtonClick);
        yield return new WaitForSeconds(GenericAudioManager.Instance.GetAudioLength(AudioName.ButtonClick));
        _canvasGroup.UpdateState(false, _fadeDuration, () => {

            LevelPanel.Instance.BringIn();
        });
    }

    internal void BringIn(float deale = 0f)
    {
        _canvasGroupButton.UpdateState(true, _fadeDuration);
        _canvasGroup.UpdateState(true, _fadeDuration);
    }
    internal void BringOut()
    {
        StartCoroutine(EBringOut());
    }
    IEnumerator EBringOut()
    {
        btnSkip.interactable = false;
        GenericAudioManager.Instance.PlaySound(AudioName.ButtonClick);
        yield return new WaitForSeconds(GenericAudioManager.Instance.GetAudioLength(AudioName.ButtonClick));
        if (_onComplete != null)
        {
            _canvasGroup.UpdateState(false, _fadeDuration, () => {
                _onComplete();
                _onComplete = null;
                btnSkip.interactable = true;
            });
        }
        else
        {
            _canvasGroup.UpdateState(false, _fadeDuration, () => {
                _onComplete = null;
                btnSkip.interactable = true;
            });
        }
    }
}
