using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform targetTransform;
    bool shouldTurnRight;
    public float rotationSpeed = 20f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(transform.position, transform.position + transform.up, Color.red);
        Vector3 directionToTarget = targetTransform.position - transform.position;
        float angle = VectorMath.VectorDot(directionToTarget, transform.right);
        if (angle >= 0)
        {
            shouldTurnRight = true;
        }
        else
        {
            shouldTurnRight = false;
        }
        Debug.Log(shouldTurnRight);

        if (shouldTurnRight)
        {
            transform.eulerAngles -= Vector3.forward * (rotationSpeed * Time.deltaTime);
        }
        else
        {
            transform.eulerAngles += Vector3.forward * (rotationSpeed * Time.deltaTime);
        }
    }
}
