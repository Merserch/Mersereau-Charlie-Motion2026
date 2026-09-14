using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.InputSystem;

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

    
}
