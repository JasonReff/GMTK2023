using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private float _minimumZoom, _maximumZoom, _zoomSpeed;
    [SerializeField] private Camera _camera;

    private void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            ChangeZoom(_zoomSpeed);
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            ChangeZoom(-_zoomSpeed);
        }
    }

    private void ChangeZoom(float amount)
    {
        var newSize = _camera.orthographicSize + amount;
        if (newSize < _minimumZoom)
        {
            newSize = _minimumZoom;
        }
        if (newSize > _maximumZoom)
        {
            newSize = _maximumZoom;
        }
        _camera.orthographicSize = newSize;
    }
}
