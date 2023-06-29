using Random = System.Random;
using Action = System.Action;
using IEnumerator = System.Collections.IEnumerator;
using MonoBehaviour = UnityEngine.MonoBehaviour;
using WaitForSeconds = UnityEngine.WaitForSeconds;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities
{
    public static class ExtensionMethodLibrary
    {
        private static readonly Random _rng = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = _rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        /// <summary>
        /// Custom Invoke Method to make Method Calls with a DELAY and Any number of PARAMETERS using the method call.
        /// Allows proper Method signature/calls refactoring and displays the call in references.
        /// USAGE-EXAMPLE: Pass the method in lambda function call as this.Invoke( ()=>MethodToCall(), timeDelay );
        /// </summary>
        /// <param name="monoBehaviour">monoBehaviour instance</param>
        /// <param name="methodName">Method name</param>
        /// <param name="delay">Time Delay</param>
        public static void Invoke(this MonoBehaviour monoBehaviour, Action methodName, float delay)
        {
            DelayedInvokeComponent delayedInvokeComponent =
                monoBehaviour.gameObject.AddComponent<DelayedInvokeComponent>();
            delayedInvokeComponent.Init(methodName, delay);
        }

        public static void UpdateState(this CanvasGroup canvasGroup, bool newState, float fadeDuration = 0.5f,
            Action onFadeComplete = null)
        {
            canvasGroup.DOFade(newState ? 1 : 0, fadeDuration).OnComplete(() =>
            {
                if (onFadeComplete != null) onFadeComplete.Invoke();
            });
            canvasGroup.interactable = newState;
            canvasGroup.blocksRaycasts = newState;
        }

        public static void SnapWorldToScreenPosition(this Transform myTransform, Transform targetObjectAtWorld,
           Canvas myCanvas, Camera worldCamera, Camera canvasCamera)
        {
            var screen = worldCamera.WorldToScreenPoint(targetObjectAtWorld.position);
            screen.z = (myCanvas.transform.position - canvasCamera.transform.position).magnitude;
            var position = canvasCamera.ScreenToWorldPoint(screen);
            myTransform.position = position;
        }

        public static float GetAnimatorClipLength(this Animator myAnimator, int animationClipHash)
        {
            AnimationClip[] clips = myAnimator.runtimeAnimatorController.animationClips;
            foreach (AnimationClip clip in clips)
            {
                int hash = Animator.StringToHash(clip.name);
                if (hash == animationClipHash)
                {
                    return clip.length;
                }
            }
            Logger.LogErrorFormat("NO ANIMATION STATE WITH HASH  {0} FOUND IN {1}", animationClipHash, myAnimator.name);
            return 0;
        }

        public static bool IsMouseInside(this RectTransform rectTransform, Camera camera = null)
        {
            if (camera == null) camera = Camera.main;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition, Camera.main,
                out var cachedLocalPoint);
            return rectTransform.rect.Contains(cachedLocalPoint);
        }

        public static void SetRect(this RectTransform trs, float left, float top, float right, float bottom)
        {
            trs.offsetMin = new Vector2(left, bottom);
            trs.offsetMax = new Vector2(-right, -top);
        }
    }

    public class DelayedInvokeComponent : MonoBehaviour
    {
        public void Init(Action methodName, float delay)
        {
            StartCoroutine(DelayedRoutine(methodName, delay));
        }

        private IEnumerator DelayedRoutine(Action method, float delay)
        {
            yield return new WaitForSeconds(delay);
            method();
            Destroy(this);
        }
    }

    public static class ScrollRectExtensions
    {
        public static Vector2 GetSnapToPositionToBringChildIntoView(this ScrollRect instance, RectTransform child)
        {
            Canvas.ForceUpdateCanvases();
            Vector2 viewportLocalPosition = instance.viewport.localPosition;
            Vector2 childLocalPosition = child.localPosition;
            Vector2 result = new Vector2(
                0 - (viewportLocalPosition.x + childLocalPosition.x),
                0 - (viewportLocalPosition.y + childLocalPosition.y)
            );
            return result;
        }
    }
}