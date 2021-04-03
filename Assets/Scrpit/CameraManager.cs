using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class CameraManager : NetworkBehaviour
{
    public float followSpeed = 3; //Speed ​​at which the camera follows us
    public float mouseSpeed = 2; //Speed ​​at which we rotate the camera with the mouse

    //public float controllerSpeed = 5; //Speed ​​at which we rotate the camera with the joystick
    public float cameraDist = 3; //Distance to which the camera is located

    private Transform target; //Player the camera follows

    private Transform pivot; //Pivot on which the camera rotates(distance that we want between the camera and our character)

    [SerializeField]
    private Transform camTrans; //Camera position

    private float turnSmoothing = .1f; //Smooths all camera movements (Time it takes the camera to reach the rotation indicated with the joystick)
    public float minAngle = -35; //Minimum angle that we allow the camera to reach
    public float maxAngle = 35; //Maximum angle that we allow the camera to reach

    private float smoothX;
    private float smoothY;
    private float smoothXvelocity;
    private float smoothYvelocity;
    public float lookAngle; //Angle the camera has on the Y axis
    public float tiltAngle; //Angle the camera has up / down

    private void Start()
    {
    }

    /*[Command]*/

    public void Init()
    {
        pivot = camTrans.parent;
    }

    /*  [ClientRpc]
      public void clientinit()
      {
      }*/

    [Command]
    public void FollowTarget(float d)
    {
        //Function that makes the camera follow the player
        clientfollowtarget(d);
    }

    [Command]
    public void handlecommand(float d, float v, float h, float targetSpeed)
    {
        HandleRotations(d, v, h, targetSpeed);
    }

    public void clientfollowtarget(float b)
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        float speed = b * followSpeed; //Set speed regardless of fps
        Vector3 targetPosition = Vector3.Lerp(transform.position, target.position, speed); //Bring the camera closer to the player interpolating with the velocity(0.5 half, 1 everything)
        transform.position = targetPosition; //Update the camera position
    }

    public void HandleRotations(float r, float z, float n, float targetSpeed)
    { //Function that rotates the camera correctly
        pivot = camTrans.parent;
        if (turnSmoothing > 0)
        {
            smoothX = Mathf.SmoothDamp(smoothX, n, ref smoothXvelocity, turnSmoothing); //Gradually change a value toward a desired goal over time.
            smoothY = Mathf.SmoothDamp(smoothY, z, ref smoothYvelocity, turnSmoothing);
        }
        else
        {
            smoothX = n;
            smoothY = z;
        }

        tiltAngle -= smoothY * targetSpeed; //Update the angle at which the camera will move
        tiltAngle = Mathf.Clamp(tiltAngle, minAngle, maxAngle); //Limits with respect to the maximum and minimum
        pivot.localRotation = Quaternion.Euler(tiltAngle, 0, 0); //Modify the up / down angle

        lookAngle += smoothX * targetSpeed; //Updates the rotation angle in y smoothly
        transform.rotation = Quaternion.Euler(0, lookAngle, 0); //Apply the angle
    }

    [Client]
    private void FixedUpdate()
    {//Function that correctly rotates the camera based on the joystick / mouse and follows the player (the delta time is sent to be independent of the fps)
        if (!hasAuthority) return;

        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");

        //float c_h = Input.GetAxis("RightAxis X");
        //float c_v = Input.GetAxis("RightAxis Y");

        float targetSpeed = mouseSpeed;

        /*if (c_h != 0 || c_v != 0)
        { //Overwrites if i use joystick
            h = c_h;
            v = -c_v;
            targetSpeed = controllerSpeed;
        }*/

        clientfollowtarget(Time.deltaTime); //Follow player
        HandleRotations(Time.deltaTime, v, h, targetSpeed); //Rotates camera
    }

    [Client]
    private void LateUpdate()
    {
        if (!hasAuthority) return;

        //Here begins the code that is responsible for bringing the camera closer by detecting wall
        float dist = cameraDist + 1.0f; // distance to the camera + 1.0 so the camera doesnt jump 1 unit in if it hits someting far out
        Ray ray = new Ray(camTrans.parent.position, camTrans.position - camTrans.parent.position);// get a ray in space from the target to the camera.
        RaycastHit hit;
        // read from the taret to the targetPosition;
        if (Physics.Raycast(ray, out hit, dist))
        {
            if (hit.transform.tag == "Wall")
            {
                // store the distance;
                dist = hit.distance - 0.25f;
            }
        }
        // check if the distance is greater than the max camera distance;
        if (dist > cameraDist) dist = cameraDist;
        camTrans.localPosition = new Vector3(0, 0, -dist);
    }

    public static CameraManager singleton; //You can call CameraManager.singleton from other script (There can be only one)

    private void awake()
    {
        singleton = this; //Self-assigns
        Init();
    }
}