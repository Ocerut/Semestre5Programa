using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class MovementController : NetworkBehaviour
{
    public CharacterController characterController;
    public Animator anim;
    public float speed;
    private bool ground;
    public float jump;
    private Vector3 velocity;
    public float gravity = -9.81f;
    public bool jumpPressed;

    [Networked] public int Score { get; set; }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_AddScore(int points)
    {
        Score += points;
    }

    public void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    public override void FixedUpdateNetwork()
    {
        if (ground && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (HasStateAuthority)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 direction = new Vector3(horizontal, 0, vertical);

            if (direction.magnitude > 0)
            {
                anim.SetBool("canWalk", true);

                characterController.Move(direction * speed * Runner.DeltaTime);
            }

            else
            {
                anim.SetBool("canWalk", false);
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                anim.SetBool("isRun", true);
                speed = 10f;
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                anim.SetBool("isRun", false);
                speed = 5f;
            }

            if (Input.GetKeyDown(KeyCode.Space) && ground)
            {
                velocity.y += jump;
                ground = false;
                anim.SetBool("isJump", true);
            }

            velocity.y += gravity * Runner.DeltaTime;
            characterController.Move(velocity * Runner.DeltaTime);
        }
    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Solid"))
        {
            ground = true;
        }
    }
}
