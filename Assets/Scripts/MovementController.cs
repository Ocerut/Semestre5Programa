using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class MovementController : NetworkBehaviour
{
    public CharacterController characterController;

    public void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    public override void FixedUpdateNetwork()
    {
        if (HasStateAuthority)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 direction = new Vector3(horizontal, 0, vertical);

            if (direction.magnitude > 0)
            {
                characterController.Move(direction);
            }
        }
    }
}
