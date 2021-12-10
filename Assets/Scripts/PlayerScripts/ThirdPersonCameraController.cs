using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour
{
    //This script is for the camera to be MMO-like, like in World of Warcraft.
    //Holding right click is free look, so it rotates the camera around the player
    //Holding left click turns the player and the camera, so player's moving direction can be controlled with mouse
    //Scroll implemented too

    public Transform player, target; //Player transform is to rotate the player (left click) and target is a point inside the player where the camera focuses (both clicks)

    public float targetHeigth = 1.7f; //Vertical offset adjustment
    public float distance = 12f; //Default Distance

    public float offsetFromWall = 0.1f; //Bring camera away from any colliding objects
    public float maxDistance = 20; //Maximum zoom Distance

    public float minDistance = 0.6f; //Minimum zoom Distance

    public float xSpeed = 200; //Orbit speed (Left/Right)
    public float ySpeed = 200; //Orbit speed (Up/Down)

    public float yMinLimit = -80; //Looking up limit
    public float yMaxLimit = 80; //Looking down limit

    public float zoomRate = 40; //Zoom Speed
    public float rotationDampening = 3.0f; //Auto Rotation speed (higher = faster)
    public float zoomDampening = 5.0f; //Auto Zoom speed (Higher = faster)

    public LayerMask collisionLayers = -1; //What the camera will collide with

    public bool lockToRearOfTarget = false; //Lock camera to rear of target
    public bool allowMouseInputX = true; //Allow player to control camera angle on the X axis (Left/Right)
    public bool allowMouseInputY = true; //Allow player to control camera angle on the Y axis (Up/Down)

    private float _xDeg = 0.0f;
    private float _yDeg = 0.0f;

    //For scroll zoom
    private float _currentDistance;
    private float _desiredDistance;
    private float _correctedDistance;

    private bool _rotateBehind = false;

    private bool _isCorrected = false;

    private RaycastHit _collisionHit;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        _xDeg = angles.x;
        _yDeg = angles.y;
        _currentDistance = distance;
        _desiredDistance = distance;
        _correctedDistance = distance;

        if (lockToRearOfTarget)
            _rotateBehind = true;
    }

    void LateUpdate()
    {
        Vector3 vTargetOffSet;

        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            Cursor.visible = false;
            if (allowMouseInputX)
            {
                _xDeg += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            }
            else
            {
                RotateBehindTarget();
            }

            if (allowMouseInputY)
            {
                _yDeg -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
            }
        }
        else
        {
            Cursor.visible = true;
        }

        _yDeg = ClampAngle(_yDeg, yMinLimit, yMaxLimit);

        //set camera rotation
        Quaternion rotation = Quaternion.Euler(_yDeg, _xDeg, 0);

        //If right clicked, rotate the player too
        if (Input.GetMouseButton(1))
            player.rotation = Quaternion.Euler(0, _xDeg, 0);


        _desiredDistance -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * zoomRate * Mathf.Abs(_desiredDistance);
        _desiredDistance = Mathf.Clamp(_desiredDistance, minDistance, maxDistance);
        _correctedDistance = _desiredDistance;

        //Desired camera position calculation
        vTargetOffSet = new Vector3(0, -targetHeigth, 0);
        Vector3 position = target.position - (rotation * Vector3.forward * _desiredDistance + vTargetOffSet);


        Vector3 trueTargetPosition = new Vector3(target.position.x, target.position.y + targetHeigth, target.position.z);


        // If there was a collision, correct the camera position and calculate the corrected distance 
        if (Physics.Linecast(trueTargetPosition, position, collisionLayers, QueryTriggerInteraction.Ignore)) //collisionHit missing here atm!!!!
        {
            _correctedDistance = Vector3.Distance(trueTargetPosition, _collisionHit.point) - offsetFromWall;
            _isCorrected = true;
        }

        //For smoothing, lerp distance only if either distance wasn't corrected, or correctedDistance is more than currentDistance 
        _currentDistance = !_isCorrected || _correctedDistance > _currentDistance ? Mathf.Lerp(_currentDistance, _correctedDistance,
            Time.deltaTime * zoomDampening) : _correctedDistance;

        //Keep within limits
        _currentDistance = Mathf.Clamp(_currentDistance, minDistance, maxDistance);

        //Recalculate position based on the new currentDistance
        position = target.position - (rotation * Vector3.forward * _currentDistance + vTargetOffSet);

        //Set Camera rotation and position
        transform.rotation = rotation;
        transform.position = position;
    }


    void RotateBehindTarget()
    {
        float targetRotationAngle = target.eulerAngles.y;
        float currentRotationAngle = transform.eulerAngles.y;
        _xDeg = Mathf.LerpAngle(currentRotationAngle, targetRotationAngle, rotationDampening * Time.deltaTime);

        if (targetRotationAngle == currentRotationAngle)
        {
            if (!lockToRearOfTarget)
                _rotateBehind = false;
        }
        else
        {
            _rotateBehind = true;
        }
    }

    float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
}
