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
    private Vector2 playerTransform;
    private Vector2 offset;
    public Vector2 bombOffset;
    public Vector2 trailOffset;
    public float inDistance;
    public Vector2 inLocation;
    public float bombSpacing = 1;
    public int trailLength;

    private void Start()
    {
        
    }

    void Update()
    {
        playerTransform = transform.position;
        trailOffset.y = playerTransform.y + 1;
        trailOffset.x = playerTransform.x + 1;
        
        
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
        if (Keyboard.current.wKey.wasPressedThisFrame)
        {
            //call warplocation with enemyTransform.position
            Vector2 warpPosition = WarpLocation(enemyTransform.position);
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
        object value = Instantiate(bombPrefab, trailOffset * spacing, Quaternion.identity);
    }

    public static Vector2 WarpLocation(Vector2 location)
    {
        //Take current position and calculate the destination based on the location of the enemy and the player's position
        Vector2 playerPosition = new Vector2(0, 0); // Assuming the player's position is at the origin (0, 0)
        Vector2 directionToEnemy = location - playerPosition;
        Vector2 destination = new Vector2(0, 0);
        return destination;
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
    
}
