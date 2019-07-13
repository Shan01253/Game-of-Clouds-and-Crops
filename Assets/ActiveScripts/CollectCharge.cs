using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public class CollectCharge : MonoBehaviour
{
    private static event Action<int> Listeners;
    public int withChargeCapacity = 63;
    // Start is called before the first frame update

    private void Start()
    {
        // Tell UniRX to, every update,
        var clickStream = Observable.EveryUpdate()
            // keep track of everytime we get w, s, d, or a inputs
            .Where(_ => Input.GetKeyDown(KeyCode.W) ||
                        Input.GetKeyDown(KeyCode.S) ||
                        Input.GetKeyDown(KeyCode.D) ||
                        Input.GetKeyDown(KeyCode.A));

        // group inputs into chunks 400 milleseconds from each other
        // call subscribers and begin scanning inputs anew after 4 inputs
        clickStream.Buffer(TimeSpan.FromMilliseconds(400), 4)
            // if you get at least 2 groups, call function to charge cloud
            .Where(xs => xs.Count >= 2)
            .Subscribe(xs => Charge(withChargeCapacity));
    }


    public static void Subscribe(Action<int> func)
    {
        Listeners += func;
    }

    public static void Unsubscribe(Action<int> func)
    {
        Listeners -= func;
    }

    public void Charge(int capacity)
    {
        Listeners?.Invoke(capacity);
    }
}
