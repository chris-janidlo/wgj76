using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Text))]
public class BranchButton : MonoBehaviour
{
    public List<AtomicAction> Actions;

    Button button;

    void Start ()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(click);
    }

    void Update ()
    {
        button.interactable = (CommandRegisters.Instance.Contents.Count + Actions.Count) <= CommandRegisters.Instance.Size;
    }

    public void SetText (string text, int lines)
    {
        GetComponent<Text>().text = text;

        var rt = GetComponent<RectTransform>();
        var sd = rt.sizeDelta;
        rt.sizeDelta = new Vector2(sd.x, GetComponent<Text>().fontSize * lines);
        rt.localScale = new Vector3(1, 1, 1); // counteract canvas resizing (if this line isn't here, the actual observed scale is 1/(resolution / base_resolution) which is tiny)
    }

    void click ()
    {
        CommandRegisters.Instance.AddActions(Actions);
        button.interactable = false;

        var next = CodePage.ActivePage.NextPage;
        if (next != null)
        {
            CodePage.ActivePage = CodePage.ActivePage.NextPage;
        }
    }
}
