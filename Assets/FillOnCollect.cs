﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FillOnCollect : MonoBehaviour
{
    Image img;
    public CollectRaindrops collector;
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        collector.Subscribe(Fill);
    }

    void Fill(int totalNumberDrops)
    {
        img.fillAmount += 1.0f/totalNumberDrops;
    }
}
