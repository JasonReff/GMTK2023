using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TrapPool")]
public class TrapPool : ScriptableObject
{

    [SerializeField] private List<UsableTrap> _usableTraps = new List<UsableTrap>();
    public List<UsableTrap> LevelTraps { get => _usableTraps; }

    public class UsableTrap
    {
        public TrapData Trap;
        public int MaxUses;
    }
}