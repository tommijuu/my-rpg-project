using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    private Rigidbody _rb;

    public float moveSpeed;
    public float rotateSpeed;
    public float jumpForce;

    //Better jump multipliers
    public float fallMultiplier = 2.5f;
    public float jumpMultiplier = 2.5f;

    public bool grounded = true;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
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
        //Moving
        float xMovement = Input.GetAxis("Horizontal");
        float zMovement = Input.GetAxis("Vertical");
        transform.Translate(xMovement * moveSpeed * Time.deltaTime, 0, zMovement * moveSpeed * Time.deltaTime);

        //Jumping math
        if (_rb.velocity.y < 0)
        {
            _rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (_rb.velocity.y > 0)
        {
            _rb.velocity += Vector3.up * Physics.gravity.y * (jumpMultiplier - 1) * Time.deltaTime;
        }

        if (_rb.velocity.y == 0) { grounded = true; }

        //Jumping
        if (Input.GetButtonDown("Jump") && grounded)
        {
            _rb.velocity = Vector3.up * jumpForce;
        }
    }
}
