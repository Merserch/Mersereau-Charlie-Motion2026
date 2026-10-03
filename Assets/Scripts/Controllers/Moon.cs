using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    public float orbitalSpeed;
    public float radius;
    
    public float shiftProgress = 0f;
    public float shiftDuration;
    public int angleIndex = 0;
    public List<float> angles = new List<float>();
    
    public float currentAngle;
    public Vector2 circleOffset;
    public Transform planetTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OrbitalMotion(radius, orbitalSpeed, planetTransform);
    }
    
    public void OrbitalMotion(float radius, float speed, Transform target)
    {
        shiftProgress += speed * Time.deltaTime; //increase the shift progress so 1 second pregresses it by 1 times the speed
        if(shiftProgress > shiftDuration) //once the progress is greater than the duration (duration acting as the desired max
        {
            angleIndex++; //increase the index
            shiftProgress = 0f; //reset the progress to zero
            
            if (angleIndex >= angles.Count) //once the index is equal or greater than the total number of angles in the list?
            {
                angleIndex = 0; //Loop the index progression back to zero
            }
        }
        
        circleOffset = new Vector2(target.position.x, target.position.y); //create a vector from the transform of the planet
        currentAngle = angles[angleIndex]; //Get the value in the current 
        float xPos = Mathf.Cos(currentAngle * Mathf.Deg2Rad) * radius; //set x from angle * radius
        float yPos = Mathf.Sin(currentAngle * Mathf.Deg2Rad) * radius; //same for y
        Vector2 currentPos = new Vector2(xPos, yPos) + circleOffset; //put x and y into Vector + the planet position
        transform.position = currentPos; //set the moon's position to the calculated value

    }
}
