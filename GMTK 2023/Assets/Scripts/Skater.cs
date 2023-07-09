using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skater : MonoBehaviour
{
    [SerializeField] private BlurrableObject _blurrable;
    [SerializeField] private SkaterLane _currentLane;
    [SerializeField] private float _speedLoss = 1f, _pumpRate = 2f, _pumpForce = 5f, _moveRate = 10f, _trickBoost = 5f;
    [SerializeField] private List<float> _pumpRates = new List<float>();
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private SkaterAnimations _animations;
    [Header("Movement")]
    [SerializeField] private float _setLossRate = 5f;
    [SerializeField] private float _lossFactorM, _lossFactorC;
    [SerializeField] private List<float> _horizontalPositions;
    private List<float> _positionsSeen = new List<float>();
    [Space(5)]
    [SerializeField] private AnimationCurve _jumpCurve;
    [SerializeField] private Material _focusMaterial;
    [Header("SoundEffects")]
    [SerializeField] private AudioClip _skateSound, _jumpSound, _landSound, _grindSound, _pumpSound;
    private float _pumpTimer, _moveTimer, _lossTimer;
    private List<float> _ratesSeen = new List<float>();
    public bool IsDoingTrick;
    public static event Action OnTrick;

    public void MoveToLane(SkaterLane lane)
    {
        _currentLane = lane;
        _blurrable.ChangeLayer(_currentLane.FocusLayer);
    }

    private void Start()
    {
        AudioManager.ToggleSkateboard(true);
        ChangeSpeed();
        ChangePosition();
        _pumpTimer = _pumpRate;
        _moveTimer = _moveRate;
        _lossTimer = _setLossRate;
    }

    private void OnDisable()
    {
        AudioManager.ToggleSkateboard(false);
    }

    private void Update()
    {
        if (IsDoingTrick)
            return;
        _pumpTimer -= Time.deltaTime;
        _moveTimer -= Time.deltaTime;
        _lossTimer -= Time.deltaTime;
        SetSpeedLoss();
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
        if (_lossTimer < 0)
        {
            ChangePosition();
            _lossTimer = _setLossRate;
        }
    }

    private void SetSpeedLoss()
    {
        var horizontal = transform.localPosition.x;
        var speedLoss = horizontal * _lossFactorM + _lossFactorC;
        _speedLoss = speedLoss;
    }

    private void FixedUpdate()
    {
        if (IsDoingTrick)
            return;
        _rigidbody.AddForce(new Vector2(-_speedLoss, 0f), ForceMode2D.Impulse);
    }

    public void Pump()
    {
        AudioManager.PlaySoundEffect(_pumpSound);
        _rigidbody.AddForce(new Vector2(_pumpForce, 0f), ForceMode2D.Impulse);
        _animations.Pump();
    }

    public void Jump(float height, float duration)
    {
        _rigidbody.AddForce(new Vector2(_trickBoost, 0f), ForceMode2D.Impulse);
        var baseHeight = transform.localPosition.y;
        StartCoroutine(JumpCoroutine());
        _animations.Jump(height >= 2f);

        IEnumerator JumpCoroutine()
        {
            _rigidbody.drag = 2;
            IsDoingTrick = true;
            OnTrick?.Invoke();
            AudioManager.ToggleSkateboard(false);
            AudioManager.PlaySoundEffect(_jumpSound);
            var time = 0f;
            while (time < duration)
            {
                transform.localPosition = new Vector2(transform.localPosition.x, baseHeight + height * _jumpCurve.Evaluate(time/duration));
                time += Time.deltaTime;
                yield return null;
            }
            IsDoingTrick = false;
            _rigidbody.drag = 5;
            AudioManager.PlaySoundEffect(_landSound);
            AudioManager.ToggleSkateboard(true);
        }
    }

    public void Crouch(float duration)
    {
        _rigidbody.AddForce(new Vector2(_trickBoost, 0f), ForceMode2D.Impulse);
        StartCoroutine(CrouchCoroutine());
        _animations.Crouch();
        
        IEnumerator CrouchCoroutine()
        {
            IsDoingTrick = true;
            OnTrick?.Invoke();
            yield return new WaitForSeconds(duration);
            IsDoingTrick = false;
        }
    }

    public void Grind(AnimationCurve curve, float height, float duration, SkaterLane nextLane)
    {
        var baseHeight = transform.localPosition.y;
        StartCoroutine(GrindCoroutine());

        IEnumerator GrindCoroutine()
        {
            var switchedLanes = false;
            bool endingGrind = false;
            IsDoingTrick = true;
            OnTrick?.Invoke();
            var time = 0f;
            _animations.StartGrind();
            AudioManager.ToggleSkateboard(false);
            AudioManager.PlaySoundEffect(_grindSound);
            while (time < duration)
            {
                if (time > duration / 2 && switchedLanes == false)
                {
                    MoveToLane(nextLane);
                    switchedLanes = true;
                }
                if (time > duration * .75 && endingGrind == false)
                {
                    _animations.EndGrind();
                    endingGrind = true;
                }
                transform.localPosition = new Vector2(transform.localPosition.x, baseHeight + height * curve.Evaluate(time / duration));
                time += Time.deltaTime;
                yield return null;
            }
            AudioManager.ToggleSkateboard(true);
            IsDoingTrick = false;
        }
    }

    public void RampJump(AnimationCurve movementCurve, AnimationCurve rotationCurve, float height, float duration, SkaterLane nextLane)
    {
        var baseHeight = transform.localPosition.y;
        StartCoroutine(RampCoroutine());

        IEnumerator RampCoroutine()
        {
            var switchedLanes = false;
            bool endingRamp = false;
            IsDoingTrick = true;
            OnTrick?.Invoke();
            var time = 0f;
            AudioManager.ToggleSkateboard(false);
            AudioManager.PlaySoundEffect(_jumpSound);
            _animations.StartRamp();
            while (time < duration)
            {
                if (time > duration / 2 && switchedLanes == false)
                {
                    MoveToLane(nextLane);
                    switchedLanes = true;
                }
                if (time > duration * .75 && endingRamp == false)
                {
                    _animations.EndRamp();
                    endingRamp = true;
                }
                transform.localPosition = new Vector2(transform.localPosition.x, baseHeight + height * movementCurve.Evaluate(time / duration));
                transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90 * rotationCurve.Evaluate(time / duration)));
                time += Time.deltaTime;
                yield return null;
            }
            AudioManager.PlaySoundEffect(_landSound);
            AudioManager.ToggleSkateboard(true);
            IsDoingTrick = false;
        }
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

    public void ChangePosition()
    {
        var positions = new List<float>(_horizontalPositions);
        if (_positionsSeen.Count == positions.Count)
            _positionsSeen.Clear();
        foreach (var position in _positionsSeen)
        {
            positions.Remove(position);
        }
        var newPosition = positions.Rand();
        _positionsSeen.Add(newPosition);
        _lossFactorC = newPosition;
    }
}
