using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Precipitation_Condensation : MonoBehaviour
{

    private float percentFull = 0;

    public float percentPerCondensate = .2f;
    public float percentPerPrecipitate = .3f;


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

        if (Input.GetButtonDown("Fire1") && percentFull > percentPerPrecipitate)
        {
            int layerMask = 1 << LayerMask.NameToLayer("Tile");

            percentFull -= percentPerPrecipitate;

            // Water tiles within inner radius
            Collider2D[] tiles = Physics2D.OverlapCircleAll(transform.position, innerRadius, layerMask);
            foreach (Collider2D coll in tiles)
            {
                Debug.Log("WATERED " + amountPerClick);
                coll.GetComponent<TileContainer>().Water(amountPerClick);
            }
            // Water tiles within outer radius
            tiles = Physics2D.OverlapCircleAll(transform.position, outerRadius, layerMask);
            foreach (Collider2D coll in tiles)
            {
                coll.GetComponent<TileContainer>().Water(amountPerClick);
            }


            StartCoroutine(rainVisualize());
            Precipitation_Listeners?.Invoke(percentPerPrecipitate);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("drop"))
        {
            collision.gameObject.SetActive(false);
            Condensation_Listeners?.Invoke(percentPerCondensate);

            if (percentFull < 1)
            {
                percentFull += percentPerCondensate;
            }
            else
            {
                percentFull = 1;
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
