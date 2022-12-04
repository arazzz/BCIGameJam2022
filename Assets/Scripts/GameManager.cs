using System;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Recipe))]
public class GameManager : MonoBehaviour
{

    public static int numRecipeTrials = 1;
    public static int numIngredients = 3;
    public int currentRecipeTrial = 0;

    public GameObject[] recipeTrials = new GameObject[numRecipeTrials];
    public GameObject ingredientObject;
    public GameObject ingredientPrefab;

    public Dictionary <string, object> foodSprites = new Dictionary<string, object>();
    public static Dictionary<string, string[]> recipes = new Dictionary<string, string[]>
    {   
        // Cheese + Flour + Tomato = Pizza
        { "pizza", new [] {
            "Food-2/225 - icons pack sprite sheet_128", // Cheese 
            "Food-2/225 - icons pack sprite sheet_64",  // Tomato 
            "Food-2/225 - icons pack sprite sheet_50" // Dough ?
        } },
    };
    public List<string> recipeNames = new List<string>(recipes.Keys);

    void Awake()
    {
        foodSprites = new Dictionary<string, object>
        {
            {"Food-2", Resources.LoadAll<Sprite>("Sprites/Food-2")}
        };
    }

    // Start is called before the first frame update
    void Start()
    {

        // Debug.Log((foodSprites["Food-2"] as Sprite[])[0]);

        // Create the recipe trials
        for (int i = 0; i < numRecipeTrials; i++)
        {

            // Get random recipe from recipes dictionary
            KeyValuePair <string, string[]> randomRecipe = GetRandomRecipe();

            recipeTrials[i] = new GameObject();
            recipeTrials[i].name = "Recipe Trial " + i;
            recipeTrials[i].AddComponent<Recipe>();
            recipeTrials[i].GetComponent<Recipe>().ingredients = new GameObject[randomRecipe.Value.Length];
            recipeTrials[i].GetComponent<Recipe>().recipeName = randomRecipe.Key;
            recipeTrials[i].GetComponent<Recipe>().transform.parent = this.transform;

            // Create the ingredients
            for (int j = 0; j < randomRecipe.Value.Length; j++)
            {
                
                recipeTrials[i].GetComponent<Recipe>().ingredients[j] = Instantiate(ingredientPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                recipeTrials[i].GetComponent<Recipe>().ingredients[j].name = $"Recipe {i} - Ingredient {j}";
                
                string[] ingredientSpriteInfo = randomRecipe.Value[j].Split('/');
                string ingredientSpriteSheet = ingredientSpriteInfo[0];
                string ingredientSpriteIndex = Regex.Match(ingredientSpriteInfo[1], @"\d+").Value; 
                Sprite ingredientSprite = (foodSprites[ingredientSpriteSheet] as Sprite[])[int.Parse(ingredientSpriteIndex)];
                
                recipeTrials[i].GetComponent<Recipe>().ingredients[j].GetComponent<SpriteRenderer>().sprite = ingredientSprite;
                
                recipeTrials[i].GetComponent<Recipe>().ingredients[j].GetComponent<SpriteRenderer>().sortingOrder = 1;
                recipeTrials[i].GetComponent<Recipe>().ingredients[j].transform.parent = recipeTrials[i].gameObject.transform;
                recipeTrials[i].GetComponent<Recipe>().ingredients[j].transform.position = new Vector3(-5.5f + 2.0f*j, 1.75f, 0.0f);
                // recipeTrials[i].GetComponent<Recipe>().ingredients[j].transform.localScale = new Vector3(2, 2, 2);
                recipeTrials[i].GetComponent<Recipe>().ingredients[j].AddComponent<BoxCollider2D>();

                // spriteObject.GetComponent<SpriteRenderer>().sortingOrder = sortingOrder;
                // spriteObject.AddComponent<BoxCollider2D>();
                // spriteObject.transform.parent = this.gameObject.transform;

                // Debug.Log($"Recipe {i} - Ingredient {j} - {randomRecipe.Value[j]}");

            }
            
        }

        // // Create ingredient object by specifying sprite path and position
        // int ingredientSpriteIndex = GetRandInt(0, 233);
        // ingredientObject = CreateIngredientObject("Sprites/Food-2/225 - icons pack sprite sheet_${ingredientSpriteIndex}", new Vector3(0, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Generate random integer
    private int GetRandInt(int minimum, int maximum)
    {
        return UnityEngine.Random.Range(minimum, maximum);
    }

    // Create a sprite object
    private GameObject CreateSpriteObject(string name, string spritePath, int sortingOrder, Vector3 position)
    {
        GameObject spriteObject = new GameObject();
        spriteObject.name = name;
        spriteObject.AddComponent<SpriteRenderer>();
        spriteObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(spritePath);
        spriteObject.GetComponent<SpriteRenderer>().sortingOrder = sortingOrder;
        spriteObject.AddComponent<BoxCollider2D>();
        spriteObject.transform.parent = this.gameObject.transform;
        
        // Position the ingredient object
        spriteObject.transform.position = position;

        return spriteObject;
    }

    // Create an ingredient object
    private GameObject CreateIngredientObject(string spritePath, Vector3 position)
    {
        return CreateSpriteObject("Sample Ingredient", spritePath, 1, position);
    }

    // Get random recipe from recipes dictionary
    private KeyValuePair<string, string[]> GetRandomRecipe()
    {
        int randomRecipeIndex = GetRandInt(0, recipeNames.Count);
        return new KeyValuePair<string, string[]>(recipeNames[randomRecipeIndex], recipes[recipeNames[randomRecipeIndex]]);
    }

}
