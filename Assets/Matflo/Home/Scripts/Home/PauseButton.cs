using UnityEngine;
using UnityEngine.UI;

namespace Matflo.Home.Scripts.Home
{
    public class PauseButton : MonoBehaviour
    {
        [SerializeField] private Button btnPause;
        void Start()
        {
            btnPause.onClick.AddListener(OnClickPauseButton);
        }
        internal void OnClickPauseButton()
        {
            PausePanel.Instance.OnClickPauseButton();
        }
    }
}
