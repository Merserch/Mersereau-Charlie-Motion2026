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
    private Transform playerTransform;
    public Vector2 bombOffset;
    public Vector2 trailOffset ;
    public float bombSpacing = 1;
    public int trailLength;

    private void Start()
    {
        
    }

    void Update()
    {
        playerTransform = GetComponent<Transform>();
        trailOffset.y = playerTransform.position.y - 1;
        trailOffset.x = playerTransform.position.x;
        
        if (Keyboard.current.bKey.wasPressedThisFrame)
        {

            SpawnBombAtOffset(bombOffset);
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
    public void SpawnBombAtOffset(Vector2 spawnOffset)
    { 
        object value = Instantiate(bombPrefab, Vector2.up + spawnOffset, Quaternion.identity);
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
}
