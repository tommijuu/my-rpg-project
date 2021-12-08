using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{

    public bool isHovering;

    //public Transform player, target;

    //public float rotationSpeed = 5.0f;

    //public Quaternion lastFreeLookPos;

    //public GameObject freelookCam;

    //public BasicMovement basicMovement;

    //float mouseX, mouseY;
    public Vector2 _move;
    public Vector2 _look;
    public float aimValue;
    public float fireValue;

    public Vector3 nextPosition;
    public Quaternion nextRotation;

    public float rotationPower = 3f;
    public float rotationLerp = 0.5f;

    public float speed = 1f;
    public Camera camera;

    public GameObject followTransform;

    void Start()
    {
        //basicMovement = GameObject.Find("Player").GetComponent<BasicMovement>();
    }

    void Update()
    {

        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            Cursor.visible = false;
            if (Input.GetMouseButton(0))
            {
                transform.rotation *= Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationPower, Vector3.up);
            }
        }
        else
        {
            Cursor.visible = true;
        }
    }

    void LateUpdate()
    {
        //mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        //mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;

        //mouseY = Mathf.Clamp(mouseY, -35, 60);

        //transform.LookAt(target); //Target is the parent of the camera in player prefab

        ////Left click down
        //if (Input.GetMouseButton(0) && !isHovering) //The latter check is so that you cannot free look while press-hovering enemy (maybe changing later?)
        //{
        //    //freelookCam.SetActive(true);
        //    //if (lastFreeLookPos != null)
        //    //{
        //    //    target.rotation = lastFreeLookPos;
        //    //}

        //    target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        //}
        ////else
        ////{
        ////  freelookCam.SetActive(false);
        ////}
        ////else if (Input.GetMouseButtonUp(0))
        ////{
        ////    lastFreeLookPos = Quaternion.Euler(mouseY, mouseX, 0);
        ////}

        ////Right click down
        //if (Input.GetMouseButton(1))
        //{
        //    target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        //    player.rotation = Quaternion.Euler(0, mouseX, 0);
        //}
    }
}
