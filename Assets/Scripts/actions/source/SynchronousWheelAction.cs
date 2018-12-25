using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SynchWheelAction", menuName = "Actions/Wheels/Move Single")]
public class SynchronousWheelAction : AtomicAction
{
    public WheelIndicator Wheel;
    [Tooltip("Values interpreted as percent of MaxTorque; positive values move forward, negative values move backward. Values outside of range [-1, 1] will cause an exception to be thrown.")]
    public AnimationCurve TorqueCurve;

	public override string SourceDisplayString => "MV " + Wheel;
	public override string RegisterDisplayString => SourceDisplayString;

	public override IEnumerator Execute()
	{
        var len = TorqueCurve.keys.Length;
        float timeMax = TorqueCurve.keys[len - 1].time;
        float timer = 0;
        while (timer <= timeMax)
        {
            WheelDriver.Instance.TorqueWheel(Wheel, TorqueCurve.Evaluate(timer));
            timer += Time.deltaTime;
            yield return null;
        }
        WheelDriver.Instance.TorqueWheel(Wheel, TorqueCurve.keys[len - 1].value);
	}
}
