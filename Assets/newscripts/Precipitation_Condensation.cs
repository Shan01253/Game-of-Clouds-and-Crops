using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Precipitation_Condensation : MonoBehaviour
{

    private float percentFull = 0;

    public float percentPerRain = .3f;
    [SerializeField]
    private float amountPerClick = 5f;
    [SerializeField]
    private float innerRadius = 1.0f;
    [SerializeField]
    private float outerRadius = 2.0f;

    private static event Action<float> Precipitation_Listeners;
    private static event Action<float> Condensation_Listeners;

    public static void Precipitation_Subscribe(Action<float> func)
    {
        Precipitation_Listeners += func;
    }
    public static void Precipitation_Unsubscribe(Action<float> func)
    {
        Precipitation_Listeners -= func;
    }

    public static void Condensation_Subscribe(Action<float> func)
    {
        Condensation_Listeners += func;
    }
    public static void Condensation_Unsubscribe(Action<float> func)
    {
        Condensation_Listeners -= func;
    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Fire1") && percentFull > percentPerRain)
        {
            Precipitation_Listeners?.Invoke(3);
            int layerMask = 1 << LayerMask.NameToLayer("Tile");

            // Water tiles within inner radius
            Collider2D[] tiles = Physics2D.OverlapCircleAll(transform.position, innerRadius, layerMask);
            foreach (Collider2D coll in tiles)
            {
                Debug.Log("WATERED " + amountPerClick);
                coll.GetComponent<TileContainer>().Water(amountPerClick);
            }
            StartCoroutine(rainVisualize());
            Precipitation_Listeners?.Invoke(percentPerRain);
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
