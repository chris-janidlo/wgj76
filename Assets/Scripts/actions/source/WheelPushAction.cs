using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WheelPush", menuName = "Actions/Wheels/Push Data")]
public class WheelPushAction : AtomicAction
{
    public WheelIndicator Wheel;
    [Range(-1, 1)]
    public float TorquePercent;

	public override string SourceDisplayString =>
        Wheel.ToShortString() + " throttle @ " + PercentageString;
	public override string RegisterDisplayString =>
        Wheel.ToShortString() + " THROT: " + PercentageString;
    
    string PercentageString => Mathf.Round(TorquePercent * 100) + "%";

	public override IEnumerator Execute()
	{
        WheelDriver.Instance.StoreWheelPercent(Wheel, TorquePercent);
        yield return null;
	}
}
