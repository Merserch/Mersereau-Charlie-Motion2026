using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class SquareSpawner : MonoBehaviour
{
    private Camera cam;
    
    public Scale previewSquareScale;

    private float squareScale = 0.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.current.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        
        if (Mouse.current.scroll.ReadValue().y > 0)
        {
            squareScale = squareScale + 0.01f;
            
        }
        if (Mouse.current.scroll.ReadValue().y < 0)
        {
            squareScale = squareScale - 0.01f;
        }
        //mouse pos, coords calc
        //(a to b) (right 1 up 1, right 1 down 1 )
        // draw mouseposition.x +1 and mouseposition.y +1
        Vector2 squareTopRight = new Vector2(mousePos.x + squareScale, mousePos.y  + squareScale);
        Vector2 squareBottomRight = new Vector2(mousePos.x + squareScale, mousePos.y - squareScale);
        Vector2 squareTopLeft = new Vector2(mousePos.x - squareScale, mousePos.y + squareScale);
        Vector2 squareBottomLeft = new Vector2(mousePos.x - squareScale, mousePos.y - squareScale);
        
        Debug.DrawLine(squareTopRight, squareBottomRight, Color.grey);
        Debug.DrawLine(squareBottomRight, squareBottomLeft, Color.grey);
        Debug.DrawLine(squareBottomLeft, squareTopLeft, Color.grey);
        Debug.DrawLine(squareTopLeft, squareTopRight, Color.grey);
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Debug.DrawLine(squareTopRight, squareBottomRight, Color.red,30f);
            Debug.DrawLine(squareBottomRight, squareBottomLeft, Color.red,30f);
            Debug.DrawLine(squareBottomLeft, squareTopLeft, Color.red,30f);
            Debug.DrawLine(squareTopLeft, squareTopRight, Color.red,30f);
        
        }
    }
}
