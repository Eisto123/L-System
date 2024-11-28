using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Event", menuName = "ScriptableObjects/EventSO", order = 1)]
public class EventSO : ScriptableObject
{
    public UnityAction<string> OnStringSubmit;

    public void SubmitString(string input){
        OnStringSubmit?.Invoke(input);
    }
}
