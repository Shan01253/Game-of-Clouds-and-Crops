using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class emptyOnSquirt : MonoBehaviour
{
    Image img;
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        Precipitation_Condensation.Precipitation_Subscribe(Empty);
    }

    void Empty(float totalNumberEmpties)
    {
        img.fillAmount -= 1.0f / totalNumberEmpties;
    }


}
