using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public float moveSpeed;
    public float rotateSpeed;
    public float jumpForce;

    //Better jump multipliers
    public float fallMultiplier = 2.5f;
    public float jumpMultiplier = 2.5f;

    public bool grounded = true;

    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) { grounded = true; }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) { grounded = false; }
    }

    void Update()
    {
        //Better jumping
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (jumpMultiplier - 1) * Time.deltaTime;
        }

        // Moving player wasd
        float xMovement = Input.GetAxis("Horizontal");
        float zMovement = Input.GetAxis("Vertical");
        transform.Translate(xMovement * moveSpeed * Time.deltaTime, 0, zMovement * moveSpeed * Time.deltaTime);

        // Jumping

        if (rb.velocity.y == 0) { grounded = true; }

        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb.velocity = Vector3.up * jumpForce;
        }

    }
}
