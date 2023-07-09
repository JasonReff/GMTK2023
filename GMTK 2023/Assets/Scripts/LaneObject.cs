using System.Collections;
using UnityEngine;

public class LaneObject : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    public SkaterLane Lane;

    private void Update()
    {
        transform.localPosition = (Vector2)transform.localPosition + new Vector2(-_movementSpeed, 0) * Time.deltaTime;
    }

    public virtual void SetLane(SkaterLane lane)
    {
        Lane = lane;
        var blurrable = GetComponent<BlurrableObject>();
        blurrable.SetLayer(Lane.FocusLayer);
        GetComponent<SpriteRenderer>().sortingOrder = lane.SortingLayer;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Skater skater))
        {
            if (skater.GetComponent<BlurrableObject>().FocusLayer == Lane.FocusLayer && !skater.IsDoingTrick)
                Activate(skater);
        }
        else if (collision.TryGetComponent(out ObjectDespawner despawner))
        {
            Destroy(gameObject);
        }
    }

    public virtual void Activate(Skater skater)
    {
        
    }
}
