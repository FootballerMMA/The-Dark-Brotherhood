using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionOptions : MonoBehaviour
{
    [SerializeField] Dropdown resolutionDropdown;
    [SerializeField] Toggle fullscreenToggle;
    bool fullscreen;
    bool initializing;

    Resolution[] resolutions;
    void Start()
    {
        bool setdefault;
        initializing = true;
        if (PlayerPrefs.HasKey("Fullscreen")){
            int val = PlayerPrefs.GetInt("Fullscreen");
            if (val == 0){
                fullscreen = false;
                fullscreenToggle.isOn = false;
            } else {
                fullscreen = true;
                fullscreenToggle.isOn = true;
            }
        } else {
            fullscreen = true;
            fullscreenToggle.isOn = true;
        }





        resolutions = Screen.resolutions;
        int defResIdx = 0;
        for (int i = 0; i < resolutions.Length; i++){
            string resolutionString = resolutions[i].width.ToString() + " x " + resolutions[i].height.ToString();
            resolutionDropdown.options.Add(new Dropdown.OptionData(resolutionString));
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height){
                defResIdx = i;
            }
        }
        if (PlayerPrefs.HasKey("resolution")){
            setdefault = false;
        } else {
            setdefault = true;
        }
        if (!setdefault){
            resolutionDropdown.value = PlayerPrefs.GetInt("resolution");
        } else {
            resolutionDropdown.value = defResIdx;
        }
        initializing = false;
    }

    public void ChangeResolution(){
        Screen.SetResolution(resolutions[resolutionDropdown.value].width, resolutions[resolutionDropdown.value].height, fullscreen);
        PlayerPrefs.SetInt("resolution", resolutionDropdown.value);
    }
    public void Show(){
        GetComponent<Canvas>().enabled = true;
    }
    public void Close(){
        GetComponent<Canvas>().enabled = false;
    }
    public void togglefullscreen(){
        if (initializing) // Skip function execution during initialization
        {
            return;
        }
        fullscreen = !fullscreen;
        if (!fullscreen){
            PlayerPrefs.SetInt("Fullscreen", 0);
        } else {
            PlayerPrefs.SetInt("Fullscreen", 1);
        }
        ChangeResolution();
    }
}
