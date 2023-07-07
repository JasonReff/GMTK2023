using System;
using UnityEngine;

public class PlaceableTile : MonoBehaviour
{
    public static event Action<PlaceableTile> OnTileSelected;
    public void SelectTile()
    {
        OnTileSelected?.Invoke(this);
    }
}