using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SourceDisplay : MonoBehaviour
{
    public BranchButton BranchDisplayPrefab;

    List<BranchButton> branchDisplays;

    void Awake ()
    {
        CodePage.OnActivePageChange += switchPage;
        branchDisplays = new List<BranchButton>();
    }

    void switchPage (CodePage newPage)
    {
        foreach (var b in branchDisplays)
        {
            Destroy(b.gameObject);
        }

        branchDisplays = new List<BranchButton>();

		for (int i = 0; i < newPage.Branches.Count; i++)
        {
			CodeBranch branch = newPage.Branches[i];
			BranchButton button = Instantiate(BranchDisplayPrefab);
            button.transform.SetParent(transform);
            button.Actions = branch.Actions;
            // add one line for the "branch n" line
            button.SetText(branch.Image(i), branch.Lines.Count + 1);

            branchDisplays.Add(button);
        }
    }
}
