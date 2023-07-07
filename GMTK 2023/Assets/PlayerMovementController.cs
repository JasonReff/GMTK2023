using UnityEngine;

public class PlayerMovementController : MovementController
{
    private void Update()
    {
        Move(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
    }
}
