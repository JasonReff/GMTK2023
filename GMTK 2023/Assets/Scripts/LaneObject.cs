using System.Collections;
using UnityEngine;

public class LaneObject : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    public SkaterLane Lane;

    private void Update()
    {
        transform.localPosition = (Vector2)transform.localPosition + new Vector2(-_movementSpeed, 0);
    }
}
