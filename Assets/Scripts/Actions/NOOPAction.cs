using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NOOP", menuName = "Actions/NOOP")]
public class NOOPAction : AtomicAction
{
	public override string SourceDisplayString => "<do nothing>";
	public override string RegisterDisplayString => "NO OP";

	public override IEnumerator Execute()
	{
        yield return null;
	}
}
