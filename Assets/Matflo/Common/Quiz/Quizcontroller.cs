using System;
using Matflo.Common.Score;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace Matflo.Common.Quiz
{
    public class Quizcontroller : MonoSingleton<Quizcontroller>
    {
        public static Action OnRightSelection, OnWrongSelection;
        private Action _onRightOption, _onWrongOption;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private QuestionPanel quizPanelTextOnly;
        private bool _isQuizWithModel;
        private int _numberOfAttempts;
        private enum QuizType
        {
            TwoOptional,
            TreeOptional,
            FourOptional
        }

        //private QuizType _quizType;
        private int _wrongAttempt;
        private bool _isOnceTimeAttempt;
        void Start()
        {
            canvasGroup.UpdateState(false,0f);
            OnRightSelection = OnCorrectButtonSelected;
            OnWrongSelection = OnWrongButtonSelected;
        }
    
        internal void BringQuizPanel(Action onRightSelection, Action onFailure, string questionText,
            string rightAnswer, string[] wrongAnswer, bool isOnceTimeAttempt = false)
        {
            _isQuizWithModel = true;
            DestroyExistingPrefab();
            _isOnceTimeAttempt = isOnceTimeAttempt;
            SetQuizType(wrongAnswer.Length);
            quizPanelTextOnly.InstantiateQuizButtons(questionText, rightAnswer, wrongAnswer);
            _onRightOption = onRightSelection;
            _onWrongOption = onFailure;
            _wrongAttempt = 0;
            canvasGroup.UpdateState(true);
        }


        private void DestroyExistingPrefab()
        {

                for (int i = quizPanelTextOnly.transform.childCount - 1; i >= 0; i--)
                {
                    DestroyImmediate(quizPanelTextOnly.transform.GetChild(i).gameObject);
                }
            

            //_wrongAttempt = 0;
        }

        private void OnCorrectButtonSelected()
        {
            //if (_isQuizWithModel)
            //{
            //    for (int i = quizPanelTextOnly.transform.childCount - 1; i >= 0; i--)
            //    {
            //        quizPanelTextOnly.transform.GetChild(i).GetComponent<Button>().interactable = false;
            //    }
            //}

            for (int i = quizPanelTextOnly.transform.childCount - 1; i >= 0; i--)
            {
                quizPanelTextOnly.transform.GetChild(i).GetComponent<Button>().interactable = false;
            }
            ScoreManager.Instance.UpdateScore(10,10, _wrongAttempt);
            Invoke(nameof(ActionDelay), 1f);
        }

        private void ActionDelay()
        {
            BringOutEffect(() =>
            {
                _onRightOption?.Invoke();
            });
        }
        private void OnWrongButtonSelected()
        {
            // BringOutEffect(_onWrongOption);
            for (int i = quizPanelTextOnly.transform.childCount - 1; i >= 0; i--)
            {
                quizPanelTextOnly.transform.GetChild(i).GetComponent<Button>().interactable = false;
            }
            _wrongAttempt++;
            //ScoreManager.Instance.UpdateScore(0, 10);
            Invoke(nameof(ActionDelayForWrong), 1f);
        }

        private void ActionDelayForWrong()
        {
            BringOutEffect(() =>
            {
                _onWrongOption?.Invoke();
            });
        }

        private void BringOutEffect(Action onFadeComplete)
        {
            if (_isQuizWithModel)
                canvasGroup.UpdateState(false, onFadeComplete: () => { onFadeComplete?.Invoke(); });
        }
        
        private void SetQuizType(int wrongAnsLength)
        {
            //switch (wrongAnsLength)
            //{
            //    case 1:
            //        _quizType = QuizType.TwoOptional;
            //        break;
            //    case 2:
            //        _quizType = QuizType.TreeOptional;
            //        break;
            //    case 3:
            //        _quizType = QuizType.FourOptional;
            //        break;
            //}
        }
    }
}
