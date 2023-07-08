using UnityEngine;

public class JumpObject : LaneObject
{
    [SerializeField] private float _jumpHeight, _jumpDuration;
    public override void Activate(Skater skater)
    {
        base.Activate(skater);
        skater.Jump(_jumpHeight, _jumpDuration);
    }
}
