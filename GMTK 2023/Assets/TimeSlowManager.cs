using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlowManager : MonoBehaviour
{
    [SerializeField] private float _slowTime = 0.5f, _regularTime = 1f;

    private void OnEnable()
    {
        Skater.OnTrick += OnTrickStart;
        Skater.OnTrickEnd += OnTrickEnd;
    }

    private void OnDisable()
    {
        Skater.OnTrick -= OnTrickStart;
        Skater.OnTrickEnd -= OnTrickEnd;
    }

    private void OnTrickStart()
    {
        Time.timeScale = _slowTime;
    }

    private void OnTrickEnd()
    {
        Time.timeScale = _regularTime;
    }
}
