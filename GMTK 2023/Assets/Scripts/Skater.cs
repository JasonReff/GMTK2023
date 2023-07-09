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
    [SerializeField] private float _lossFactorM, _lossFactorC;
    [SerializeField] private AnimationCurve _jumpCurve;
    [SerializeField] private Material _focusMaterial;
    private float _pumpTimer, _moveTimer;
    private List<float> _ratesSeen = new List<float>();
    public bool IsDoingTrick;

    public void MoveToLane(SkaterLane lane)
    {
        _currentLane = lane;
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
        if (IsDoingTrick)
            return;
        _pumpTimer -= Time.deltaTime;
        _moveTimer -= Time.deltaTime;
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
            var time = 0f;
            while (time < duration)
            {
                transform.localPosition = new Vector2(transform.localPosition.x, baseHeight + height * _jumpCurve.Evaluate(time/duration));
                time += Time.deltaTime;
                yield return null;
            }
            IsDoingTrick = false;
            _rigidbody.drag = 5;
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
            var time = 0f;
            _animations.StartGrind();
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
            var time = 0f;
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
}
