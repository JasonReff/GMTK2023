using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampObject : LaneObject
{
    [SerializeField] private AnimationCurve _movementCurve, _rotationCurve;
    [SerializeField] private float _height, _duration;

    public override void Activate(Skater skater)
    {
        base.Activate(skater);
        skater.RampJump(_movementCurve, _rotationCurve, _height, _duration, Lane);
    }
}
