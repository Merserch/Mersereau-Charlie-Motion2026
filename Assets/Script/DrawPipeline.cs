using UnityEngine;
using UnityEngine.InputSystem;

public class DrawPipeline : MonoBehaviour
{
    public Camera cam;
    private float drawTimer = 0.1f;
    Vector2 mousePos = new Vector2(0,0);
    Vector2 lastPos = new Vector2(0,0);

    void Start()
    {
       
    }
    
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            mousePos = Camera.current.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        }
        if (Mouse.current.leftButton.isPressed)
        {
            drawTimer -= Time.deltaTime;
            if (drawTimer <= 0)
            {
                Debug.Log("Tries to draw.");
                drawTimer = 0.1f;
                lastPos = mousePos;
                mousePos = Camera.current.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                Debug.DrawLine(mousePos, lastPos, Color.red,30f);
            }
        }
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            drawTimer = 0.1f;
        }
    }
}
