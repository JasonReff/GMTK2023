using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private Rigidbody2D _rigidbody;

    public virtual void Move(Vector2 direction)
    {
        _rigidbody.AddForce(direction.normalized * _movementSpeed);
    }
}
