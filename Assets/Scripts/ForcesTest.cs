using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;


public class ForcesTest : MonoBehaviour
{

    public Vector3 MousePos;
    public float forceStrength;
    public Rigidbody2D rB;
    void Start()
    {
        rB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        MousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        MousePos.z = 0;
        
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 directionToMouse = MousePos - transform.position;
            rB.AddForce(directionToMouse.normalized * forceStrength, ForceMode2D.Force); 
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision Enter");
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("Collision Exit");
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("Collision Staying");
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger Enter");
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Trigger Exit");
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Trigger Staying");
    }
}
