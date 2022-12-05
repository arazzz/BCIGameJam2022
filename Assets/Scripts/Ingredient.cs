using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Ingredient : MonoBehaviour
{

    // The ingredient game object
    // public GameObject ingredient;
    // public GameObject gameManager;

    void Awake() {

        // Find Game Manager and get the food sprites
        

        // Dictionary<string, object> foodSprites = gameManager.GetComponent<GameManager>().foodSprites;

        // Get random sprite from food sprites
        // Sprite randomSprite = GetRandomSprite(foodSprites);

        // Set the sprite of the ingredient
        // ingredient.GetComponent<SpriteRenderer>().sprite = randomSprite;

    }

    // Start is called before the first frame update
    void Start()
    {
        // gameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // On pointer click, debug the name of the ingredient
    public void onMouseDown() {
        Debug.Log("Clicked on " + gameObject.name);
    }

}
