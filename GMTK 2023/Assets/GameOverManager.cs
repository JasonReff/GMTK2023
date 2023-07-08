using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverPanel;
    private void Start()
    {
        Time.timeScale = 1f;
    }
    public void GameOver()
    {
        Time.timeScale = 0f;
        _gameOverPanel.SetActive(true);
    }
}