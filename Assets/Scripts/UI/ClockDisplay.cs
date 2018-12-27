using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ClockDisplay : MonoBehaviour
{
    public AnimationCurve PercentToLerpolant;
    public Color FlashColor;
    
    Image image;

    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        var lerpolant = PercentToLerpolant.Evaluate(Clock.Instance.PercentLeftUntilTick);
        image.color = Color.Lerp(Color.white, FlashColor, lerpolant);
    }
}
