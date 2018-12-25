using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AtomicAction : ScriptableObject
{
    public abstract string SourceDisplayString { get; } // <= 17 characters
    public abstract string RegisterDisplayString { get; } // <= 16 characters

    public abstract IEnumerator Execute ();
}
