using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkaterLane : MonoBehaviour
{
    [SerializeField] private int _focusLayer;

    public int FocusLayer { get => _focusLayer; }
}
