using System.Collections;

public interface IAtomicAction
{
    string RegisterName { get; }
    IEnumerator Execute ();
}
