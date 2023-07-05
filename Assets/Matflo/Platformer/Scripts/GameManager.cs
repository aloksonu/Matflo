using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

        if (collectedCoins >= playerController.totalSteps)
        {
            GameWin();
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
        cgGameWin.alpha = 1f;
        cgGameWin.blocksRaycasts = true;
        cgGameWin.interactable = true;
    }

    internal void GameLose()
    {
        cgGameLose.alpha = 1f;
        cgGameLose.blocksRaycasts = true;
        cgGameLose.interactable = true;
    }
}
