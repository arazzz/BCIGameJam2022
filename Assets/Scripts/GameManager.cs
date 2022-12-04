using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int numRecipeTrials = 1;
    public int numIngredients = 3;
    public GameObject[] recipeTrials = new GameObject[numRecipeTrials];
    public int currentRecipeTrial = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private float GetRandomNumber(float minimum, float maximum)
    {
        return Random.Range(minimum, maximum);
    }
    
}
