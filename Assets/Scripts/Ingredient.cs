using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{

    // The ingredient game object
    public GameObject gameManager;
    private bool sendDown = false;

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
        gameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (sendDown == true)
        {
            FallDown();
        }
    }

    private void OnMouseDown()
    {
        if (transform.position.y > 7 && transform.position.x > -6.5 && transform.position.x < 5)
        {
            sendDown = true;
            gameObject.AddComponent<Rigidbody2D>();
        }
    }

    private void FallDown()
    {     
        if (gameObject.transform.position.y < -5)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        }
    }

    // Get random sprite from food sprites
    Sprite GetRandomSprite(Dictionary<string, object> foodSprites) {

        // Get random sprite from food sprites
        Sprite[] sprites = (Sprite[])foodSprites["Food-2"];
        Sprite randomSprite = sprites[Random.Range(0, sprites.Length)];

        return randomSprite;

    }

}
