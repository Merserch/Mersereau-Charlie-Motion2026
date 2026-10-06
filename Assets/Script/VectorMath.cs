using System.Numerics;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class VectorMath : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentMousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        DrawSquare(currentMousePosition, 5f, Color.red, 15f);
        
    }

    

    public static void DrawSquare(Vector2 vector, float size, Color color, float duration)
    {
        Vector2 topLeft = new Vector2(-size / 2, size / 2);
        Vector2 topRight = new Vector2(size / 2, size / 2);
        Vector2 bottomLeft = new Vector2(-size / 2, -size / 2);
        Vector2 bottomRight = new Vector2(size / 2, -size / 2);
        Debug.DrawLine(topLeft + vector, topRight + vector, color, duration);
        Debug.DrawLine(topRight + vector, bottomRight + vector, color, duration);
        Debug.DrawLine(bottomRight + vector, bottomLeft + vector, color, duration);
        Debug.DrawLine(bottomLeft + vector, topLeft + vector, color, duration);
    }

    public static Vector2 GetNormalizedVector(Vector2 vector)
    {
        float sizeOfVector = GetMagnitude(vector);
        if (sizeOfVector != 0)
        {
            Vector2 normalizedVector = new Vector2(vector.x / sizeOfVector, vector.y / sizeOfVector);
            return normalizedVector;
        }
        return Vector2.zero;
    }
    public static float GetMagnitude(Vector2 vector)
    {
        return Mathf.Sqrt(vector.x * vector.x + vector.y * vector.y);
    }
    
    //convert from a vector to an angle based around the x-axis
    public static float VectorToAngle(Vector3 vector)
    {
        float angle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
        return angle - 90f;
    }

    public static float VectorDot(Vector3 a, Vector3 b)
    {
        float dotProduct = a.x * b.x + a.y * b.y;
        return dotProduct;
    }
    
}
