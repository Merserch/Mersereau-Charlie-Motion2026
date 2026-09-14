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
    public Vector2 spawnOffset;

    void Update()
    {

        
        if (Keyboard.current.bKey.wasPressedThisFrame)
        {

            SpawnBombAtOffset(spawnOffset);
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
    public void SpawnBombAtOffset(Vector2 inOffset)
    { 
        object value = Instantiate(bombPrefab, Vector2.up + inOffset, Quaternion.identity, bombsTransform);
    }

    public static Vector2 WarpLocation(Vector2 location)
    {
        //Take current position and calculate the destination based on the location of the enemy and the player's position
        Vector2 playerPosition = new Vector2(0, 0); // Assuming the player's position is at the origin (0, 0)
        Vector2 directionToEnemy = location - playerPosition;

        return destination;
    }
}
