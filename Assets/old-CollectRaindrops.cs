using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CollectRaindrops : MonoBehaviour
{
    public int withDropCapacity = 21;
    private static event Action<int> Listeners;
 
    public static void Subscribe(Action<int> listener_function)
    {
        Listeners += listener_function;
    }

    public static void Unsubscribe(Action<int> listener_function)
    {
        Listeners -= listener_function;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("drop"))
        {
            collision.gameObject.SetActive(false);
            Listeners?.Invoke(withDropCapacity);
        }
    }

     
}
