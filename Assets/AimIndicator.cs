using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class AimIndicator : MonoBehaviour
{
    private Player player;
    private Vector2 aimDirection;

    // Start is called before the first frame update
    void Start()
    {
        player = ReInput.players.GetPlayer(0);
        aimDirection = Vector2.up;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newDirection = player.GetAxis2D("Move Horizontal", "Move Vertical");
        aimDirection = newDirection.sqrMagnitude > 0 ? newDirection : aimDirection;
    }

    private void LateUpdate()
    {
        float angle = Vector2.SignedAngle(Vector2.right, aimDirection);
        // lock aim angle to 8 directions
        angle = Mathf.Round(angle / 45) * 45;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }
}
