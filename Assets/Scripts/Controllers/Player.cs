using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Player : MonoBehaviour
{
    public List<Transform> asteroidTransforms;
    public Transform enemyTransform;
    public Vector2 enemyVector;
    
    public GameObject bombPrefab;
    public GameObject powerupPrefab;
    public Transform bombsTransform;
    private Vector2 offset;
    public int maxSpeed = 2;

    public int numberOfPowerups = 5;
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

    // radar stuff
    public List<float> angles = new List<float>();
    float degInRadians;
    Vector2 lineEnd;
    Vector2 lineStart;
    public float currentRadius = 3f;
    public Vector2 currentOrigin =  Vector2.zero;
    
    // powerup stuff
    Vector2 powerupSpawn;
    
    private void Start()
    {
        SpawnPowerups();
        CalculatePointsOfRadar();
        currentAcceleration = maxSpeed / accelerationTime;
        deceleration =  maxSpeed / decelerationTime;

    }

    void Update()
    {
        
        
        CalculatePointsOfRadar();
        PlayerMovement();
        DrawRadarCircle();
        
        currentOrigin = transform.position; //wherever you are, that's where the origin is

        
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

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            //spawn powerups
            SpawnPowerups();
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
        { xVal = -1; }
        else
        { xVal = 1; }
        float yVal = Random.Range(0f, 1f);
        if (yVal < 0.5)
        { yVal = -1; }
        else
        { yVal = 1; }
        return new Vector2(xVal, yVal);
    }

    public void PlayerMovement()
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

    void CalculatePointsOfRadar()
    {
        for (int i = 0; i < angles.Count; i++)
        {
            angles[i] = i * (360/angles.Count);
        }
    }

    void DrawRadarCircle()
    {
        //get the position of the enemy for reference
        enemyVector = new  Vector2(enemyTransform.position.x, enemyTransform.position.y);
        
        //calculate the lines between each point
        for (int i = 0; i < angles.Count; i++)
        {
            int jPoint = i + 1; //we have an i index, j should be the next index
            if (jPoint == angles.Count)
            {
                jPoint = 0; //and if we go over the count, connect the highest number to the lowest number, 0
            }
            float currentAngle = angles[i]; //set the first angle
            float destinationAngle = angles[jPoint]; //set the second angle
            float currentInRadians = currentAngle * Mathf.Deg2Rad; //convert first to radians
            float destinationInRadians = destinationAngle * Mathf.Deg2Rad; //convert second to radians
            float yVali = Mathf.Sin(currentInRadians); //first y
            float xVali = Mathf.Cos(currentInRadians); //first x
            float yValj = Mathf.Sin(destinationInRadians); //first y
            float xValj = Mathf.Cos(destinationInRadians); //first x
            lineStart =  (new Vector2(xVali, yVali) * currentRadius) + currentOrigin; //set the first point in relation to the origin
            lineEnd = (new Vector2(xValj, yValj) * currentRadius) + currentOrigin; //set the second point in relation to the origin
            
            if (Vector2.Distance(transform.position, enemyTransform.position) < currentRadius) //if the distance between the two is less than the radius...
            {
                Debug.DrawLine(lineStart, lineEnd, Color.red); //it's red
            }
            else 
            {
                Debug.DrawLine(lineStart, lineEnd, Color.green); //otherwise it's green
            }
            
        }
    }

    public void SpawnPowerups()
    {
        float powerupRadius = currentRadius;
        float powerupAngle;
        float powerupRadians;
        for (int i = 1; i <= numberOfPowerups; i++)
        {
            powerupAngle = i * (360 / numberOfPowerups);
            powerupRadians = powerupAngle * Mathf.Deg2Rad;
            float xVal = Mathf.Cos(powerupRadians);
            float yVal = Mathf.Sin(powerupRadians);
            powerupSpawn = (new Vector2(xVal, yVal) * powerupRadius) + currentOrigin;
            Instantiate(powerupPrefab, powerupSpawn, Quaternion.identity);
            Debug.Log("I tried to spawn powerup #" + i);
        }
    }

}
