using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WheelThrottle", menuName = "Actions/Wheels/Set Throttle")]
public class WheelThrottleAction : AtomicAction
{
	public bool ThrottleValue;
	public List<WheelIndicator> WheelsToChange;

	public override string SourceDisplayString {
		get
		{
			var result = (ThrottleValue ? "Stop" : "Start") + " wheel";
			foreach (var w in WheelsToChange)
			{
				result += " " + w.ToShortString();
			}
			return result;
		}
	}
	public override string RegisterDisplayString => SourceDisplayString;

	public override IEnumerator Execute()
	{
		foreach (var w in WheelsToChange)
		{
			WheelDriver.Instance.SetThrottled(w, ThrottleValue);
		}
		yield return null;
	}
}
