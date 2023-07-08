using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LaneObjectPool")]
public class LaneObjectPool : ScriptableObject
{
    public List<LaneObject> FunctionalObjects = new List<LaneObject>();
    public List<LaneObject> DecorativeObjects = new List<LaneObject>();

    public LaneObject RandomObject()
    {
        var objects = new List<LaneObject>(FunctionalObjects);
        objects.AddRange(DecorativeObjects);
        return objects.Rand();
    }
}