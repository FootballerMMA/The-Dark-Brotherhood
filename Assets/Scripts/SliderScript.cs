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
        //float volume;
        if (!PlayerPrefs.HasKey("Default Volume Changed")){
            Debug.Log("No key!");
            savedVolume = -80.0f;
            PlayerPrefs.SetInt("Default Volume Changed", 1);
        } else {
            Debug.Log("Key");
            savedVolume = PlayerPrefs.GetFloat("Master");
            float retrievedValue = Mathf.Pow(10, savedVolume / 20);
            //Debug.Log("Receiving this saved " + savedVolume + " turned into " + retrievedValue);
            //slideThing.value = savedVolume;
            slideThing.value = retrievedValue;
            volumeLevel.text = handle.anchorMin.x.ToString();
        }
        SetVolume("Master", savedVolume);
        /*
        if (PlayerPrefs.HasKey("Music")){
            savedVolume = PlayerPrefs.GetFloat("Music");
            Debug.Log("Received value " + savedVolume);
            SetVolume("Music", savedVolume);
        } else {
            Debug.Log("Does not exist music key");
        }
        */
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
        //SetVolume("Music", volumeSetting);
        float adjustedVolume = handle.anchorMin.x;
        volumeLevel.text = adjustedVolume.ToString();
    }

    void SetVolume(string name, float value){
        //Debug.Log("")
        //Debug.Log("Setting mixer to " + value + " from " + code);
        PlayerPrefs.SetFloat(name, value);
        mixer.SetFloat(name, value);
    }

}
