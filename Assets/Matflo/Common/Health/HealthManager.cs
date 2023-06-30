using TMPro;
using UnityEngine;
using Utilities;

public class HealthManager : MonoSingleton<HealthManager>
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private TextMeshProUGUI healthTextMeshProUGUI;
    private float _fadeDuration = 0.2f;
    private int maxHealth;
    private int leftHealth;
    void Start()
    {
        maxHealth = 20;
        ResetHealth();
    }
    internal void BringIn()
    {
        _canvasGroup.UpdateState(true, _fadeDuration);
    }
    internal void BringOut()
    {
        _canvasGroup.UpdateState(false, _fadeDuration);
    }

    internal void ResetHealth()
    {
        leftHealth = maxHealth;
        healthTextMeshProUGUI.text = "Lives: " + leftHealth.ToString();
    }
    internal void UpdateHealth(int s)
    {

        leftHealth -= s;
        healthTextMeshProUGUI.text = "Lives: " + leftHealth.ToString();

    }

    internal int GetHealth()
    {
        return leftHealth;
    }
}
