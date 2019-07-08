using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Rain_FillDeplete : MonoBehaviour
{
    Image img;
    private float targetFill = 0;
    public Precipitation_Condensation PC;
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();

        Precipitation_Condensation.Condensation_Subscribe(Fill);
        Precipitation_Condensation.Precipitation_Subscribe(Empty);
    }
    float currentVelocity = 0;
    public float smoothTime = .2f;
    private void Update()
    {
        img.fillAmount = Mathf.SmoothDamp(img.fillAmount, targetFill, ref currentVelocity, smoothTime);
    }

    void Fill(float percentFill)
    {
        if (targetFill + percentFill < 1)
            targetFill += percentFill;
        else
            targetFill = 1;

    }

    void Empty(float percentDeplete)
    {
        if (targetFill - percentDeplete > 0)
            targetFill -= percentDeplete;
        else
            targetFill = 0;
    }
}
