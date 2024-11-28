using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TreeDataSO[] treeDataSOs;
    public InputField iteration;
    public InputField angle;
    public InputField axiom;
    public InputField lineLength;
    public Toggle leafToggle;
    
    public RuleSet[] ruleSets;
    public GameObject set;
    public Transform rules;
    public bool onCustomMode;
    public LSystem generator;
    public Dropdown dropdown;
    public EventSO keyEvent;
    public EventSO valueEvent;
    private TreeDataSO currentDataSO;
    private CustomData customData;
    private int currentSOIndex;


    private void OnEnable()
    {
        keyEvent.OnStringSubmit += StoreKeyValue;
        valueEvent.OnStringSubmit += StoreValueValue;
    }

    private void OnDisable()
    {
        keyEvent.OnStringSubmit -= StoreKeyValue;
        valueEvent.OnStringSubmit += StoreValueValue;
    }


    private void StoreKeyValue(string key){
        StoreCustomValue();
    }
    private void StoreValueValue(string value)
    {
        StoreCustomValue();
    }
    private void Start()
    {
        customData = new CustomData();
        GetTreeDataSO(0);
        SendValue();
    }
    public void GetTreeDataSO(int index){
        currentSOIndex = index;
        if(currentSOIndex==treeDataSOs.Length){
            currentSOIndex = 0;
        }
        if(currentSOIndex==6){
            return;
        }
        onCustomMode = false;
        
        currentDataSO = treeDataSOs[currentSOIndex];
        SetValue();
        
        
        //treeDataSOs[index];
    }


    public void SetValue(){
        iteration.text = currentDataSO.iteration.ToString();
        angle.text = currentDataSO.angle.ToString();
        axiom.text = currentDataSO.axiom;
        lineLength.text = currentDataSO.length.ToString();
        //leafToggle.isOn = currentDataSO.hasLeaf;
        ruleSets = new RuleSet[currentDataSO.RulesKey.Length];
        for(int i = 0; i< currentDataSO.RulesKey.Length; i++){
            if(currentDataSO.RulesKey.Length == 1 && ruleSets.Length>1){
                for(int j = 1; j<ruleSets.Length;j++)
                Destroy(ruleSets[j].gameObject);
            }
            ruleSets[i] = Instantiate<GameObject>(set,rules).GetComponent<RuleSet>();
            ruleSets[i].transform.localPosition = transform.localPosition+Vector3.down*i*40;
            ruleSets[i].key.text = currentDataSO.RulesKey[i].ToString();
            ruleSets[i].value.text = currentDataSO.RulesValue[i].ToString();

        }
    }

    public void SendValue(){
        if(!onCustomMode){
            generator.LoadSO(currentDataSO);
        }
        else{
            generator.LoadCustom(customData);
        }
        
    }

    
    public void StoreCustomValue(){
        onCustomMode = true;
        currentDataSO = treeDataSOs[6];
        dropdown.SetValueWithoutNotify(6);
        customData.iteration = int.Parse(iteration.text);
        customData.angle = float.Parse(angle.text);
        customData.axiom = axiom.text;
        customData.length = float.Parse(lineLength.text);
        //customData.hasLeaf = leafToggle.isOn;
        customData.RulesKey = new char[ruleSets.Length];
        customData.RulesValue = new string[ruleSets.Length];
        for(int i = 0; i<ruleSets.Length;i++){
            customData.RulesKey[i] = char.Parse(ruleSets[i].key.text);    
            customData.RulesValue[i] = ruleSets[i].value.text;
        }
            

    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow)){
            onCustomMode = true;
            angle.text = (float.Parse(angle.text) + 2.5f).ToString();
            StoreCustomValue();
            SendValue();
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow)){
            onCustomMode = true;
            angle.text = (float.Parse(angle.text) - 2.5f).ToString();
            StoreCustomValue();
            SendValue();
        }
        if(Input.GetKeyDown(KeyCode.UpArrow)){
            onCustomMode = true;
            iteration.text = (int.Parse(iteration.text) + 1).ToString();
            StoreCustomValue();
            SendValue();
        }
        if(Input.GetKeyDown(KeyCode.DownArrow)){
            onCustomMode = true;
            iteration.text = (int.Parse(iteration.text) - 1).ToString();
            StoreCustomValue();
            SendValue();
        }
        if(Input.GetKeyDown(KeyCode.Tab)){
            dropdown.SetValueWithoutNotify(currentSOIndex+1);
            GetTreeDataSO(currentSOIndex+1);
            SendValue();
        }
    }

}
