using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public Button btnLevel;
    public LevelName levelName;
    void Start()
    {
        btnLevel.onClick.AddListener(OnClickLeveButton);
    }

    private void OnClickLeveButton()
    {
        LevelPanel.Instance.levelName = levelName;
        LevelPanel.Instance.OnContinueButtonPressed(levelName.ToString());
    }
}