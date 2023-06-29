using Audio.Matflo;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

public class MatfloIntroNarrator : MonoSingleton<MatfloIntroNarrator>
{
    private static Action _onCompleteNarrator;
    [SerializeField] private CanvasGroup _canvasGroupNarrator;
    [SerializeField] private TextMeshProUGUI panelText;
    [SerializeField] private Button btnClose;
    private string _narratorText;
    private float _fadeDuration = 0.2f;

    internal string str1 = "";
    internal string str2 = "";
    internal string str3 = "";

    void Start()
    {
        str1 = "Matflo is a warehouse control system (WCS) developed by Dematic. A WCS is a software solution that manages" +
            " and controls the activities and processes within a warehouse or distribution center. It interfaces with" +
            " various systems and equipment, such as conveyors, sorters, automated guided vehicles (AGVs), and other" +
            " material handling equipment.";
        str2 = "Matflo WCS is designed to optimize and coordinate the flow of materials and information in a warehouse environment." +
            " It provides real-time visibility into inventory, manages order fulfillment, orchestrates the movement of goods," +
            " and ensures efficient utilization of resources. Matflo WCS integrates with other Dematic software and hardware" +
            " solutions to create a comprehensive warehouse automation system.";
        str3 = "With Matflo WCS, businesses can streamline their operations, improve order accuracy, reduce inventory holding costs," +
            " enhance productivity, and achieve overall operational efficiency in their warehouses or distribution centers.";
        _canvasGroupNarrator.UpdateState(false, 0);
        btnClose.onClick.AddListener(() => BringOut());
    }
    internal void BringIn(string narratorText,
         Action onCompleteNarrator = null, AudioName audioName = AudioName.NotSet)
    {
        _narratorText = narratorText;
        panelText.text = _narratorText;
        _onCompleteNarrator = onCompleteNarrator;
        _canvasGroupNarrator.UpdateState(true, _fadeDuration, () => { StartCoroutine(PlayAudio(audioName)); });
    }

    private IEnumerator PlayAudio(AudioName audioName)
    {
        if (audioName != AudioName.NotSet)
        {
            btnClose.interactable = false;
            yield return new WaitForSeconds(0.5f);
            GenericAudioManager.Instance.PlaySound(audioName);
            yield return new WaitForSeconds(GenericAudioManager.Instance.GetAudioLength(audioName));
            btnClose.interactable = true;
        }
    }
    internal void BringOut()
    {
        //GenericAudioManager.Instance.PlaySound(AudioName.ButtonClick);
        //_canvasGroupNarrator.UpdateState(false, _fadeDuration, () =>
        //{

        //    LevelPanel.Instance.BringIn();
        //});
        if (_onCompleteNarrator != null)
        {
            _canvasGroupNarrator.UpdateState(false, _fadeDuration,()=> {

                _onCompleteNarrator();
                _onCompleteNarrator = null;
            });

        }
    }
}
