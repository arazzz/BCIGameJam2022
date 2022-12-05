using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(PlayerPhysics))]
public class PlayerController : MonoBehaviour
{

    public float speed = 8;
    private Vector2 amountToMove;

    private PlayerPhysics playerPhysics;

    // private int direction = 1;
    private float maxPositionX = 7;
    private float minPositionX = -7;

    private bool triggerEntered = false;
    private Collider2D itemCollidedWith;

    private Animator anim;
    
    // The game manager
    private GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
        playerPhysics = GetComponent<PlayerPhysics>();
        anim = gameObject.GetComponent<Animator>();

        // Get the game manager
        gameManager = this.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {

        amountToMove = new Vector2(speed, 0);
        transform.Translate(amountToMove * Time.deltaTime);
        
        if (triggerEntered) {
            if (itemCollidedWith.gameObject.name == "Right Border") {
                this.transform.Rotate(0, 180, 0);
                triggerEntered = false;
            } else if (itemCollidedWith.gameObject.name == "Left Border") {
                this.transform.Rotate(0, -180, 0);
                triggerEntered = false;
            }
        }

        if (triggerEntered) {
            Debug.Log(itemCollidedWith.gameObject.name);
        }

        // if (Input.GetKeyDown (KeyCode.Space)) {
            
        //     anim.Play("chef_holding");

        //     if (triggerEntered == true) {

        //         // Debug.Log(gameManager.GetComponent<GameManager>().currentRecipe[0]);
                
        //         // if (itemCollidedWith.gameObject.name.)
        //         // Debug.Log("Current recipe: " + gameManager.GetComponent<GameManager>().currentRecipe);
        //         // Debug.Log("Item collided with: " + itemCollidedWith.gameObject.name);
        //         // if (gameManager.GetComponent<GameManager>().currentRecipe.Any(itemCollidedWith.gameObject.name.Contains)) {
        //         //     Debug.Log("Correct ingredient!");
        //         //     // gameManager.currentRecipe.Remove(itemCollidedWith.gameObject.name);
        //         //     Destroy(itemCollidedWith.gameObject);
        //         // } else {
        //         //     anim.Play("chef_panic");
        //         // }

        //         Debug.Log(itemCollidedWith.gameObject.name);

        //     }
        // }

    }

    private void OnTriggerEnter2D(Collider2D item) {
        triggerEntered = true;
        itemCollidedWith = item;
    }

    private void OnTriggerExit2D() {
        triggerEntered = false;
    }

}
