using UnityEngine;

public class Guard : Trap
{
    [SerializeField] private PlayerMovementController _movementController;
    public override void ControlTrap()
    {
        base.ControlTrap();
        _movementController.enabled = true;
    }
}