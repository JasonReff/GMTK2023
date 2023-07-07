using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "TrapData")]
public class TrapData : ScriptableObject
{
    [SerializeField] private string _trapName;
    [SerializeField] private Sprite _trapSprite;
    [SerializeField] private int _trapCost;
    [SerializeField] private Trap _trapPrefab;

    public string TrapName { get => _trapName; set => _trapName = value; }
    public Sprite TrapSprite { get => _trapSprite; set => _trapSprite = value; }
    public int TrapCost { get => _trapCost; set => _trapCost = value; }
    public Trap TrapPrefab { get => _trapPrefab; set => _trapPrefab = value; }
}
