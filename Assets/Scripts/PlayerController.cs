using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour
{
    //top speed, can be set in inspector
    public float maxSpeed = 3f;
    //time to reach top speed, can be set in inspector
    public float accelerationTime = 1f;
    //calculated acceleration value
    private float acceleration;
    //value that will go into linear dampening
    public float friction = 3f;
    public float landingDetectionLength;
    public LayerMask groundLayer;
    public bool lastFacingRight = true;
    
    
    public enum FacingDirection
    {
        left, right
    }
    
    Rigidbody2D myRB;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        //calculate the acceleration value
        acceleration = maxSpeed/accelerationTime;
    }

    // Update is called once per frame
    void Update()
    {
        //The input from the player needs to be determined and then passed in the to the MovementUpdate which should
        //manage the actual movement of the character.
        Vector2 playerInput = new Vector2();
        if (Keyboard.current.aKey.isPressed)
        {
            //sets the input value to a left transform
            playerInput.x -= 1f;
        }
        if (Keyboard.current.dKey.isPressed)
        {
            //sets the input vallue to a right transform
            playerInput.x += 1f;
        }
        
        MovementUpdate(playerInput);
        
    }

    private void MovementUpdate(Vector2 playerInput)
    {
        Vector2 newVelocity = myRB.linearVelocity;
        
        if (playerInput.x != 0)
        {
            //acceleration
            newVelocity += playerInput * (acceleration * Time.deltaTime);
            myRB.linearDamping = 0;
        }
        else
        {
            myRB.linearDamping = friction;
            
        }

        //normalize the speed to max speed if you go faster than it.
        if (newVelocity.sqrMagnitude > maxSpeed * maxSpeed)
        {
            newVelocity = newVelocity.normalized * maxSpeed;
        }
        
        myRB.linearVelocity = newVelocity;
        
        
    }

    public bool IsWalking()
    {
        if (myRB.linearVelocity.x > -0.1f && myRB.linearVelocity.x < 0.1f)
        {
            return false;
        }
        else 
        {
            return true;
        }
    }
    public bool IsGrounded()
    {
        //get a origin point in relation to the player's position.
        Vector2 lineEnd = myRB.transform.position - new Vector3(0, landingDetectionLength, 0);
        
        //Test for seeing the length of the cast
        //Debug.DrawLine(myRB.transform.position, lineEnd, Color.red);
        
        //BoxCast, from position calculation above, a box size of one, rotated zero degrees, casting down,
        //a cast length of zero (so only what is within the box), and only detecting the custom ground layer.
        if (Physics2D.BoxCast(lineEnd, Vector2.one, 0f, Vector2.down, 0f, groundLayer))
        {
            //for testing
            //Debug.Log("detecting");
            return true;
        }
        else
        {
            return false;
        }
        
    }

    public FacingDirection GetFacingDirection()
    {
        
        if (myRB.linearVelocity.x <= -0.05f)
        {
            lastFacingRight = false;
        } else if (myRB.linearVelocity.x >= 0.05f)
        {
            lastFacingRight = true;
        }

        if (lastFacingRight)
        {
            return FacingDirection.right;
        }
        else
        {
            return FacingDirection.left;
        }
    }

    

    
}
