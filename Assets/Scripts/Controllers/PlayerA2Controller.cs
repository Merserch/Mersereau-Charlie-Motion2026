using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;

public class PlayerA2Controller : MonoBehaviour
{
    public Rigidbody2D myRB;
    public float turnspeed = 90f;
    public GameObject midZone;
    public Color midColour;
    
    void Start()
    {
        //ContactFilter2D filter = new ContactFilter2D();
        
        midColour = midZone.GetComponent<SpriteRenderer>().color;
        myRB = GetComponent<Rigidbody2D>();
        myRB.interpolation = RigidbodyInterpolation2D.Interpolate;
        midZone.transform.position = new Vector2(0, 0);
    }

    void Update()
    {
        
        
        if (myRB.OverlapPoint(midZone.transform.position))
        {
            midColour = Color.green;
        }
        else
        {
            midColour = Color.red;
        }

        
        if (Keyboard.current.wKey.isPressed)
        {
            myRB.AddForce(transform.up);
        }

        if (Keyboard.current.aKey.isPressed)
        {
            myRB.angularDamping = 0;
            myRB.angularVelocity = turnspeed;
        }
        if (Keyboard.current.dKey.isPressed)
        {
            myRB.angularDamping = 0;
            myRB.angularVelocity = -turnspeed;
        }

        if (!Keyboard.current.aKey.isPressed && !Keyboard.current.dKey.isPressed)
        {
            myRB.angularDamping = 5;
        }

        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            
        }
    }
}
