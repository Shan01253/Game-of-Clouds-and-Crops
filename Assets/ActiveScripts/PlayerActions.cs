﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerActions : MonoBehaviour
{
    public Sprite normalSprite;       // sprite to display normally
    public Sprite panicSprite;        // sprite to display when shift is pressed
    public float waitTime;
    private SpriteRenderer sr;       // wanna sprite cranberry
    public bool displayAnim;



    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        displayAnim = false;
    }

    // Update is called once per frame
    void Update()
    {
       //if (Input.GetButtonUp("Fire1")) {
       //    Debug.Log("testing");
       //    StartCoroutine(panicTemp());

       //}
    }

    //IEnumerator panicTemp() {
    //    sr.sprite = panicSprite;
    //    sr.sortingLayerName = "front";
    //    yield return new WaitForSeconds(waitTime);
    //    sr.sortingLayerName = "Default";
    //    sr.sprite = normalSprite;
    //}
}
