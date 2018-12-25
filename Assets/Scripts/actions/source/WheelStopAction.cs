using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WheelStop", menuName = "Actions/Wheels/Decelerate All")]
public class WheelStopAction : AtomicAction
{
	[Tooltip("Curve that starts at (0, 1) and arrives at (t, 0) where t is the time of the final key")]
    public AnimationCurve DecelCurve;
	public override string SourceDisplayString => "Stop wheels";
	public override string RegisterDisplayString => SourceDisplayString;

	public override IEnumerator Execute()
	{
        float leftTorque = WheelDriver.Instance.GetWheelTorquePercent(WheelIndicator.Left);
        float rightTorque = WheelDriver.Instance.GetWheelTorquePercent(WheelIndicator.Right);

        var len = DecelCurve.keys.Length;
        float maxTime = DecelCurve.keys[len - 1].time;
        float timer = 0;
        while (timer < maxTime)
        {
            var val = DecelCurve.Evaluate(timer);
            WheelDriver.Instance.TryTorqueBoth(leftTorque * val, rightTorque * val);
            timer += Time.deltaTime;
            yield return null;
        }

        WheelDriver.Instance.TryTorqueBoth(0, 0);
	}
}
