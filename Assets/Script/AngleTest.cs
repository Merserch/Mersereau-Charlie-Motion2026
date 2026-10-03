using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AngleTest : MonoBehaviour
{
    public List<float> angles = new List<float>();
    public float currentAngle;
    public int currentAngleIndex = 0;
    float degInRadians;
    Vector2 lineEnd;
    public float currentRadius = 1;
    public Vector2 currentOrigin =  Vector2.zero;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        float twoPiRadians = 2 * Mathf.PI;
        
        
        
        float degInRadians = currentAngle * Mathf.Deg2Rad;
        float tprInDegrees = twoPiRadians * Mathf.Rad2Deg;

        
        Mathf.Sin(degInRadians);
        Mathf.Cos(degInRadians);
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        currentAngle = angles[currentAngleIndex];
        degInRadians = currentAngle * Mathf.Deg2Rad;
        float yVal = Mathf.Sin(degInRadians);
        float xVal = Mathf.Cos(degInRadians);
        lineEnd = (new Vector2(xVal, yVal) * currentRadius) + currentOrigin ;
        Debug.DrawLine(currentOrigin, lineEnd, Color.red);
        
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Increment();
        }
    }

    void Increment()
    {
        currentAngleIndex++;
        if (currentAngleIndex > angles.Count - 1)
        {
            currentAngleIndex = 0;
        }
    }
}
