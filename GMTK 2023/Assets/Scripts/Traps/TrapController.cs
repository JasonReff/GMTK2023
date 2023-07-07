using UnityEngine;

public class TrapController : MonoBehaviour
{
    [SerializeField] private Trap _controlledTrap;

    public void ControlTrap(Trap newTrap)
    {
        _controlledTrap = newTrap;
    }
}