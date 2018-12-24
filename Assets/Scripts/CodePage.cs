using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "CodePage", menuName = "Code Page")]
public class CodePage : ScriptableObject
{
    public static CodePage ActivePage
    {
        get
        {
            return _activePage;
        }

        set
        {
            _activePage = value;
            if (OnActivePageChange != null) OnActivePageChange(value);
        }
    }
    private static CodePage _activePage;
    public static event Action<CodePage> OnActivePageChange;

    public List<CodeBranch> Branches;
}

[Serializable]
public class CodeBranch
{
    public List<CodeLine> Lines;

    public List<AtomicAction> Actions
    {
        get
        {
            List<AtomicAction> output = new List<AtomicAction>();
            foreach (var line in Lines)
            {
                output.AddRange(line.Tokens);
            }
            return output;
        }
    }

    public string Image (int branchIndex)
    {
        var img = "Branch " + branchIndex.ToString() + ":";
        foreach (var ln in Lines)
        {
            img += "\n " + ln.Image();
        }
        return img;
    }
}

[Serializable]
public class CodeLine
{
    public List<AtomicAction> Tokens;

    public string Image ()
    {
        var img = "";
        foreach (var tok in Tokens)
        {
            img += tok.SourceDisplayString + " ";
        }
        return img;
    }
}
