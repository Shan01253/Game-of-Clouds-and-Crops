using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class emptyOnSquirt : MonoBehaviour
{
    Image img;
    public PlayerActions actions;
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
<<<<<<< HEAD
        actions.subscribe_Spritz(empty);
=======
        Precipitation_Condensation.Precipitation_Subscribe(Empty);
>>>>>>> bd1864fff4fa447fd31c3cb9254e9c31304da467
    }

    void Empty(float totalNumberEmpties)
    {
        img.fillAmount -= 1.0f / totalNumberEmpties;
    }


}
