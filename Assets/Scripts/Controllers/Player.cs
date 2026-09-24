using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public List<Transform> asteroidTransforms;
    public Transform enemyTransform;
    public GameObject bombPrefab;
    public Transform bombsTransform;
    private Vector2 offset;
    public int maxSpeed = 2;
    
    public Vector2 bombOffset;
    public Vector2 trailOffset;
    public float inDistance;
    public Vector2 inLocation;
    public float bombSpacing = 1;
    public int trailLength;
    public Vector3 currentVelocity =  Vector3.zero;
    public float accelerationTime = 3;
    public float currentAcceleration;
    public float decelerationTime = 3;
    public float deceleration = 1f;

    private void Start()
    {
        currentAcceleration = maxSpeed / accelerationTime;
        deceleration =  maxSpeed / decelerationTime;
    }

    void Update()
    {
        PlayerMovement();
        
        trailOffset.y = transform.position.y + 1;
        trailOffset.x = transform.position.x + 1;
        
        
        if (Keyboard.current.bKey.wasPressedThisFrame)
        {
            offset = RandomDiagonal();
            SpawnBombAtOffset();
        }
        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            for(int i = 0; i < trailLength; i++)
            {
                SpawnBombTrail(i);
            }
        }
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            //call warplocation with enemyTransform.position
            Vector2 warpPosition = WarpLocation(enemyTransform);
            transform.position = warpPosition;
        }
    }
    
    //public Vector2 GetTargetPosition()
    //{
    //    //Vector2 currentMousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    //    //Vector2 origin = new Vector2(0, 0);
        
    //    //return currentMousePosition;
    //}
    public void SpawnBombAtOffset()
    { 
        inLocation.x = transform.position.x + (offset.x * inDistance); 
        inLocation.y = transform.position.y + (offset.y * inDistance);
        Instantiate(bombPrefab, inLocation, Quaternion.identity);
    }

    public void SpawnBombTrail(float spacing)
    {
        Instantiate(bombPrefab, trailOffset * spacing, Quaternion.identity);
    }

    public Vector2 WarpLocation(Transform enemy)
    {
        //Take current position and calculate the destination based on the location of the enemy and the player's position
        Vector2 directionToEnemy = new Vector2(enemy.position.x - transform.position.x, enemy.position.y - transform.position.y);
        return directionToEnemy;
    }
    
    public static Vector2 RandomDiagonal()
    {
        float xVal = Random.Range(0f, 1f);
        if (xVal < 0.5)
        {
            xVal = -1;
        }
        else
        {
            xVal = 1;
        }
        float yVal = Random.Range(0f, 1f);
        if (yVal < 0.5)
        {
            yVal = -1;
        }
        else
        {
            yVal = 1;
        }
        return new Vector2(xVal, yVal);
    }

    public void PlayerMovement()
    {
        Vector3 accelerationDirection = Vector3.zero;
        if (Keyboard.current.wKey.isPressed)
        {
            accelerationDirection += (Vector3.up * maxSpeed);
            //moves the player one unit up over one second
        }
        if (Keyboard.current.aKey.isPressed)
        {
            accelerationDirection += (Vector3.left * maxSpeed);
            //moves the player one unit left over one second
        }
        if (Keyboard.current.sKey.isPressed)
        {
            accelerationDirection += (Vector3.down * maxSpeed);
            //moves the player one unit down over one second
        }
        if (Keyboard.current.dKey.isPressed)
        {
            accelerationDirection += (Vector3.right * maxSpeed);
            //moves the player one unit right over one second
        }
        currentVelocity += accelerationDirection.normalized * (currentAcceleration * Time.deltaTime);
        if (!Keyboard.current.wKey.isPressed && !Keyboard.current.aKey.isPressed && !Keyboard.current.dKey.isPressed &&
            !Keyboard.current.sKey.isPressed)
        {
            currentVelocity -= currentVelocity.normalized * (deceleration * Time.deltaTime);
        }
        
        transform.position = transform.position + currentVelocity * Time.deltaTime;
        
        if (currentVelocity.magnitude > maxSpeed)
        {
            currentVelocity = currentVelocity.normalized * maxSpeed;
        }

        if (currentVelocity.magnitude < 0.00001f)
        {
            currentVelocity *= 0;
        }
        
        
    }

}
