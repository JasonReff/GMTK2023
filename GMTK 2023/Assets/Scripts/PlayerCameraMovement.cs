using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraMovement : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _movementSpeed = 1f, _cameraMovementSpeed = 0.1f;
    [SerializeField] private float _lowestBound, _highestBound, _leftestBound, _rightestBound;

    private void Update()
    {
        MovePosition(new Vector2(Input.GetAxisRaw("Horizontal") * _movementSpeed, Input.GetAxisRaw("Vertical") * _movementSpeed));
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
}
