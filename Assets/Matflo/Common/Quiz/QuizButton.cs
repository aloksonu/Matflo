using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Matflo.Common.Quiz
{
    public class QuizButton : MonoBehaviour ,IPointerEnterHandler,IPointerExitHandler
    {
        public enum ButtonType
        {
            NotSet = -1,
            RightAnswer = 0,
            WrongAnswer = 1,
        }

        public ButtonType buttonType;
        public TextMeshProUGUI buttonTextComponent;
        public Button quizButton;
        public Color normalColor, clickedColor;
        //private float _fadeDuration = 0.4f;
        
        private void Start()
        {
            if (quizButton == null)
                quizButton = GetComponent<Button>();

            quizButton.onClick.AddListener(OnClickButton);
           // imageFeedback.DOFade(0, 0f);
        }
        private void OnClickButton()
        {
            if (buttonType == ButtonType.RightAnswer)
            {
                quizButton.interactable = false;
                Quizcontroller.OnRightSelection();
                OnPressedEvent();
            }
            else
            {
                Quizcontroller.OnWrongSelection();
                OnPressedEvent();
            }
        }

        private void OnPressedEvent()
        {
            // imageFeedback.DOFade(1, 0.1f);
            quizButton.GetComponent<Image>().color = clickedColor;
            Invoke(nameof(OnReleasedEvent), 1f);
        }
        
        private void OnReleasedEvent()
        {
            quizButton.GetComponent<Image>().color = normalColor;
            // imageFeedback.DOFade(0, 0.1f);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
           // quizButton.gameObject.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        }
        public void OnPointerExit(PointerEventData eventData)
        {
           // quizButton.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
