using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Quiz
{
    public class QuestionPanel : MonoBehaviour
    {
        [SerializeField] private Button[] quizOptionButtonRef = null;
        [SerializeField] private RectTransform parentRecTrans;
        [SerializeField] private QuizButton rightAnsPrefabs;
        [SerializeField] private QuizButton wrongAnsPrefabs;
        [SerializeField] private VerticalLayoutGroup layoutGroup;
        [SerializeField] private TextMeshProUGUI questionTextComponent;
        //[SerializeField] private GameObject parentModel;
        void Start()
        {

        }
        
        internal void InstantiateQuizButtons(string questionText,
            string rightAnswer,
            string[] wrongAnsText)
        {
            questionTextComponent.text = questionText;
            QuizButton b = Instantiate(rightAnsPrefabs, parentRecTrans, false);
            b.buttonTextComponent.text = rightAnswer;
            for (int i = 0; i < wrongAnsText.Length; i++)
            {
                QuizButton qb = Instantiate(wrongAnsPrefabs, parentRecTrans, false);
                qb.buttonTextComponent.text = wrongAnsText[i];
            }

            ShuffleOptionAndSetLayoutSpacing(wrongAnsText.Length);
        }

        //internal void InstantiateQuizButtons(string questionText, GameObject model, string rightAnswer, string[] wrongAnswer)
        //{
        //    GameObject modelPrefab = Instantiate(model, parentModel.transform);
        //   // modelPrefab.AddComponent<ModelRotation>(); // it is already comented
        //    InstantiateQuizButtons(questionText, rightAnswer, wrongAnswer);
        //}
        
        private void ShuffleOptionAndSetLayoutSpacing(int length)
        {
            switch (length)
            {
                case 1:
                    layoutGroup.spacing = 50;
                    break;
                case 2:
                    layoutGroup.spacing = 30;
                    break;
                case 3:
                    layoutGroup.spacing = 10;
                    break;
                default:
                    layoutGroup.spacing = 5;
                    break;
            }

            int childCount = parentRecTrans.childCount;
            quizOptionButtonRef = new Button[childCount];
            for (int i = 0; i < childCount; i++)
            {
                quizOptionButtonRef[i] = parentRecTrans.GetChild(i).GetComponent<Button>();
            }

            ShuffleQuizOption();
        }

        private void ShuffleQuizOption()
        {
            for (int i = 0; i < quizOptionButtonRef.Length; i++)
            {
                int index = Random.Range(0, quizOptionButtonRef.Length);
                parentRecTrans.GetChild(i).SetSiblingIndex(index);
            }

        }
        
        internal void DestroyExistingPrefab()
        {
            for (int i = 0; i < parentRecTrans.childCount; i++)
            {
                Destroy(parentRecTrans.GetChild(i).gameObject);
            }
        }
    }
}
