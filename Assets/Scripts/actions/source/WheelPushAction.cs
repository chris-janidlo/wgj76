using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WheelPush", menuName = "Actions/Wheels/Push Data")]
public class WheelPushAction : AtomicAction
{
    public WheelIndicator Wheel;
    [Range(-1, 1)]
    public float TorquePercent;

	public override string SourceDisplayString => Wheel + " wheel at " + PercentageString + " PWR";
	public override string RegisterDisplayString => Wheel + " wheel: " + PercentageString;
    
    string PercentageString => string.Format("{0:P1}", TorquePercent);

	public override IEnumerator Execute()
	{
        WheelDriver.Instance.StoreWheelPercent(Wheel, TorquePercent);
        yield return null;
	}
}
