using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;

public class Precipitation_Condensation : MonoBehaviour
{

    private float percentFull = 0;
    [SerializeField]
    [Range(0, 1)]
    private float percent_FillMeter_PerCondensate = .2f;

    [SerializeField]
    [Range(0, 1)]
    private float percent_DepleteMeter_PerPrecipitate = .3f;

    private GridManager GM;

    [SerializeField]
    [Range(0, 1)]
    private float percent_WaterFarm_PerClick = 1;
    //[SerializeField]
    //private float innerRadius = 1.0f;
    //[SerializeField]
    //private float outerRadius = 2.0f;

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

    private void Awake()
    {
        GM = FindObjectOfType<GridManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Fire1") && percentFull > percent_DepleteMeter_PerPrecipitate)
        {
            int layerMask = 1 << LayerMask.NameToLayer("Tile");

            percentFull -= percent_DepleteMeter_PerPrecipitate;

            // Water tiles within inner radius


            //Collider2D[] tiles = Physics2D.OverlapCircleAll(transform.position, innerRadius, layerMask);
            //foreach (Collider2D coll in tiles)
            //{
            //    Debug.Log("WATERED " + amountPerClick);
            //    coll.GetComponent<TileContainer>().Water(amountPerClick);
            //}
            //// Water tiles within outer radius
            //tiles = Physics2D.OverlapCircleAll(transform.position, outerRadius, layerMask);
            //foreach (Collider2D coll in tiles)
            //{
            //    coll.GetComponent<TileContainer>().Water(amountPerClick);
            //}
            waterTilesBelow(transform.position, percent_WaterFarm_PerClick);

            StartCoroutine(rainVisualize());
            Precipitation_Listeners?.Invoke(percent_DepleteMeter_PerPrecipitate);
        }
    }

    private void waterTilesBelow(Vector3 position, float waterAmount)
    {
        Vector3Int v = Vector3Int.RoundToInt(position);

        GM.GetTile(v).Water(waterAmount);
        //GM.GetTile(v).gameObject.GetComponent<SpriteRenderer>().color = Color.black;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("drop"))
        {
            collision.gameObject.SetActive(false);
            Condensation_Listeners?.Invoke(percent_FillMeter_PerCondensate);

            if (percentFull < 1)
            {
                percentFull += percent_FillMeter_PerCondensate;
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
    //private void OnDrawGizmos()
    //{
    //    if (raining)
    //    {
    //        Gizmos.color = Color.blue;
    //        Gizmos.DrawWireSphere(transform.position, innerRadius);
    //        Gizmos.DrawWireSphere(transform.position, outerRadius);
    //    }
    //}
}
