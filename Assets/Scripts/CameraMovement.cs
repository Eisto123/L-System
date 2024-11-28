using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public float moveAmount;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W)){
            transform.position = new Vector3(transform.position.x,transform.position.y-moveAmount,transform.position.z);
        }
        else if(Input.GetKeyDown(KeyCode.S)){
            transform.position = new Vector3(transform.position.x,transform.position.y+moveAmount,transform.position.z);
        }
        else if(Input.GetKeyDown(KeyCode.A)){
            transform.position = new Vector3(transform.position.x + moveAmount,transform.position.y,transform.position.z);
        }else if(Input.GetKeyDown(KeyCode.D)){
            transform.position = new Vector3(transform.position.x - moveAmount,transform.position.y,transform.position.z);
        }
    }
}
