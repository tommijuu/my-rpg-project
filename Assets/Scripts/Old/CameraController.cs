using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public Transform player, target;
    public Quaternion previousFreeLookPos;
    public GameObject freelookCam;
    public PlayerMovementController playerMovementController;

    public float rotationSpeed = 5.0f;
    public float zoomAmount = 8f;
    public bool isHovering;

    private float _mouseX, _mouseY;


    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            Cursor.visible = false;
        }
        else
        {
            previousFreeLookPos = target.rotation;
            Cursor.visible = true;
        }
    }

    void LateUpdate()
    {
        //Vector3 direction = previousFreeLookPos - GetComponent<Camera>().ScreenToViewportPoint(Input.mousePosition);

        Vector3 previousFreeLookPosVector = previousFreeLookPos * Vector3.forward;

        _mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        _mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;

        _mouseY = Mathf.Clamp(_mouseY, -35, 60);

        transform.LookAt(target); //Target is the parent of the camera in player prefab

        //Left click
        if (Input.GetMouseButton(0) && !isHovering) //The latter check is so that you cannot free look while press-hovering enemy (maybe changing later?)
        {
            target.rotation = Quaternion.Euler(_mouseY, _mouseX, 0);
        }

        //Right click 
        if (Input.GetMouseButton(1))
        {
            target.rotation = Quaternion.Euler(_mouseY, _mouseX, 0);
            player.rotation = Quaternion.Euler(0, _mouseX, 0);
        }
    }
}
