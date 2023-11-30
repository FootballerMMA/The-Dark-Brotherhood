using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SliderScript : MonoBehaviour
{
    [SerializeField] Slider slideThing;
    [SerializeField] Text volumeLevel;
    [SerializeField] AudioMixer mixer;
    [SerializeField] RectTransform handle;
    void Awake(){
        if (!PlayerPrefs.HasKey("secondTime")){
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt("secondTime", 1);
        }
    }
    void Start(){
        float savedVolume;
        if (!PlayerPrefs.HasKey("Default Volume Changed")){
            savedVolume = -80.0f;
            PlayerPrefs.SetInt("Default Volume Changed", 1);
        } else {
            savedVolume = PlayerPrefs.GetFloat("Master");
            float retrievedValue = Mathf.Pow(10, savedVolume / 20);
            slideThing.value = retrievedValue;
            volumeLevel.text = handle.anchorMin.x.ToString();
        }
        SetVolume("Master", savedVolume);
    }
    public void onValueChanged(){
        float sliderVal = slideThing.value;
        float volumeSetting = 0.0f;
        if (sliderVal == 0){
            volumeSetting = -80f;
        } else {
            volumeSetting = Mathf.Log10(slideThing.value) * 20;
        }
        Debug.Log("Setting volume to " + volumeSetting);
        SetVolume("Master", volumeSetting);
        float adjustedVolume = handle.anchorMin.x;
        volumeLevel.text = adjustedVolume.ToString();
    }

    void SetVolume(string name, float value){
        PlayerPrefs.SetFloat(name, value);
        mixer.SetFloat(name, value);
    }

}
