using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Looker : MonoBehaviour
{
    private int currentTarget;
    public List<Transform> targets;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        currentTarget = 0;
        LookAt();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 facingDirection = transform.up;
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            currentTarget++;
            if (currentTarget >= targets.Count)
            {
                currentTarget = 0;
            }
            LookAt();
        }

        if (Keyboard.current.yKey.wasPressedThisFrame)
        {
            //check the angle to each target in the list and move it if it was less than the last set angle
            
        }
    }

    void LookAt()
    {
        //subtract the current direction from the target direction, get the direction from current to target
        Vector3 direction = targets[currentTarget].position - transform.position;
        //get the vector of that direction
        float facingAngle = VectorMath.VectorToAngle(direction);
        //set the rotation to the calculated angle
        transform.eulerAngles = new Vector3(0, 0, facingAngle);
    }
}
