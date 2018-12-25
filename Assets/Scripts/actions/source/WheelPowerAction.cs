using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WheelPowerSet", menuName = "Actions/Wheels/Set Power")]
public class WheelPowerAction : AtomicAction
{
    public WheelIndicator Wheel;
    [Range(-1, 1)]
    public float Power;

    public override string SourceDisplayString => Wheel.ToShortString() + " wheel PWR " + PercentageString;
    public override string RegisterDisplayString => Wheel.ToShortString() + " PWR: " + PercentageString;
    
    string PercentageString => Mathf.Round(Power * 100) + "%";

    public override IEnumerator Execute()
    {
        WheelDriver.Instance.SetPower(Wheel, Power);
        yield return null;
    }
}
