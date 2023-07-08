using UnityEngine;

public class GrindRail : LaneObject
{
    public SkaterLane Lane2;
    public LaneDirection Direction;
    [SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] private float _height, _duration;

    public override void SetLane(SkaterLane lane)
    {
        base.SetLane(lane);
        switch (Direction)
        {
            case LaneDirection.Up:
                Lane2 = lane.UpLane;
                break;
            case LaneDirection.Down:
                Lane2 = lane.DownLane;
                break;
            case LaneDirection.Straight:
                Lane2 = lane;
                break;
        }
    }

    public override void Activate(Skater skater)
    {
        base.Activate(skater);
        skater.Grind(_animationCurve, _height, _duration, Lane2);
    }

}

public enum LaneDirection
{
    Up,
    Down,
    Straight
}