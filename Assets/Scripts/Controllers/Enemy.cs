using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float maxSpeed;
    public float timeToMaxSpeed;
    public float acceleration;
    public float velocity;
    public Vector3 movementDirection = Vector3.zero;
    public Transform player;
    public float detectionRadius = 3f;
    
    private void Update()
    {
        movementDirection = -(player.position - transform.position).normalized;
        if (Vector3.Distance(transform.position, player.position) < detectionRadius)
            { acceleration = maxSpeed/timeToMaxSpeed; } else { acceleration = -2/(maxSpeed/timeToMaxSpeed); }
        
        EnemyMovement();
    }

    public void EnemyMovement()
    {
        Debug.DrawLine(transform.position,transform.position + (movementDirection * detectionRadius), Color.red);
        velocity += acceleration * Time.deltaTime;
        transform.position = transform.position + (movementDirection * (velocity * Time.deltaTime));
    }
}
