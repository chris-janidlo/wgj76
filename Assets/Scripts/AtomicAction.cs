using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AtomicAction : ScriptableObject
{
    public abstract string SourceDisplayString { get; }
    public abstract string RegisterDisplayString { get; }

    public abstract IEnumerator Execute ();
}
