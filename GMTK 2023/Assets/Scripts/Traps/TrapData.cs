using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "TrapData")]
public class TrapData : ScriptableObject
{
    [SerializeField] private string _trapName;
    [SerializeField] private Sprite _trapSprite;
    [SerializeField] private int _trapCost;
    [SerializeField] private Trap _trapPrefab;
}
