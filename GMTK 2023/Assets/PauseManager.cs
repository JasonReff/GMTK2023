using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject _pauseCanvas;
    public void Pause()
    {
        Time.timeScale = 0;
        _pauseCanvas.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        _pauseCanvas.SetActive(false);
    }
}
