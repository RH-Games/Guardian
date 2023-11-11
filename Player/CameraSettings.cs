using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraSettings : MonoBehaviour
{
    //calls input manager
    InputManager inputManager;

    public Transform camFollow; //cam will follow this object which will be followPlayer Object on the player Object.
    public Transform camAngle; //This is to have the camera pivot up and down angle.
    public Transform camTransform; //this is the camera transform.
    private Vector3 camFollowVelocity = Vector3.zero;

    //camera Collision 
    private Vector3 camVectorPos; //this is so the z position can be edited in the code for collision
    public LayerMask objectCollison; // all objects under this layer is the layers that will collide with the camera
    
    private float camDefaultPos;            //cam starting position
    public float camCollisionRadius = 0.2f; //the radius of the cam collision
    public float camCollisionOffSet = 0.2f; //the distance the camera will move away from objects the collision radius will hit.
    public float minCollisonOffSet = 0.2f; //minimum off set  

    public float camFollowSpeed = 0.2f;

    //stores the angles 
    public float lookAngle; //left and right 
    public float pivotAngle; //up and down

    //speed for the look and pivot
    public float lookSpeed = 2f; 
    public float pivotSpeed = 2f;

    //max and min is inverted.
    private float MaxPivot = 25;
    private float minPivot = -25;

    private void Awake()
    {
        inputManager = FindObjectOfType<InputManager>();
        camFollow = FindObjectOfType<PlayerManger>().transform;
        camTransform = Camera.main.transform;
        camDefaultPos = camTransform.localPosition.z; //makes ther default position the local z position at start of the game
    }

    public void CameraMovement()
    {
        FollowPlayer();
        RotateCam();
        CameraCollisons();
   
    }

    private void FollowPlayer()
    {   
        // this will update to the players position, using smooth damp is better and smoother camera movement 
        Vector3 playerPosition = Vector3.SmoothDamp(transform.position, camFollow.position, ref camFollowVelocity, camFollowSpeed);

        //cam postion 
        transform.position = playerPosition;

    }

    private void RotateCam()
    {

        Vector3 rotate;
        Quaternion rotation;

        lookAngle = lookAngle - (inputManager.cameraInputX * lookSpeed);

        //using it as minus will invert the pviotangle 
        pivotAngle = pivotAngle - (inputManager.cameraInputY * pivotSpeed);

        //mathf takes the pivot angle and uses min and max to limit them.
        pivotAngle = Mathf.Clamp(pivotAngle,minPivot,MaxPivot);

        rotate = Vector3.zero;
        rotate.y = lookAngle;
        rotation = Quaternion.Euler(rotate);
        transform.rotation = rotation;

        rotate = Vector3.zero;
        rotate.x = pivotAngle;
        rotation = Quaternion.Euler(rotate);

        camAngle.localRotation = rotation;

        //limits the lookangle values to not go over 360 or -360. 
        if (lookAngle >= 360)
            lookAngle = 0;
        if (lookAngle <= -360)
            lookAngle = 0;
    }


    private void CameraCollisons()
    {

        ///DOESN'T WORK REDO AT SOMEPOINT OR CAMERA WILL CLIP THROUGH EVERY OBJECT
        float targetPos = camDefaultPos;
        Vector3 direction;
        //ray cast to check if the direction of the camera transform hits an object then move camera in view of player
        direction = camTransform.position - camAngle.position;
        direction.Normalize();

        //makes a shere cast at the camera angle position and has a ray cast in the direction of traget position
        if (Physics.SphereCast(camAngle.transform.position,camCollisionRadius,direction, out RaycastHit hit,Mathf.Abs(targetPos), objectCollison))
        {
            //the distance between camera position and the hit point of another object.
            float distance = Vector3.Distance(camAngle.position, hit.point);
            targetPos =- (distance - camCollisionOffSet);//pushes the camera away from the objext it hits

        }

        if(Mathf.Abs(targetPos) < minCollisonOffSet)
        {
            targetPos =- minCollisonOffSet;
        }


        camVectorPos.z = Mathf.Lerp(camTransform.localPosition.z, targetPos, 0.2f);
        camTransform.localPosition = camVectorPos;

    
    }

    

}
