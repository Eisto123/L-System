using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeRotation : MonoBehaviour
{
    //value from scroll bar, 0.0f - 1.0f
    public void RotateTree(float value){
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, value*-360, transform.eulerAngles.z);
    }
}
