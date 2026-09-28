using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

public class Asteroid : MonoBehaviour
{
    public float moveSpeed;
    public float arrivalDistance;
    public float maxFloatDistance;
    public Vector3 destination;
    public float distance;

    // Start is called before the first frame update
    void Start()
    {
        GetDestination();
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Print("Distance: " + distance);
        if (distance < arrivalDistance)
        {
            GetDestination();
        }
        else
        {
            CalculateDistance();
            AsteroidMovement();
        }
        
    }

    

    void GetDestination()
    {
        //set a random point up to the maxFloatDistance away from the current position as the destination. 
            destination = new Vector3(Random.Range(-1f,1f), Random.Range(-1f,1f));
            destination.Normalize();
            destination *= maxFloatDistance;
    }
    void AsteroidMovement()
    {
        transform.position = (destination - transform.position).normalized * (moveSpeed * Time.deltaTime);
    }

    void CalculateDistance()
    {
        distance = Vector3.Distance(transform.position, destination);
    }
    
    
}
