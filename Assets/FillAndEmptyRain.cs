using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FillAndEmptyRain : MonoBehaviour
{

    Image img;
    public CollectRaindrops collector;
    public PlayerActions actions;

    // Start is called before the first frame update
    void Awake()
    {
        img = GetComponent<Image>();
        collector.Subscribe(Fill);
        actions.subscribe_Spritz(Empty);
    }

    void Fill(int totalNumberDrops)
    {
        img.fillAmount += 1.0f / totalNumberDrops;
    }

    void Empty(int totalNumberEmpties)
    {
        img.fillAmount -= 1.0f / 3;
    }
}
