using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerCameraMovement : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _swayTransform;
    [SerializeField] private float _movementSpeed = 1f, _cameraMovementSpeed = 0.1f, _driftSpeed = 0.01f, _swayAmplitude, _swayFrequency;
    [SerializeField] private float _lowestBound, _highestBound, _leftestBound, _rightestBound;
    [SerializeField] private Vector2 _sway;
    private float _shakeTimer = 0f;

    private void Update()
    {
        MovePosition(new Vector2(Input.GetAxisRaw("Horizontal") * _movementSpeed * Time.deltaTime, Input.GetAxisRaw("Vertical") * _movementSpeed * Time.deltaTime));
        CameraDrift();
        CameraSway();
    }

    private void MovePosition(Vector2 movement)
    {
        var newPosition = (Vector2)transform.localPosition + movement;
        if (newPosition.y < _lowestBound)
        {
            newPosition.y = _lowestBound;
        }
        if (newPosition.y > _highestBound)
        {
            newPosition.y = _highestBound;
        }
        if (newPosition.x < _leftestBound)
        {
            newPosition.x = _leftestBound;
        }
        if (newPosition.x > _rightestBound)
        {
            newPosition.x = _rightestBound;
        }
        transform.localPosition = newPosition;
    }

    private void CameraDrift()
    {
        transform.localPosition = transform.localPosition - transform.localPosition * _driftSpeed * Time.deltaTime;
    }

    private void CameraSway()
    {
        _sway = new Vector2((Mathf.Sin(Time.time * _swayFrequency) * _swayAmplitude), Mathf.Sin((Time.time * _swayFrequency + 1) * _swayAmplitude));
        _swayTransform.localPosition = _sway;
    }
}
