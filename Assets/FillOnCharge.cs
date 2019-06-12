using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillOnCharge : MonoBehaviour
{
    Image img;
    private void Start()
    {
        img = gameObject.GetComponent<Image>();
        CollectCharge.Subscribe(Fill);
    }

    void Fill(int chargeCapacity)
    {
        img.fillAmount += 1.0f / chargeCapacity;

    }
}
