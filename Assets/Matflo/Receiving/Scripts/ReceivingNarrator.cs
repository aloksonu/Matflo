using Matflo.Common.Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

public class ReceivingNarrator : MonoSingleton<ReceivingNarrator>
{
    internal string NOIScreen;
    internal string NSelectDelivery;
    internal string NScanSKU;
    internal string NScanTM;
    internal string NEnterQuantity;
    internal string NPutTM;
    internal string NClosingDelivery;

    public Sprite spriteOIScreen;
    public Sprite spriteSelectDelivery;
    public Sprite spriteScanSKU;
    public Sprite spriteScanTM;
    public Sprite spriteEnterQuantity;
    public Sprite spritePutTM;
    public Sprite spriteClosingDelivery;

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
        NOIScreen = "If the person has multiple duties, click on the inbound tile and then click on the Receiving tile.";
        NSelectDelivery = "The delivery can be searched and selected corresponding to Delivery Id.";
        NScanSKU = "WMS checks if the SKU is valid <br> " +
                   "If so it displays the Scan TM details page<br>" +
                   "Else scan another SKU";
        NScanTM = "WMS checks if the TM is valid <br>" +
                   "If so it displays its information and we have to select the TM type<br>" +
                   "else scan another TM";
        NEnterQuantity = "The quantity entered should not exceed the total quantity of the TM<br>" +
                         "If so, we have to re-entered the quantity.<br>" +
                         "If it is valid quantity we receive the stock.";
        NPutTM = "The received stock is send to Storage Location.";
        NClosingDelivery = "Press confirm and close the delivery once all the stocks of the delivery is recived.";

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
