using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{

    void Start() {

    }

    public void MoveAmount(Vector2 moveAmount) {

        // float deltaY = moveAmount.y;
        // Vector2 p = transform.position;

        transform.Translate(moveAmount);

    }

}
