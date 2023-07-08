using UnityEngine;

public class CrouchObject : LaneObject
{
    [SerializeField] private float _crouchDuration;
    [SerializeField] private float _spawnOffset = 0.5f;

    public override void SetLane(SkaterLane lane)
    {
        base.SetLane(lane);
        transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + _spawnOffset);
    }

    public override void Activate(Skater skater)
    {
        base.Activate(skater);
        skater.Crouch(_crouchDuration);
    }
}
