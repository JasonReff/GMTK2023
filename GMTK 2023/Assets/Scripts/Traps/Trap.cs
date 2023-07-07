using UnityEngine;

public abstract class Trap : MonoBehaviour
{
    [SerializeField] private TrapData _trapData;
    public virtual void SetupTrap()
    {

    }

    public virtual void ControlTrap()
    {

    }
}
