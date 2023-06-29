using TMPro;
using UnityEngine;
using Utilities;

namespace Ui.ScoreSystem
{
    public class ScoreManager : MonoSingleton<ScoreManager>
    {
        [SerializeField] private TextMeshProUGUI scoreTextMeshProUGUI;
        [SerializeField] private TextMeshProUGUI maxScoreTextMeshProUGUI;
        
        private int _score;
        private int _maxScore;
        
        void Start()
        {
            ResetScore();
        }

        internal void ResetScore()
        {
            _maxScore = 0;
            _score = 0;
            scoreTextMeshProUGUI.text = _score.ToString();
            maxScoreTextMeshProUGUI.text = _maxScore.ToString();
        }

        internal void UpdateScore(int score ,int maxScore)
        {
        
            _maxScore = _maxScore + maxScore;
            _score =  _score + score;
            scoreTextMeshProUGUI.text = _score.ToString();
            maxScoreTextMeshProUGUI.text = _maxScore.ToString();

        }

        internal int GetScore()
        {
            return _score;
        }
        internal int GetMaxScore()
        {
            return _maxScore;
        }
        
        internal float ScoreInPercent()
        {
           return (_score * 100f) / _maxScore;
        }
    
    }
}
