using System.Collections.Generic;
using UnityEngine;

public class Skater : MonoBehaviour
{
    [SerializeField] private BlurrableObject _blurrable;
    [SerializeField] private SkaterLane _currentLane;
    [SerializeField] private float _speedLoss = 1f, _pumpRate = 2f, _pumpForce = 5f, _moveRate = 10f;
    [SerializeField] private List<float> _pumpRates = new List<float>();
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private SkaterAnimations _animations;
    private float _pumpTimer, _moveTimer;
    private List<float> _ratesSeen = new List<float>();

    public void MoveToLane(LaneTransition transition)
    {
        _currentLane = transition.Lane2;
        _blurrable.ChangeLayer(_currentLane.FocusLayer);
    }

    private void Start()
    {
        ChangeSpeed();
        _pumpTimer = _pumpRate;
        _moveTimer = _moveRate;
    }

    private void Update()
    {
        _pumpTimer -= Time.deltaTime;
        _moveTimer -= Time.deltaTime;
        if (_pumpTimer < 0)
        {
            Pump();
            _pumpTimer = _pumpRate;
        }
        if (_moveTimer < 0)
        {
            ChangeSpeed();
            _moveTimer = _moveRate;
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.AddForce(new Vector2(-_speedLoss, 0f), ForceMode2D.Impulse);
    }

    public void Pump()
    {
        _rigidbody.AddForce(new Vector2(_pumpForce, 0f), ForceMode2D.Impulse);
        _animations.Pump();
    }

    public void ChangeSpeed()
    {
        var speeds = new List<float>(_pumpRates);
        if (_ratesSeen.Count == speeds.Count)
            _ratesSeen.Clear();
        foreach (var rate in _ratesSeen)
        {
            speeds.Remove(rate);
        }
        var newSpeed = speeds.Rand();
        _ratesSeen.Add(newSpeed);
        _pumpRate = newSpeed;
    }
}
