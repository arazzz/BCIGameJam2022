using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RecipeTrial))]
public class GameManager : MonoBehaviour
{

    public static int numRecipeTrials = 1;
    public static int numIngredients = 3;
    public int currentRecipeTrial = 0;

    public GameObject[] recipeTrials = new GameObject[numRecipeTrials];
    public GameObject ingredientObject;

    public recipes = Dictionary<string, string[]> dict = new Dictionary<string, string[]>
    {   
        // Cheese + Flour + Tomato = Pizza
        { "pizza", new [] {
            "Sprites/Food-2/225 - icons pack sprite sheet_128", // Cheese 
            "Sprites/Food-2/225 - icons pack sprite sheet_64",  // Tomato 
            "Sprites/Food-2/225 - icons pack sprite sheet_50" // Dough ?
        } },
    };
    public List<string> recipeNames = new List<string>(dict.Keys);

    // Start is called before the first frame update
    void Start()
    {
        // ingredientObject = new GameObject();
        // int ingredientSpriteIndex = GetRandInt(222, 239);
        // ingredientObject.name = "Sample Ingredient";
        // ingredientObject.AddComponent<SpriteRenderer>();
        // ingredientObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>($"Sprites/FOOD 1_{ingredientSpriteIndex}");
        // ingredientObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
        // ingredientObject.AddComponent<BoxCollider2D>();
        
        // // Position the ingredient object
        // ingredientObject.transform.position = new Vector3(0, 0, 0);

        

        // for (int i = 0; i < numRecipeTrials; i++)
        // {
        //     recipeTrials[i] = new GameObject();
        //     recipeTrials[i].name = "Recipe Trial " + i;
        //     recipeTrials[i].AddComponent<RecipeTrial>();
        //     recipeTrials[i].GetComponent<RecipeTrial>().ingredients = new GameObject[numIngredients];
        //     recipeTrials[i].GetComponent<RecipeTrial>().ingredients[0] = ingredientObject;
        //     recipeTrials[i].GetComponent<RecipeTrial>().ingredients[1] = ingredientObject;
        //     recipeTrials[i].GetComponent<RecipeTrial>().ingredients[2] = ingredientObject;
        // }

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
        return Random.Range(minimum, maximum);
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

    // Get random recipe entry
    private KeyValuePair<string, string[]> GetRandomRecipe()
    {
        int recipeIndex = GetRandInt(0, recipeNames.Count);
        return recipes[recipeNames[recipeIndex]];
    }

}
