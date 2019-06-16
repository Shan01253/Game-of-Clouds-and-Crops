using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D body;
    public float speed = 5;

    private Player player;

    private void Awake()
    {
        player = ReInput.players.GetPlayer(0);
    }

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // default zero velocity if no inputs
        Vector2 velocity = player.GetAxis2D("Move Horizontal", "Move Vertical");

        float angle = Vector2.SignedAngle(Vector2.right, velocity);

        // lock movement angle to 8 directions
        angle = Mathf.Round(angle / 45) * 45;

        if (velocity.sqrMagnitude > 0)
        {
            // velocity is adjusted for 8-direction movement only if sqrMagnitude > 0
            // because cos(0) = 1, making the player move right with no input
            float rad = angle * Mathf.Deg2Rad;
            velocity = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

            transform.rotation = Quaternion.Euler(0, 0, angle + 90);
        }
        // make velocity go in correct direction at correct speed
        body.velocity = speed * velocity.normalized;
    }

}
