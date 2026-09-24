using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Stars : MonoBehaviour
{
    public GameObject stars;
    public int numberOfStars = 1;
    public float xMaxRange;
    public float yMinRange;
    public List<Vector2> starTransforms;
    public float drawingTime;
    public Vector3 drawingPoint;

    private void Start()
    {
        //draw a number of stars equal to the desired amount in random places within a range
        for (int i = 0; i < numberOfStars - 1; i++)
        {
            float randomX = Random.Range(xMaxRange, -xMaxRange);
            float randomY = Random.Range(yMinRange, -yMinRange);
            starTransforms.Add(new Vector2(randomX, randomY));
        }

        foreach (Vector2 starTransform in starTransforms)
        {
            Instantiate(stars, starTransform, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        DrawConstellation();
    }

    private void DrawConstellation()
    {
        
    }
}
