using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Rain_FillDeplete : MonoBehaviour
{
    Image img;
    public Precipitation_Condensation PC;
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();

        Precipitation_Condensation.Condensation_Subscribe(Fill);
        Precipitation_Condensation.Precipitation_Subscribe(Empty);
    }

    void Fill(float percentFill)
    {
        img.fillAmount += percentFill;
    }

    void Empty(float percentDeplete)
    {
        img.fillAmount -= percentDeplete;
    }
}
