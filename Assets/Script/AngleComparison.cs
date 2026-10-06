using UnityEngine;

public class AngleComparison : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vector3 facingDirection = transform.up;
        float facingAngle = VectorMath.VectorToAngle(facingDirection);
        
        Debug.Log(facingAngle);
        Debug.Log(transform.eulerAngles.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
