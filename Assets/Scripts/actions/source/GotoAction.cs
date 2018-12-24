using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GotoAction", menuName = "Actions/Goto")]
public class GotoAction : AtomicAction
{
    public CodePage TargetPage;

	public override string SourceDisplayString => "GOTO " + TargetPage.GetHashCode().ToString();
	public override string RegisterDisplayString => SourceDisplayString;

	public override IEnumerator Execute()
	{
		CodePage.ActivePage = TargetPage;
        yield return null;
	}
}
