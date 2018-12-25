using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaitFor", menuName = "Actions/Wait for Seconds")]
public class WaitAction : AtomicAction
{
    public float WaitTime;

	public override string SourceDisplayString => RegisterDisplayString + " seconds";
	public override string RegisterDisplayString => "Wait " + WaitTime;

	public override IEnumerator Execute()
	{
        yield return new WaitForSeconds(WaitTime);
	}
}
