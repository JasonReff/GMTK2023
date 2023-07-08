using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraMovement : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _movementSpeed = 1f, _cameraMovementSpeed = 0.1f;

    private void Update()
    {
        MovePosition(new Vector2(Input.GetAxisRaw("Horizontal") * _movementSpeed, Input.GetAxisRaw("Vertical") * _movementSpeed));
    }

    private void MovePosition(Vector2 movement)
    {
        transform.localPosition = (Vector2)transform.localPosition + movement;
        //_camera.transform.localPosition = (Vector2)_camera.transform.localPosition + movement;
    }
}
