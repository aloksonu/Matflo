using Audio.Matflo;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

public class PutawayNarrator : MonoSingleton<PutawayNarrator>
{
    internal string NOIScreen;
    internal string NPickingLocation;
    internal string NScanTM;
    internal string NScanSKU;
    internal string NPutawayLocation;
    internal string NScanLocation;
    internal string NPutTM;

    public Sprite spriteOIScreen;
    public Sprite spritePickingLocation;
    public Sprite spriteScanTM;
    public Sprite spriteScanSKU;
    public Sprite spritePutawayLocation;
    public Sprite spriteScanLocation;
    public Sprite spritePutTM;

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
        NOIScreen = "Accessing the user interface for putaway operations.";
        NPickingLocation = "Moving to the designated area for picking items.";
        NScanTM = "Using a scanning device to read the unique identifier of a transport medium (TM) present in the location.";
        NScanSKU = "Scanning the barcode or identifier of a specific item (SKU) stored within the transport medium.";
        NPutawayLocation = "Showing the appropriate storage location for putaway on the screen.";
        NScanLocation = "Scanning the barcode or identifier of the designated putaway location";
        NPutTM = "Placing the transport medium onto the assigned putaway location.";

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
