using System;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Recipe))]
public class GameManager : MonoBehaviour
{

    // private Vector2 screenBounds;

    public static int numRecipeTrials = 1;
    public static int numIngredients = 3;
    public int currentRecipeTrial = 0;

    public GameObject[] recipeTrials = new GameObject[numRecipeTrials];
    public GameObject ingredientObject;
    public GameObject ingredientPrefab;
    public GameObject[] setPathPoints;

    public Dictionary <string, object> foodSprites = new Dictionary<string, object>();
    public static Dictionary<string, string[]> recipes = new Dictionary<string, string[]>
    {
        { 
            "applepie", new [] {
                "applepie_apple",
                "applepie_egg",
                "applepie_flour",
            }
        },
        { 
            "cheeseburger", new [] {
                "cheeseburger_bun",
                "cheeseburger_cheese",
                "cheeseburger_meat",
            }
        },
        { 
            "curry", new [] {
                "curry_sauce",
                "curry_bellpeppers",
                "curry_steak",
            }
        },
        { 
            "strawberrycake", new [] {
                "strawberrycake_strawberry",
                "strawberrycake_butter",
                "strawberrycake_milk",
            }
        },
        { 
            "pizza", new [] {
                "pizza_flour",
                "pizza_tomato",
                "pizza_cheese",
            }
        },
        
    };
    public List<string> recipeNames;

    void Awake()
    {
        
        recipeNames = new List<string>(recipes.Keys);

        // float x1 = GameObject.Find("Left Border").GetComponent<SpriteRenderer>().bounds.size.x / 2 + GameObject.Find("Left Border").transform.position.x;
        // float x2 = GameObject.Find("Right Border").transform.position.x - GameObject.Find("Right Border").GetComponent<SpriteRenderer>().bounds.size.x / 2;
        // float y1 = GameObject.Find("Bottom Border").GetComponent<SpriteRenderer>().bounds.size.y / 2 + GameObject.Find("Bottom Border").transform.position.y;
        // float y2 = GameObject.Find("Top Border").transform.position.y - GameObject.Find("Top Border").GetComponent<SpriteRenderer>().bounds.size.y / 2;

        // foodSprites = new Dictionary<string, object>
        // {
        //     {"Food-2", Resources.LoadAll<Sprite>("Sprites/Food-2")}
        // };
    }

    // Start is called before the first frame update
    void Start()
    {
        //new ingredient spawn time
        Time.fixedDeltaTime =5.0f;

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
                
                // Parse the sprite name from the recipes dictionary and load it from the foodSprites dictionary 
                // string[] ingredientSpriteInfo = randomRecipe.Value[j].Split('/');
                // string ingredientSpriteSheet = ingredientSpriteInfo[0];
                // MatchCollection ingredientSpriteIndexMatches = Regex.Matches(ingredientSpriteInfo[1], @"\d+");
                // int ingredientSpriteIndex = Int32.Parse(ingredientSpriteIndexMatches[ingredientSpriteIndexMatches.Count - 1].Value);
                // Sprite ingredientSprite = (foodSprites[ingredientSpriteSheet] as Sprite[])[ingredientSpriteIndex];

                Sprite ingredientSprite = Resources.Load<Sprite>($"Sprites/menu/{randomRecipe.Key}/{randomRecipe.Value[j]}");

                recipeTrials[i].GetComponent<Recipe>().ingredients[j].GetComponent<SpriteRenderer>().sprite = ingredientSprite;
                recipeTrials[i].GetComponent<Recipe>().ingredients[j].GetComponent<SpriteRenderer>().sortingOrder = 1;
                recipeTrials[i].GetComponent<Recipe>().ingredients[j].transform.parent = recipeTrials[i].gameObject.transform;
                
                // ! TODO: Optimize the placement and sizing of the ingredients
                recipeTrials[i].GetComponent<Recipe>().ingredients[j].transform.position = new Vector3(-5.5f + 2.0f*j, 1.75f, 0.0f);
                recipeTrials[i].GetComponent<Recipe>().ingredients[j].transform.localScale = new Vector3(5f, 5f, 5f);
                
                recipeTrials[i].GetComponent<Recipe>().ingredients[j].AddComponent<BoxCollider2D>();

            }
            
        }

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

    void FixedUpdate(){
       spawnIngredientOnBelt();
    }

    private void spawnIngredientOnBelt(){

        
        GameObject ingredient = Instantiate(ingredientPrefab, new Vector3(9, -8, 0), Quaternion.identity);
        ingredient.name = "applepie_apple";//$"Recipe {i} - Ingredient {j}";

        Sprite ingredientSprite = Resources.Load<Sprite>($"Sprites/menu/applepie/applepie_apple");

        ingredient.GetComponent<SpriteRenderer>().sprite = ingredientSprite;
        ingredient.GetComponent<SpriteRenderer>().sortingOrder = 1;
        ingredient.transform.parent = ingredient.transform;
        ingredient.transform.localScale = new Vector3(5f, 5f, 5f);
        ingredient.AddComponent<BoxCollider2D>();

        GameObject np = new GameObject();
        np.AddComponent<BorderPathMovement>().name = "path";
        np.GetComponent<BorderPathMovement>().obj = ingredient;
        np.GetComponent<BorderPathMovement>().pathPoints = setPathPoints;
        np.GetComponent<BorderPathMovement>().numPoints = 4;
        np.GetComponent<BorderPathMovement>().speed = 2;

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
    // private KeyValuePair<string, string[]> GetRandomRecipe()
    // {
    //     int randomRecipeIndex = GetRandInt(0, recipeNames.Count);
    //     return new KeyValuePair<string, string[]>(recipeNames[randomRecipeIndex], recipes[recipeNames[randomRecipeIndex]]);
    // }

    private KeyValuePair<string, string[]> GetRandomRecipe()
    {
        int randomRecipeIndex = GetRandInt(0, recipeNames.Count);
        return new KeyValuePair<string, string[]>(recipeNames[randomRecipeIndex], recipes[recipeNames[randomRecipeIndex]]);
    }

}
