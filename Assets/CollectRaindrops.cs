
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[RequireComponent(typeof(PlayerPrecipitation))]
public class CollectRaindrops : MonoBehaviour
{
    private PlayerPrecipitation PP;
    public float percentFillPerDrop = 0.2f;
    [SerializeField]
    private float percentFilledWithWater = 0;
    private static event Action<float> Listeners;

    private static CollectRaindrops Instance;

    private void Awake()
    {
        Instance = this;
        PlayerPrecipitation.subscribe(decreaseTank);
    }

    void decreaseTank(float percentLess)
    {
        if (percentLess > 1 || percentLess < 0)
        {
            Debug.Log("ERROR BAD ARGS", this);
            return;
        }

        if(percentFilledWithWater - percentLess > 0)
        {
            percentFilledWithWater -= percentLess;
        }
        else
        {
            percentFilledWithWater = 0;
        }
    }

    public static void Subscribe(Action<float> listener)
    {
        Listeners += listener;
    }

    public static void Unsubscribe(Action<float> listener)
    {
        Listeners -= listener;
    }
    public static float percentFull()
    {
        return Instance.percentFilledWithWater;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("drop"))
        {
            collision.gameObject.SetActive(false);
            Listeners?.Invoke(percentFillPerDrop);
            if (percentFilledWithWater < 1)
            {
                percentFilledWithWater += percentFillPerDrop;
            }
            else
            {
                percentFilledWithWater = 1;
            }
            
        }
    }


}

