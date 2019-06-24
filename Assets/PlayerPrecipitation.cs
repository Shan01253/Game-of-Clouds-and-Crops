using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrecipitation : MonoBehaviour
{
    [SerializeField]
    private float amountPerClick = 5f;
    [SerializeField]
    private float innerRadius = 1.0f;
    [SerializeField]
    private float outerRadius = 2.0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            int layerMask = 1 << LayerMask.NameToLayer("Tile");

            // Water tiles within inner radius
            Collider2D[] tiles = Physics2D.OverlapCircleAll(transform.position, innerRadius, layerMask);
            foreach (Collider2D coll in tiles)
            {
                coll.GetComponent<TileContainer>().Water(amountPerClick);
            }

            // Water tiles within outer radius
            tiles = Physics2D.OverlapCircleAll(transform.position, outerRadius, layerMask);
            foreach (Collider2D coll in tiles)
            {
                coll.GetComponent<TileContainer>().Water(amountPerClick);
            }
        }
    }
}
