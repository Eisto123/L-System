using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TreeData", menuName = "ScriptableObjects/TreeDataScriptableObject", order = 1)]
public class TreeDataSO : ScriptableObject
{
    public int iteration;
    public float angle;
    public string axiom;
    public float length;
    public bool hasLeaf;
    public char[] RulesKey;
    public string[] RulesValue;
}
