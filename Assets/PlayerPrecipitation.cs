using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerPrecipitation : MonoBehaviour
{
    private float percentfull = 0;
    public float percentPerRain = .3f;
    [SerializeField]
    private float amountPerClick = 5f;
    [SerializeField]
    private float innerRadius = 1.0f;
    [SerializeField]
    private float outerRadius = 2.0f;

    private static event Action<float> Listeners;

    public static void subscribe(Action<float> func)
    {
        Listeners += func;
    }
    public static void unsubscribe(Action<float> func)
    {
        Listeners -= func;
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(CollectRaindrops.percentFull());
        if (Input.GetButtonDown("Fire1") && percentfull > percentPerRain)
        {
            Listeners?.Invoke(3);
            int layerMask = 1 << LayerMask.NameToLayer("Tile");

            // Water tiles within inner radius
            Collider2D[] tiles = Physics2D.OverlapCircleAll(transform.position, innerRadius, layerMask);
            foreach (Collider2D coll in tiles)
            {
                Debug.Log("WATERED " + amountPerClick);
                coll.GetComponent<TileContainer>().Water(amountPerClick);
            }
            StartCoroutine(rainVisualize());
            Listeners?.Invoke(percentPerRain);
            // Water tiles within outer radius
            tiles = Physics2D.OverlapCircleAll(transform.position, outerRadius, layerMask);
            foreach (Collider2D coll in tiles)
            {
                coll.GetComponent<TileContainer>().Water(amountPerClick);
            }
        }
    }
    IEnumerator rainVisualize()
    {
        raining = true;
        yield return new WaitForSeconds(.3f);
        raining = false;
    }

    bool raining = false;
    private void OnDrawGizmos()
    {
        if (raining)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, innerRadius);
            Gizmos.DrawWireSphere(transform.position, outerRadius);
        }
    }
}
