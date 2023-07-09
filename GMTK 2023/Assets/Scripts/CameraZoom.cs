using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private float _minimumZoom, _maximumZoom, _zoomSpeed;
    [SerializeField] private Camera _camera;
    [SerializeField] private BoxCollider2D _collider;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            ChangeZoom(_zoomSpeed);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            ChangeZoom(-_zoomSpeed);
        }
    }

    private void ChangeZoom(float amount)
    {
        var newSize = _camera.orthographicSize + amount;
        if (newSize < _minimumZoom)
        {
            amount = 0;
            newSize = _minimumZoom;
        }
        if (newSize > _maximumZoom)
        {
            amount = 0;
            newSize = _maximumZoom;
        }
        _camera.orthographicSize = newSize;
        _collider.size = _collider.size - new Vector2(amount, amount);
    }

    public float ZoomLevel()
    {
        return 1 /_camera.orthographicSize;
    }
}
