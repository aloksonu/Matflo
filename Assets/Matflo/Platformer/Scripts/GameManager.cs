using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public CanvasGroup cgGameWin;
    public CanvasGroup cgGameLoose;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI timeText;
    public float timeLimit = 60f;
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
            EndGame(cgGameLoose);
        }
    }

    public void CollectCoin()
    {
        collectedCoins++;
        UpdateCoinText();

        if (collectedCoins >= 6)
        {
            EndGame(cgGameWin);
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

    private void EndGame(CanvasGroup cg)
    {
        // Display the end game message
        cg.alpha = 1f;
        cg.blocksRaycasts = true;
        cg.interactable = true;
    }
}
