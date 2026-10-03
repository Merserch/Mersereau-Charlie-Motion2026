using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerA1 : MonoBehaviour
{
    //movement and positioning
    public Vector3 currentVelocity =  Vector3.zero;
    public float accelerationTime = 3;
    public float currentAcceleration;
    public float decelerationTime = 3;
    public float deceleration = 1f;
    public float maxSpeed = 1;
    
    

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentAcceleration = maxSpeed / accelerationTime;
        deceleration =  maxSpeed / decelerationTime;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        Vector3 accelerationDirection = Vector3.zero;
        if (Keyboard.current.wKey.isPressed)
        {
            accelerationDirection += (Vector3.up * maxSpeed);
            //add upward acceleration
        }
        if (Keyboard.current.aKey.isPressed)
        {
            accelerationDirection += (Vector3.left * maxSpeed);
            //add leftward acceleration
        }
        if (Keyboard.current.sKey.isPressed)
        {
            accelerationDirection += (Vector3.down * maxSpeed);
            //add downward acceleration
        }
        if (Keyboard.current.dKey.isPressed)
        {
            accelerationDirection += (Vector3.right * maxSpeed);
            //add rightward acceleration
        }
        
        //apply all acceleration values in a normalized vector to the current velocity vector,
        //   multiplied by the amount of time we want it to take to reach the maximum speed of acceleration.
        currentVelocity += accelerationDirection.normalized * (currentAcceleration * Time.deltaTime);
        
        //when no buttons are pressed, apply drag that decelerates the player
        if (!Keyboard.current.wKey.isPressed && !Keyboard.current.aKey.isPressed && !Keyboard.current.dKey.isPressed &&
            !Keyboard.current.sKey.isPressed)
        {
            currentVelocity -= currentVelocity.normalized * (deceleration * Time.deltaTime);
        }
        
        //move the player at the projected velocity, accounting for time
        transform.position = transform.position + currentVelocity * Time.deltaTime;
        
        //cap the speed at the maximum after calculating a higher value
        if (currentVelocity.magnitude > maxSpeed)
        {
            currentVelocity = currentVelocity.normalized * maxSpeed;
        }
        
        //set the speed to zero if it's very close to it
        if (currentVelocity.magnitude < 0.00001f)
        {
            currentVelocity *= 0;
        }
    }
}
