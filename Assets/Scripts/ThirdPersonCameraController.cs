using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour
{
    public Transform player, target; //Target to follow

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

    private float currentDistance;
    private float desiredDistance;
    private float correctedDistance;

    private bool rotateBehind = false;

    private bool isCorrected = false;

    private RaycastHit collisionHit;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        _xDeg = angles.x;
        _yDeg = angles.y;
        currentDistance = distance;
        desiredDistance = distance;
        correctedDistance = distance;

        if (lockToRearOfTarget)
            rotateBehind = true;
    }

    // Update is called once per frame
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


        desiredDistance -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * zoomRate * Mathf.Abs(desiredDistance);
        desiredDistance = Mathf.Clamp(desiredDistance, minDistance, maxDistance);
        correctedDistance = desiredDistance;

        //Desired camera position calculation
        vTargetOffSet = new Vector3(0, -targetHeigth, 0);
        Vector3 position = target.position - (rotation * Vector3.forward * desiredDistance + vTargetOffSet);


        Vector3 trueTargetPosition = new Vector3(target.position.x, target.position.y + targetHeigth, target.position.z);


        // If there was a collision, correct the camera position and calculate the corrected distance 
        if (Physics.Linecast(trueTargetPosition, position, collisionLayers, QueryTriggerInteraction.Ignore)) //collisionHit missing here atm!!!!
        {
            correctedDistance = Vector3.Distance(trueTargetPosition, collisionHit.point) - offsetFromWall;
            isCorrected = true;
        }

        //For smoothing, lerp distance only if either distance wasn't corrected, or correctedDistance is more than currentDistance 
        currentDistance = !isCorrected || correctedDistance > currentDistance ? Mathf.Lerp(currentDistance, correctedDistance,
            Time.deltaTime * zoomDampening) : correctedDistance;

        //Keep within limits
        currentDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance);

        //Recalculate position based on the new currentDistance
        position = target.position - (rotation * Vector3.forward * currentDistance + vTargetOffSet);

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
                rotateBehind = false;
        }
        else
        {
            rotateBehind = true;
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
