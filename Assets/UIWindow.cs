using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWindow : MonoBehaviour
{
    public void Show(){
        GetComponent<Canvas>().enabled = true;
    }
    public void Close(){
        GetComponent<Canvas>().enabled = false;
    }
}
