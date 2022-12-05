using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerPhysics))]
public class PlayerController : MonoBehaviour
{

    public float speed = 8;

    private Vector3 amountToMove;

    private PlayerPhysics playerPhysics;

    // private int direction = 1;
    private float maxPositionX = 7;
    private float minPositionX = -7;

    private bool triggerEntered = false;
    private Collider2D itemCollidedWith;

    // Start is called before the first frame update
    void Start()
    {
        playerPhysics = GetComponent<PlayerPhysics>();
    }

    // Update is called once per frame
    void Update()
    {
        
        amountToMove = new Vector3(speed, 0, 0);
        transform.Translate(amountToMove * Time.deltaTime);

        // if (transform.position.x > maxPositionX) {
        //     // direction = -1;
        //     this.transform.Rotate(0, 180, 0);
        // } else if (transform.position.x < minPositionX) {
        //     // direction = 1;
        //     this.transform.Rotate(0, -180, 0);
        // }

        if (triggerEntered) {
            if (itemCollidedWith.gameObject.name == "Right Border") {
                this.transform.Rotate(0, 180, 0);
                triggerEntered = false;
            } else if (itemCollidedWith.gameObject.name == "Left Border") {
                this.transform.Rotate(0, -180, 0);
                triggerEntered = false;
            }
        }

        

        // amountToMove = new Vector2(currentSpeed, 0);
        // playerPhysics.MoveAmount(direction*amountToMove * Time.deltaTime);

        // if (Input.GetKeyDown (KeyCode.Space) && triggerEntered == true) {
        if (triggerEntered == true) {
            Debug.Log(itemCollidedWith.gameObject.name);
        }

    }

    private void OnTriggerEnter2D(Collider2D item) {
        triggerEntered = true;
        itemCollidedWith = item;
    }

    private void OnTriggerExit2D() {
        triggerEntered = false;
    }

    /** 

        - Cheese + Flour + Tomato = Pizza
    
    **/

}
