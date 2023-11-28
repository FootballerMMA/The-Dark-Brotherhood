using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VsyncOption : MonoBehaviour
{
    public bool vsyncEnabled = true;
    void Start()
    {
        if (PlayerPrefs.HasKey("VSync")){
            int val = PlayerPrefs.GetInt("VSync");
            if (val == 0){
                vsyncEnabled = false;
            }
        }
        QualitySettings.vSyncCount = vsyncEnabled ? 1 : 0;
    }

    public void toggleVsync(){
        if (vsyncEnabled){
            //vsync was enabled now disable it
            //Debug.Log("Disabled vsync");
            PlayerPrefs.SetInt("Vsync", 0);
        } else {
            PlayerPrefs.SetInt("Vsync", 1);
            //Debug.Log("enabled vsync");
        }
        vsyncEnabled = !vsyncEnabled;
        QualitySettings.vSyncCount = vsyncEnabled ? 1 : 0;
        
    }
}
