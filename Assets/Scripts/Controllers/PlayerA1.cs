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
    public float boostMax = 4f;
    public float boostMeter;
    public float boostCooldown = 1f;
    public float cooldownMeter;
    bool onCooldown = false;
    public float boostExhaust = 2f;
    public float exhaustMeter;
    bool onExhaust = false;
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
        //setting the max speed to what it normally is, immediately followed by boost so that it can be modified if you are boosting
        maxSpeed = defaultSpeed;
        
        if (Keyboard.current.shiftKey.isPressed && cooldownMeter >= boostCooldown && exhaustMeter >= boostExhaust)
        {
            Boost();
        }

        //Logic for cooldown mechanic
        //if the boost button was released and you are not exhausted, it will put you into cooldown mode
        if (Keyboard.current.shiftKey.wasReleasedThisFrame && exhaustMeter >= boostExhaust)
        {
            onCooldown = true;
        }

        //cooldown mode runs the cooldown method undil it is turned off, which it is within the method
        if (onCooldown)
        {
            Cooldown();
        }
        
        //Logic for exhaust mechanic
        if (onExhaust)
        {
            Exhaust();
        }

        //when the exhaust is finished, boost can be charged
        if (maxSpeed == defaultSpeed && boostMeter < boostMax && exhaustMeter >= boostExhaust)
        {
            boostMeter += Time.deltaTime;
            //if that addition is over the max, set it to the max
            if (boostMeter > boostMax)
            {
                boostMeter = boostMax;
            }
        }
        
        //Constantly calculating the acceleration and deceleration since this is modified by the boost mechanic
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
        if (!Keyboard.current.wKey.isPressed && !Keyboard.current.dKey.isPressed)
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
        if (boostMeter <= 0f)
        {
            onExhaust = true;
        }
    }

    void Cooldown()
    {
        cooldownMeter -= Time.deltaTime;
        if (cooldownMeter <= 0f)
        {
            cooldownMeter = boostCooldown;
            onCooldown = false;
        }
    }

    void Exhaust()
    {
        exhaustMeter -= Time.deltaTime;
        if (exhaustMeter <= 0f)
        {
            exhaustMeter = boostExhaust;
            onExhaust = false;
            onCooldown = true;
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
