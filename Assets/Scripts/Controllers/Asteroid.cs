using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float moveSpeed;
    public float arrivalDistance;
    public float maxFloatDistance;
    public Vector2 destination;

    // Start is called before the first frame update
    void Start()
    {
        GetDestination();
    }

    // Update is called once per frame
    void Update()
    {

        

        AsteroidMovement();
        
    }

    public void AsteroidMovement()
    {
        transform.position *= destination * (moveSpeed * Time.deltaTime);
    }

    void GetDestination()
    {
        //set a random point up to the maxFloatDistance away from the current position as the destination. 
            destination = new Vector2(Random.Range(-1f,1f), Random.Range(-1f,1f));
            destination.Normalize();
            destination *= maxFloatDistance;
    }
}
