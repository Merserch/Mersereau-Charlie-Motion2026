using UnityEngine;

public class Turret : MonoBehaviour
{
    //The object this script is on will rotate to the target assigned in the inspector.
    public Transform targetTransform; //Assign in the inspector
    public float rotationSpeed = 20f; //degrees per second of rotation, can change in the inspector
    
    
    void Update()
    {
        //Find the direction to the target transform
        Vector3 directionToTarget = targetTransform.position - transform.position;
        //gets the dot product, something between 1 and -1
        float rightTurn = VectorMath.VectorDot(directionToTarget, transform.right);
        //we use transform.right as the starting point.
        //You could do this using transform.left instead, but 
        //angles from 90 to 270 will be on the left of transform.up
        
        if (rightTurn >= 0) 
        {
            //set the euler angles of this object to the current value minus the following value
            //forward is the word we have to use for toward/away from the camera, the z axis, the axis we use for 2D rotation
            //if we subtract from forward, it rotates right >>> and if we add it rotates left <<<
            //you can see this in the inspector if you grab the z rotation, move it right, the object rotates right and the number gets smaller
            //so, if we subtract every frame, account for time with Time.deltaTime, and multiply it for the number of degrees we want it to rotate per second...
            //it rotates!
            //we rotate right when it's between 0 and 90 AND between 270 and 360
            //else, it rotates the other way, so we add instead of subtract
            
            transform.eulerAngles -= Vector3.forward * (rotationSpeed * Time.deltaTime);
        }
        else
        {
            transform.eulerAngles += Vector3.forward * (rotationSpeed * Time.deltaTime);
        }
    }
}
