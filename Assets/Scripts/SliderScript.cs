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

    void Start(){
        float savedVolume;
        if (PlayerPrefs.GetInt("Default Volume Changed") == 0){
            savedVolume = 0.1f;
            PlayerPrefs.SetInt("Default Volume Changed", 1);
        } else {
            savedVolume = PlayerPrefs.GetFloat("Master");
            float retrievedValue = Mathf.Pow(10, savedVolume / 20);
            slideThing.value = retrievedValue;
            volumeLevel.text = handle.anchorMin.x.ToString();
            SetVolume("Master", savedVolume);
            SetVolume("Music", savedVolume);
        }
    }
    public void onValueChanged(){
        float sliderVal = slideThing.value;
        float volumeSetting = 0.0f;
        if (sliderVal == 0){
            volumeSetting = -80f;
        } else {
            volumeSetting = Mathf.Log10(slideThing.value) * 20;
        }
        SetVolume("Master", volumeSetting);
        SetVolume("Music", volumeSetting);
        //float adjustedVolume = Mathf.Abs((volumeSetting / -80f) - 1f);
        float adjustedVolume = handle.anchorMin.x;
        volumeLevel.text = adjustedVolume.ToString();
    }

    void SetVolume(string name, float value){
        PlayerPrefs.SetFloat(name, value);
        mixer.SetFloat(name, value);
    }

}
