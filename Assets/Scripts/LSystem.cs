using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;


public class LSystem : MonoBehaviour
{

    //public TreeDataSO treeDataSO;
    public int iteration;
    public float angle;
    public string axiom;
    public float Length;
    public bool hasLeaf;
    public GameObject Line;
    public GameObject Leaf;
    public Transform TreeHolder;
    public string currentString;
    private Vector3 initialPos;
    private Quaternion quaternion;
    private Dictionary<char,string> rules = new Dictionary<char,string>();
    private Stack<TransformInfo> transformStack = new Stack<TransformInfo>();

    // Start is called before the first frame update
    void Start()
    {
        initialPos =  transform.position;
        quaternion = transform.rotation;
        
    }

    public void LoadSO(TreeDataSO treeDataSO)
    {
        iteration = treeDataSO.iteration;
        angle = treeDataSO.angle;
        axiom = treeDataSO.axiom;
        Length = treeDataSO.length;
        hasLeaf = treeDataSO.hasLeaf;
        
        for(int i = 0; i< treeDataSO.RulesKey.Length; i++){
            if(rules.ContainsKey(treeDataSO.RulesKey[i])){
                rules[treeDataSO.RulesKey[i]] = treeDataSO.RulesValue[i];
            }
            else{
                rules.Add(treeDataSO.RulesKey[i],treeDataSO.RulesValue[i]);
            }
            
        }
        currentString = axiom;
        Generate();
    }
    public void LoadCustom(CustomData customData)
    {
        iteration = customData.iteration;
        angle = customData.angle;
        axiom = customData.axiom;
        Length = customData.length;
        //hasLeaf = customData.hasLeaf;
        
        for(int i = 0; i< customData.RulesKey.Length; i++){
            if(rules.ContainsKey(customData.RulesKey[i])){

                rules[customData.RulesKey[i]] = customData.RulesValue[i];
                //Debug.Log(rules[customData.RulesKey[i]]);
            }
            else{
                rules.Add(customData.RulesKey[i],customData.RulesValue[i]);
                //Debug.Log(customData.RulesKey[i]);
                //Debug.Log(customData.RulesValue[i]);
            }
            
        }
        currentString = axiom;
        Generate();
    }


    public void Generate(){
        //Debug.Log(initialPos);
        transform.position = initialPos;
        transform.rotation = quaternion;
        foreach(Transform item in TreeHolder){
            GameObject.Destroy(item.gameObject);
        }

        for(int j = 0;j<iteration;j++){
        
        string processedString = "";
        char[] characters = currentString.ToCharArray();
        for(int i = 0; i<characters.Length; i++){
            char currentCharacter = characters[i];
            if(rules.ContainsKey(currentCharacter)){
                processedString += rules[currentCharacter];
            }
            else{
                processedString += characters[i];
            }
        }
        currentString = processedString;
        //Debug.Log(currentString);
        
    }
        char[] chars = currentString.ToCharArray();
        for(int i = 0; i<chars.Length; i++){
            char currentCharacter = chars[i];
            if(currentCharacter == 'F'||currentCharacter == 'X'){ //
                Vector3 initialPos = transform.position;
                transform.Translate(Vector3.up*Length);
                var newLine = Instantiate(Line,TreeHolder);
                newLine.GetComponent<LineRenderer>().SetPosition(0,initialPos);
                newLine.GetComponent<LineRenderer>().SetPosition(1,transform.position);
                //Debug.DrawLine(initialPos,transform.position,Color.white,10000f,false);
            }else if(currentCharacter =='+'){
                transform.Rotate(Vector3.forward * angle);
            }else if(currentCharacter =='-'){
                transform.Rotate(Vector3.forward * -angle);
            }else if(currentCharacter =='&'){
                transform.Rotate(Vector3.right * angle);
            }else if(currentCharacter =='^'){
                transform.Rotate(Vector3.right * -angle);
            }else if(currentCharacter == '<' ){
                transform.Rotate(Vector3.up * angle);
            }else if(currentCharacter == '>' ){
                transform.Rotate(Vector3.up * angle);
            }
            else if(currentCharacter == '|' ){
                transform.Rotate(180,0,0);
            }
            else if(currentCharacter =='['){
                TransformInfo info = new TransformInfo();
                info.position = transform.position;
                info.rotation = transform.rotation;
                transformStack.Push(info);
            }else if(currentCharacter ==']'){
                if(hasLeaf){
                    Instantiate(Leaf,transform.position,transform.rotation,TreeHolder);
                }
                TransformInfo info = transformStack.Pop();
                transform.position = info.position;
                transform.rotation = info.rotation;
            }
        }
    }

}
