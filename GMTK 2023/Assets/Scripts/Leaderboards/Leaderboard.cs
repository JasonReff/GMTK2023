using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dan.Main;
using TMPro;
using Dan.Models;
using System;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private string _leaderboardPublicKey = "0f9611374f45d16c5e2b3d60d78f856f0788670429e542880e9019977f50d66d";
    [SerializeField] private TextMeshProUGUI _playerScoreText;
    [SerializeField] private TMP_InputField _usernameInput;
    [SerializeField] private List<TextMeshProUGUI> _entryFields;
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private GameObject _input, _submitting;



    public void Load()
    {
        _playerScoreText.text = $"{_scoreManager.Score}";
        _usernameInput.text = "AAA";
        LeaderboardCreator.GetLeaderboard(_leaderboardPublicKey, (entries) => {
            foreach (var entryField in _entryFields)
                entryField.text = "";
            for (int i = 0; i < entries.Length; i++)
            {
                Entry entry = entries[i];
                _entryFields[i].text = $"{i + 1}. {entry.Username}: {entry.Score}";
            }
        });
    }

    public void OnLeaderboardLoaded(Entry[] entries)
    {
        foreach (var entryField in _entryFields)
            entryField.text = "";
        for (int i = 0; i < entries.Length; i++)
        {
            Entry entry = entries[i];
            _entryFields[i].text = $"{i + 1}. {entry.Username}: {entry.Score}";
        }
    }

    public void Submit()
    {
        _submitting.SetActive(true);
        _input.SetActive(false);
        if (_usernameInput.text == "")
            return;
        LeaderboardCreator.UploadNewEntry(_leaderboardPublicKey, _usernameInput.text.ToUpper(), _scoreManager.Score, (success) => {
            if (success)
            {
                _submitting.SetActive(false);
                Load();
            }
            else
            {
                _submitting.SetActive(false);
                _input.SetActive(true);
            }
        });
        
    }

    public void OnUploadComplete(bool success)
    {
        _submitting.SetActive(false);
        if (success)
        {
            Load();
        }
    }
}
