using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class StatusDisplay : MonoBehaviour
{
    public Transform EyeStem;

    Text text;
    float leftPow, rightPow;

    void Start ()
    {
        text = GetComponent<Text>();
    }

    void Update ()
    {
        var t_health = RobotBody.Instance.HealthStatusString();
        leftPow = Mathf.Lerp(leftPow, Mathf.Round(WheelDriver.Instance.GetPower(WheelIndicator.Left) * 100), .5f);
        rightPow = Mathf.Lerp(rightPow, Mathf.Round(WheelDriver.Instance.GetPower(WheelIndicator.Right) * 100), .5f);

        float leftT = WheelDriver.Instance.GetTorque(WheelIndicator.Left), rightT = WheelDriver.Instance.GetTorque(WheelIndicator.Right);

        string
            __s = $"<color={t_health.Item2}>{t_health.Item1.PadRight(5)}</color>",
            ____b = "<empty>",
            d = transformHeading(RobotBody.Instance.transform),
            D = transformHeading(EyeStem),
            _p = (leftPow < 0 ? "-" : " ") + Mathf.Round(Mathf.Abs(leftPow)).ToString().PadRight(3),
            _P = (rightPow < 0 ? "-" : " ") + Mathf.Round(Mathf.Abs(rightPow)).ToString().PadRight(3),
            t = "[" + (WheelDriver.Instance.GetThrottled(WheelIndicator.Left) ? "x" : " ") + "]",
            T = "[" + (WheelDriver.Instance.GetThrottled(WheelIndicator.Right) ? "x" : " ") + "]",
            _s = (leftT < 0 ? "-" : " " ) + Mathf.Round(Mathf.Abs(leftT)).ToString(),
            _S = (rightT < 0 ? "-" : " " ) + Mathf.Round(Mathf.Abs(rightT)).ToString();
       
        text.text =
        // must be within 40 characters (next line is 40 -s):
        //    ----------------------------------------
            $"Status: {__s} Direction    PWR THROT SPD\n" +
            $"Bay: {____b} Body {d}   L {_p}  {t} {_s}\n" +
            $"             Cam. {D}   R {_P}  {T} {_S}";
    }

    string transformHeading (Transform tform)
    {
        var deg = tform.rotation.eulerAngles.y;

        // clockwise from the top
        string[] heading_names = {"N", "NNE", "NE", "ENE", "E", "ESE", "SE", "SSE", "S", "SSW", "SW", "WSW", "W", "WNW", "NW", "NNW"};

        float range = 360f / heading_names.Length;
        // finds the range° window centered at deg, and converts it to an index
        int index = Mathf.FloorToInt((deg + range/2) / range) % heading_names.Length;
        
        return heading_names[index].PadRight(3);
    }
}
