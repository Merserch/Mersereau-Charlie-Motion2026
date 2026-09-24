using UnityEngine;
using System.Collections.Generic;

public class Zoo : MonoBehaviour
{
    public List<string> animals;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animals.Add("Penguin");
        animals.Add("Triceratops");
        animals.Add("Shark");
        animals.Remove("Triceratops");

        foreach (string currentAnimal in animals)
        {
            Debug.Log("Here comes the " + currentAnimal + "!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
