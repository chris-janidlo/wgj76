using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimeMover : MonoBehaviour
{
    public CodePage InitialCodePage;

    void Start ()
    {
        CodePage.ActivePage = InitialCodePage;
    }
}
