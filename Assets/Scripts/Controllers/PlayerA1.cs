using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerA1 : MonoBehaviour
{
    //movement and positioning
    public Vector3 currentVelocity = Vector3.zero;
    public float accelerationTime = 3;
    public float currentAcceleration;
    public float decelerationTime = 3;
    public float deceleration;
    public float defaultSpeed = 1;
    public float maxSpeed;
    public float boostSpeed = 3;
    float boostMax = 10f;
    public float boostMeter;
    float boostCooldown = 2f;
    public float cooldownMeter;
    float boostExhaust = 3f;
    public float exhaustMeter;
    public float rotationSpeed = 90f;
    Vector3 rotationVector = Vector3.zero;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //set the boost to the maximum
        boostMeter = boostMax;
        cooldownMeter = boostCooldown;
        exhaustMeter = boostExhaust;
        maxSpeed = defaultSpeed;

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
        maxSpeed = defaultSpeed;
        
        if (Keyboard.current.shiftKey.isPressed && cooldownMeter >= boostCooldown)
        {
            Boost();
        }

        if (Keyboard.current.shiftKey.wasReleasedThisFrame && boostMeter < boostMax && exhaustMeter >= boostExhaust)
        {
            Cooldown();
        }
        
        currentAcceleration = maxSpeed / accelerationTime;
        deceleration =  maxSpeed / decelerationTime;
        
        Vector3 accelerationDirection = Vector3.zero;
        if (Keyboard.current.wKey.isPressed)
        {
            accelerationDirection += (transform.up * maxSpeed);
            //add upward acceleration
        }
        if (Keyboard.current.aKey.isPressed)
        {
            Rotate(-transform.right);
            //add leftward acceleration
        }
        if (Keyboard.current.sKey.isPressed)
        {
            accelerationDirection += (-transform.up * maxSpeed);
            //add downward acceleration
        }
        if (Keyboard.current.dKey.isPressed)
        {
            Rotate(transform.right);
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
            currentVelocity -= currentVelocity.normalized * (deceleration * Time.deltaTime);
        }
        
        //set the speed to zero if it's very close to it
        if (currentVelocity.magnitude < 0.00001f)
        {
            currentVelocity *= 0;
        }
        
        
    }

    void Boost()
    {
        boostMeter -= Time.deltaTime;
        maxSpeed = boostSpeed;
        if (boostCooldown <= 0f)
        {
            Exhaust();
        }
    }

    void Cooldown()
    {
        cooldownMeter -= Time.deltaTime;
        if (cooldownMeter <= 0f)
        {
            cooldownMeter = boostCooldown;
        }
    }

    void Exhaust()
    {
        exhaustMeter -= Time.deltaTime;
        if (exhaustMeter <= 0f)
        {
            exhaustMeter = boostExhaust;
        }
    }
    
    void Rotate(Vector3 direction)
    {
        
        float rightTurn = VectorMath.VectorDot(direction, transform.right);
        
        if (rightTurn >= 0) 
        {
            
            transform.eulerAngles -= Vector3.forward * (rotationSpeed * Time.deltaTime);
        }
        else
        {
            transform.eulerAngles += Vector3.forward * (rotationSpeed * Time.deltaTime);
        }
    }
}
