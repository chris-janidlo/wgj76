using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ClockDisplay : MonoBehaviour
{
    public int Segments;
    [Range(0, 1)]
    [Tooltip("How much of the tick time to spend doing a flashing animation.")]
    public float FlashPercent;
    public Color FlashColor;
    
    Image image;
    float invPercent { get { return 1 - FlashPercent; } }

    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        var percent = Clock.Instance.PercentLeftUntilTick;
        
        var fillAmount = Mathf.Clamp(percent, 0, invPercent) / invPercent;
        image.fillAmount = Mathf.Round(fillAmount * Segments) / Segments;
        
        if (percent >= invPercent)
        {
            image.color = Color.Lerp(FlashColor, Color.white, (1f - percent) * 10);
        }
        else
        {
            image.color = Color.white;
        }
    }
}
