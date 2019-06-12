using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D body;
    public float speed = 5;


    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        // default zero velocity if no inputs
        Vector2 velocity = Vector2.zero;


        // add vectors depending on key pressed
        if (Input.GetKey(KeyCode.W))
        {
            velocity += Vector2.up;
        }
        if (Input.GetKey(KeyCode.S))
        {
            velocity += Vector2.down;
        }
        if (Input.GetKey(KeyCode.D))
        {
            velocity += Vector2.right;
        }
        if (Input.GetKey(KeyCode.A))
        {
            velocity += Vector2.left;
        }

        float angle = Vector2.SignedAngle(Vector2.right, velocity);

        if (velocity.sqrMagnitude > 0)
            transform.rotation = Quaternion.Euler(0, 0, angle + 90);
        // make velocity go in correct direction at correct speed
        body.velocity = speed * velocity.normalized;
    }

}
