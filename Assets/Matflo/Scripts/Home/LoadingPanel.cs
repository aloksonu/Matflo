using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class LoadingPanel : MonoSingleton<LoadingPanel>
{
    [SerializeField] private CanvasGroup _canvasGroup;
    private float _fadeDuration = 0.2f;
    void Start()
    {
        _canvasGroup.UpdateState(false, 0);
    }
    internal void BringIn()
    {
        _canvasGroup.UpdateState(true, _fadeDuration);
    }
    internal void BringOut()
    {
        _canvasGroup.UpdateState(false, _fadeDuration);
    }
}
