using Matflo.Common.Audio;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

public class PlateformerNarrator : MonoSingleton<PlateformerNarrator>
{
    internal string None;
    internal string Ntwo;
    internal string Nthree;
    internal string Nfour;

    public Sprite spriteOne;
    public Sprite spriteTwo;
    public Sprite spriteThree;
    public Sprite spriteFour;

    private static Action _onCompleteNarrator;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private TextMeshProUGUI textTMP;
    [SerializeField] private Image img;
    [SerializeField] private Button btnClose;
    private string _narratorText;
    private float _fadeDuration = 0.2f;
    public bool isNarratorOpen = false;
    void Start()
    {
        None = "One";
        Ntwo = "Two";
        Nthree = "Three";
        Nfour = "Four";

        _canvasGroup.UpdateState(false, 0);
        btnClose.onClick.AddListener(BringOutNarrator);
    }
    private void OnDestroy()
    {
        btnClose.onClick.RemoveAllListeners();
    }
    internal void BringInNarrator(string narratorText, AudioName audioName, Sprite spr,
        Action onCompleteNarrator = null)
    {
        btnClose.interactable = false;
        _narratorText = narratorText;
        textTMP.text = _narratorText;
        img.sprite = spr;
        _onCompleteNarrator = onCompleteNarrator;
        isNarratorOpen = true;
        _canvasGroup.UpdateState(true, _fadeDuration, () => { StartCoroutine(PlayAudio(audioName)); });
    }
    private IEnumerator PlayAudio(AudioName audioName)
    {
        yield return new WaitForSeconds(0.5f);
        GenericAudioManager.Instance.PlaySound(audioName);
        yield return new WaitForSeconds(GenericAudioManager.Instance.GetAudioLength(audioName));
        btnClose.interactable = true;
    }

    internal void BringOutNarrator()
    {
        GenericAudioManager.Instance.PlaySound(AudioName.ButtonClick);
        isNarratorOpen = false;
        if (_onCompleteNarrator != null)
        {
            _onCompleteNarrator();
            _onCompleteNarrator = null;
            _canvasGroup.UpdateState(false, _fadeDuration);
        }
        else
        {
            _canvasGroup.UpdateState(false, _fadeDuration, () => {
                _onCompleteNarrator = null;
            });
        }

    }
}
