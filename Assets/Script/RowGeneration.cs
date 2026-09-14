using System;
using UnityEngine;
using UnityEngine.UI;

public class RowGeneration : MonoBehaviour
{
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TextCheck(string userInput)
    {
        //check if the text is an int
        if (userInput != null)
        {
            int value;
            bool validCheck = int.TryParse(userInput, out value);
            if (validCheck)
            {
                //check if the int is more than zero, and fewer than 20
                int squareNum = int.Parse(userInput);
                if (squareNum < 1)
                {
                    Debug.LogWarning("Number cannot be less than 1!");
                } else if (squareNum > 19)
                {
                    Debug.LogWarning("Number cannot be be greater than 19!");
                }
                //have a result that says the number is too small, must be larger than 0
                //have a result that says the number is too big, must be smaller than 20
                //a valid number will take the int as the number of times it runs a "for each" loop
            }
            else
            {
                //have a result in the console saying to input a valid number
                Debug.LogWarning(userInput + " was not accepted. Input a valid number.");
            }
        }
    }
    
    public void Generate(int squareNum)
    {
        Vector2 squareOrigin = new Vector2(100, -100);
        for (int i = 0; i < squareNum; i++)
        {
            Debug.Log(i);
        }
        //the "for each" loop triggers a number of times equal to the passed int
        //the "for each" loop moves the square origin over by one unit each loop.
        //after the loop is over, the square origin is reset.
    }
}
