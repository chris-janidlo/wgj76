using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class RegisterDisplay : MonoBehaviour
{
    public string EmptyText;

    Text text;
    
    void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        text.text = "";
        foreach (var action in CommandRegisters.Instance.Contents)
        {
            text.text += action.RegisterDisplayString + "\n";
        }

        if (text.text == "")
        {
            text.text = EmptyText;
        }
    }
}
