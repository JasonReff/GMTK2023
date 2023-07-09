using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private AudioClip _recordScratch;
    private void Start()
    {
        Time.timeScale = 1f;
    }
    public void GameOver()
    {
        AudioManager.PlaySoundEffect(_recordScratch);
        Time.timeScale = 0f;
        _gameOverPanel.SetActive(true);
    }
}