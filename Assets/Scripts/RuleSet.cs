using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuleSet : MonoBehaviour
{
    public InputField key;
    public InputField value;

    public void SetValue(char x, string rule){
        key.text = x.ToString();
        value.text = rule;
    }
}
