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

        //Player character was sliding because of terrain while using transform to move so I added additional checks that seem to fix the issue.
        //Another workaround is to straight move through RigidBody but doing that would mean editing the camera system as it uses transform too.

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            transform.Translate(xMovement * moveSpeed * Time.deltaTime, 0, zMovement * moveSpeed * Time.deltaTime);
        }
        else if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            xMovement = 0;
            zMovement = 0;
        }

        //The mentioned RigidBody movement script:

        //Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        //_rb.MovePosition(transform.position + movement * Time.deltaTime * moveSpeed);


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
