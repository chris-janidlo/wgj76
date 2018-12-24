using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LogAction", menuName = "Actions/Log")]
public class LogAction : AtomicAction
{
    public string PrintValue;

	public override string SourceDisplayString => "PRINT '" + PrintValue + "'";
	public override string RegisterDisplayString => "PT '" + PrintValue + "'";

	public override IEnumerator Execute()
	{
        Debug.Log(PrintValue);
        yield return null;
	}
}
