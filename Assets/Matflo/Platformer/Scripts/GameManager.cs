using Matflo.Common.Audio;
using TMPro;
using UnityEngine;

namespace Matflo.Platformer.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        public CanvasGroup cgGameWin;
        public CanvasGroup cgGameLose;
        public TextMeshProUGUI coinText;
        public TextMeshProUGUI timeText;
        public float timeLimit = 60f;
        public PlayerController playerController;
        private float timeLeft;
        private int collectedCoins = 0;
        internal bool isGameWin;
        internal bool isGameLose;

        void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);
        }

        void Start()
        {
            timeLeft = timeLimit;
            isGameWin = false;
            isGameLose = false;
            UpdateCoinText();
            UpdateTimeText();
        }

        void Update()
        {
            timeLeft -= Time.deltaTime;
            UpdateTimeText();

            if (timeLeft <= 0f)
            {
                GameLose();
            }
        }

        public void CollectCoin()
        {
            collectedCoins++;
            UpdateCoinText();
            ShowNarrator();
            //if (collectedCoins >= playerController.totalSteps)
            //{
            //    GameWin();
            //}
        }

        private void ShowNarrator()
        {
            if (collectedCoins == 1)
            {
                PlateformerNarrator.Instance.BringInNarrator(PlateformerNarrator.Instance.None, AudioName.ButtonClick, PlateformerNarrator.Instance.spriteOne);
            }
            if (collectedCoins == 2)
            {
                PlateformerNarrator.Instance.BringInNarrator(PlateformerNarrator.Instance.Ntwo, AudioName.ButtonClick, PlateformerNarrator.Instance.spriteTwo);
            }
            if (collectedCoins == 3)
            {
                PlateformerNarrator.Instance.BringInNarrator(PlateformerNarrator.Instance.Nthree, AudioName.ButtonClick, PlateformerNarrator.Instance.spriteThree);
            }
            if (collectedCoins == 4)
            {
                PlateformerNarrator.Instance.BringInNarrator(PlateformerNarrator.Instance.Nfour, AudioName.ButtonClick, PlateformerNarrator.Instance.spriteFour,()=> {GameWin();});
            }
        }

        private void UpdateCoinText()
        {
            coinText.text = "Coins: " + collectedCoins.ToString();
        }

        private void UpdateTimeText()
        {
            timeText.text = "Time: " + Mathf.RoundToInt(timeLeft).ToString();
        }

        internal void GameWin()
        {
            isGameWin = true;
            cgGameWin.alpha = 1f;
            cgGameWin.blocksRaycasts = true;
            cgGameWin.interactable = true;
        }

        internal void GameLose()
        {
            isGameLose = true;
            cgGameLose.alpha = 1f;
            cgGameLose.blocksRaycasts = true;
            cgGameLose.interactable = true;
        }
    }
}
