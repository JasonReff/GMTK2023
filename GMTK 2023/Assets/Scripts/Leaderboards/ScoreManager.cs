using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private ScoreKeeper _scoreKeeper;
    [SerializeField] private float _score = 0, _highScore = 0;
    [SerializeField] private TextMeshProUGUI _scoreTextbox, _highScoreTextbox, _endScoreTextbox, _endHighScore;
    public int Score { get => (int)_score; set => _score = value; }

    private void Start()
    {
        _score = 0;
        _highScore = _scoreKeeper.HighScore;
    }

    private void Update()
    {
        _scoreTextbox.text = $"Score: {Score}";
        _highScoreTextbox.text = $"Record: {(int)_highScore}";
        _endScoreTextbox.text = $"Score: {Score}";
        _endHighScore.text = $"Record: {(int)_highScore}";
    }

    public void AddScore(float score)
    {
        _score += score;
        if (_score > _highScore)
        {
            _highScore = _score;
            _scoreKeeper.SetHighScore((int)_highScore);
        }
    }
}