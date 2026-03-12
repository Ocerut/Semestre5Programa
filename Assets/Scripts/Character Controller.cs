using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    Animator anim;
    private float movX;
    private float movZ;
    Vector3 direction;
    public float speed;
    public float rotSpeed;
    Rigidbody rb;
    private bool ground;
    public float jump;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        movX = Input.GetAxis("Horizontal");
        movZ = Input.GetAxis("Vertical");
        direction = new Vector3(movX, 0, movZ);
        transform.Translate(direction * Time.deltaTime * speed);
        transform.Rotate(new Vector3(0, movX, 0) * rotSpeed);
        if (movX != 0 || movZ != 0)
        {
            anim.SetBool("canWalk", true);
        }
        else if (movX == 0 || movZ == 0)
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (ground)
            {
                rb.AddForce(Vector3.up * jump, ForceMode.Impulse);
                ground = false;
                anim.SetBool("isJump", true);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Solid")
        {
            ground = true;
            anim.SetBool("isJump", false);
        }
    }
}
