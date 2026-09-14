using UnityEngine;
using UnityEngine.InputSystem;

public class AddVector : MonoBehaviour
{
    public Transform rTransform; //(1, 3)
    public Transform bTransform; //(2, 2)
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 rPlusBVector = rTransform.position + bTransform.position;
        Vector2 rMinusBVector = rTransform.position - bTransform.position;
        
        Vector2 origin = new Vector2(0, 0);
        
        Vector2 fromRToB = bTransform.position - rTransform.position;
        
        Debug.DrawLine(rTransform.position,fromRToB,Color.red);

        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            Debug.DrawLine(origin,rTransform.position,Color.red, 10f);
            Debug.Log("Red is at " + rTransform.position);
            
            if (Keyboard.current.bKey.isPressed)
            {
                Debug.DrawLine(origin,rPlusBVector,Color.magenta, 10f);
                Debug.DrawLine(origin,rMinusBVector,Color.yellow, 10f);
                Debug.Log("Red + Blue is at " + rPlusBVector);
                Debug.Log("Red - Blue is at " + rMinusBVector);
            }
        }

        if (Keyboard.current.bKey.wasPressedThisFrame)
        {
            Debug.DrawLine(origin, bTransform.position, Color.teal, 10f);
            Debug.Log("Blue is at " + rTransform.position);

            if (Keyboard.current.rKey.isPressed)
            {
                Debug.DrawLine(origin,rPlusBVector,Color.magenta, 10f);
                Debug.DrawLine(origin,rMinusBVector,Color.yellow, 10f);
                Debug.Log("Red + Blue is at " + rPlusBVector);
                Debug.Log("Red - Blue is at " + rMinusBVector);
            }
        }
    }
}
