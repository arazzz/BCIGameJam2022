using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(PlayerPhysics))]
public class PlayerController : MonoBehaviour
{

    public float speed = 8;
    public float acceleration = 12;

    private float currentSpeed;
    private float targetSpeed;
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
        // targetSpeed = Input.GetAxisRaw("Horizontal") * speed;
        targetSpeed = speed;
        currentSpeed = IncrementTowards(currentSpeed, targetSpeed, acceleration);

        if (transform.position.x > maxPositionX) {
            // direction = -1;
            this.transform.Rotate(0, 180, 0);
        } else if (transform.position.x < minPositionX) {
            // direction = 1;
            this.transform.Rotate(0, -180, 0);
        }

        amountToMove = new Vector2(currentSpeed, 0);
        // playerPhysics.MoveAmount(direction*amountToMove * Time.deltaTime);
        playerPhysics.MoveAmount(amountToMove * Time.deltaTime);

        if (Input.GetKeyDown (KeyCode.Space)) {
            
            anim.Play("chef_holding");

            if (triggerEntered == true) {

                // Debug.Log(gameManager.GetComponent<GameManager>().currentRecipe[0]);
                
                // if (itemCollidedWith.gameObject.name.)
                // Debug.Log("Current recipe: " + gameManager.GetComponent<GameManager>().currentRecipe);
                // Debug.Log("Item collided with: " + itemCollidedWith.gameObject.name);
                // if (gameManager.GetComponent<GameManager>().currentRecipe.Any(itemCollidedWith.gameObject.name.Contains)) {
                //     Debug.Log("Correct ingredient!");
                //     // gameManager.currentRecipe.Remove(itemCollidedWith.gameObject.name);
                //     Destroy(itemCollidedWith.gameObject);
                // } else {
                //     anim.Play("chef_panic");
                // }

                Debug.Log(itemCollidedWith.gameObject.name);

            }
        }

    }

    private float IncrementTowards(float n, float target, float a) {
        if (n == target) {
            return n;
        } else {
            float dir = Mathf.Sign(target - n); // must n be increased or decreased to get closer to target
            n += a*Time.deltaTime * dir;
            return (dir == Mathf.Sign(target - n)) ? n : target; // if n has now passed target then return target, otherwise return n
        }
    }

    private void OnTriggerEnter2D(Collider2D foodItem) {
        triggerEntered = true;
        itemCollidedWith = foodItem;
        Debug.Log("Trigger entered");
    }

    private void OnTriggerExit2D() {
        triggerEntered = false;
    }

}
