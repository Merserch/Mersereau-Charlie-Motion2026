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
    public List<Vector3> starTransforms;
    public float drawingTime;
    public Vector3 drawingPoint;
    public int currentStar = 0;

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
        if (currentStar < numberOfStars)
        {
            DrawConstellation();
        }
        else
        {
            currentStar = 0;
        }
    }

    private void DrawConstellation()
    {
        //move a point from current position towards star
        drawingPoint += Time.deltaTime * drawingTime * (starTransforms[currentStar + 1] - drawingPoint).normalized;
        //draw a line from currentStar to current position
        Debug.DrawLine(starTransforms[currentStar], drawingPoint, Color.red);
    }
}
