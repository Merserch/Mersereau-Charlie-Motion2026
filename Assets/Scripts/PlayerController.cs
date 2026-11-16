using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed = 3f;
    public float accelerationTime = 1f;
    private float acceleration;
    
    public enum FacingDirection
    {
        left, right
    }
    
    Rigidbody2D myRB;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
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
            playerInput.x -= 1f;
        }
        if (Keyboard.current.dKey.isPressed)
        {
            playerInput.x += 1f;
        }
        
        MovementUpdate(playerInput);
    }

    private void MovementUpdate(Vector2 playerInput)
    {
        Vector2 newVelocity  = myRB.linearVelocity;
        
        if (playerInput.x != 0)
        {
            //acceleration
            newVelocity += playerInput * (acceleration * Time.deltaTime);
        }
        else
        {
            //????
        }
        
        myRB.linearVelocity = newVelocity;
    }

    public bool IsWalking()
    {
        return false;
    }
    public bool IsGrounded()
    {
        return true;
    }

    public FacingDirection GetFacingDirection()
    {
        return FacingDirection.left;
    }
}
