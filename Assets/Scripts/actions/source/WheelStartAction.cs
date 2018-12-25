using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WheelStart", menuName = "Actions/Wheels/Accelerate All")]
public class WheelStartAction : AtomicAction
{
	[Tooltip("Curve that starts at (0, 0) and arrives at (t, 1) where t is the time of the final key")]
	public AnimationCurve AccelCurve;
	public override string SourceDisplayString => "Start wheels";
	public override string RegisterDisplayString => SourceDisplayString;

	public override IEnumerator Execute()
	{
		float? leftTorque = WheelDriver.Instance.PopWheelPercents(WheelIndicator.Left);
		float? rightTorque = WheelDriver.Instance.PopWheelPercents(WheelIndicator.Right);

		var len = AccelCurve.keys.Length;
		float maxTime = AccelCurve.keys[len - 1].time;
		float timer = 0;
		while (timer < maxTime)
		{
			var val = AccelCurve.Evaluate(timer);
			WheelDriver.Instance.TryTorqueBoth(leftTorque * val, rightTorque * val);
			timer += Time.deltaTime;
			yield return null;
		}

		WheelDriver.Instance.TryTorqueBoth(leftTorque, rightTorque);
		WheelDriver.Instance.SetIdleState(true, WheelIndicator.Left, WheelIndicator.Right);
	}
}
