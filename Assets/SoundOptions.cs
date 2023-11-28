using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundOptions : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;

    [SerializeField] Text volumeLevelMaster;
    [SerializeField] Text volumeLevelMusic;
    [SerializeField] Text volumeLevelSFX;

    [SerializeField] RectTransform handleMaster;
    [SerializeField] RectTransform handleMusic;
    [SerializeField] RectTransform handleSFX;

    private const string masterVolumeKey = "Master";
    private const string musicVolumeKey = "MusicVK";
    private const string sfxVolumeKey = "SFX";
    private const string defaultVolumesChangedKey = "DefaultVolumesChanged";

    private void Start()
    {
        LoadVolumes();
    }

    private void LoadVolumes()
    {
        if (!PlayerPrefs.HasKey(defaultVolumesChangedKey))
        {
            LoadDefaultVolumes();
        }
        else
        {
            LoadCustomVolumes();
        }
    }

    private void LoadDefaultVolumes()
    {
        float defaultVolume = PlayerPrefs.GetFloat(masterVolumeKey);
        float volumeConvertedToSlider = ConvertToSlider(defaultVolume);

        masterSlider.value = volumeConvertedToSlider;
        sfxSlider.value = volumeConvertedToSlider;
        musicSlider.value = volumeConvertedToSlider;

        PlayerPrefs.SetInt(defaultVolumesChangedKey, 1);
    }

    private void LoadCustomVolumes()
    {
        float volumeConvertedToSlider = ConvertToSlider(PlayerPrefs.GetFloat(masterVolumeKey));
        masterSlider.value = volumeConvertedToSlider;
        volumeLevelMaster.text = ConvertFloatToString(volumeConvertedToSlider);

        volumeConvertedToSlider = ConvertToSlider(PlayerPrefs.GetFloat(musicVolumeKey));
        musicSlider.value = volumeConvertedToSlider;
        volumeLevelMusic.text = ConvertFloatToString(volumeConvertedToSlider);

        volumeConvertedToSlider = ConvertToSlider(PlayerPrefs.GetFloat(sfxVolumeKey));
        sfxSlider.value = volumeConvertedToSlider;
        volumeLevelSFX.text = ConvertFloatToString(volumeConvertedToSlider);
    }

    private void SetVolume(string name, Slider slider)
    {
        float volumeSetting = 0.0f;
        if (slider.value == 0)
        {
            volumeSetting = -80f;
        }
        else
        {
            volumeSetting = Mathf.Log10(slider.value) * 20;
        }
        PlayerPrefs.SetFloat(name, volumeSetting);
        if (name == musicVolumeKey){
            name = "Music";
        }
        mixer.SetFloat(name, volumeSetting);
    }

    public void SetMasterVolume()
    {
        SetVolume(masterVolumeKey, masterSlider);
        float adjustedVolume = handleMaster.anchorMin.x;
        volumeLevelMaster.text = ConvertFloatToString(adjustedVolume);
    }

    public void SetMusicVolume()
    {
        SetVolume(musicVolumeKey, musicSlider);
        float adjustedVolume = handleMusic.anchorMin.x;
        volumeLevelMusic.text = ConvertFloatToString(adjustedVolume);
    }

    public void SetSFXVolume()
    {
        SetVolume(sfxVolumeKey, sfxSlider);
        float adjustedVolume = handleSFX.anchorMin.x;
        volumeLevelSFX.text = ConvertFloatToString(adjustedVolume);
    }

    private float ConvertToSlider(float savedValue)
    {
        return Mathf.Pow(10, savedValue / 20);
    }

    private string ConvertFloatToString(float value)
    {
        return value.ToString();
    }
}

/*
public class SoundOptions : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;

    [SerializeField] Text volumeLevelMaster;
    [SerializeField] Text volumeLevelMusic;
    [SerializeField] Text volumeLevelSFX;

    [SerializeField] RectTransform handleMaster;
    [SerializeField] RectTransform handleMusic;
    [SerializeField] RectTransform handleSFX;
    

    void Start(){
        Debug.Log("Saved in 2nd is " + PlayerPrefs.GetFloat("Music"));
        float volumeConvertedToSlider;
        //PlayerPrefs.DeleteAll();
        if(!PlayerPrefs.HasKey("Default Volumes Changed")){
            print("default being loaded");
            float savedVolume;

            if (!PlayerPrefs.HasKey("Master")){
                savedVolume = -80.0f;
            } else {
                savedVolume = PlayerPrefs.GetFloat("Master");
            }

            volumeConvertedToSlider = convertToSlider(savedVolume);

            masterSlider.value = volumeConvertedToSlider;
            sfxSlider.value = volumeConvertedToSlider;
            musicSlider.value = volumeConvertedToSlider;

            PlayerPrefs.SetInt("Default Volumes Changed",1);
        }else{
            print("custom being loaded!");
            volumeConvertedToSlider = convertToSlider(PlayerPrefs.GetFloat("Master"));
            masterSlider.value = volumeConvertedToSlider;
            volumeLevelMaster.text = convertFloatToString(volumeConvertedToSlider);

            volumeConvertedToSlider = convertToSlider(PlayerPrefs.GetFloat("Music"));
            Debug.Log("Saved in 2nd is " + PlayerPrefs.GetFloat("Music"));
            musicSlider.value = volumeConvertedToSlider;
            volumeLevelMusic.text = convertFloatToString(volumeConvertedToSlider);

            //Debug.Log("musicSlider savedPref as " + PlayerPrefs.GetFloat("Music"));
            volumeConvertedToSlider = convertToSlider(PlayerPrefs.GetFloat("SFX"));
            sfxSlider.value = volumeConvertedToSlider;
            volumeLevelSFX.text = convertFloatToString(volumeConvertedToSlider);
        }
        // if not works then call setvolume here

    }

    void SetVolume(string name,Slider slider){
        //PlayerPrefs.SetFloat(name, slider.value);
        //Debug.Log("Saving " + name + " as " + slider.value);
        float volumeSetting = 0.0f;
        if(slider.value == 0){
            volumeSetting = -80f;
        } else {
            volumeSetting = Mathf.Log10(slider.value) * 20;
        }
        PlayerPrefs.SetFloat(name, volumeSetting);
        Debug.Log("Saving " + name + " w/ val " + volumeSetting);
        mixer.SetFloat(name,volumeSetting);
    }

     public void SetMasterVolume(){
        SetVolume("Master",masterSlider);
        float adjustedVolume = handleMaster.anchorMin.x;
        volumeLevelMaster.text = convertFloatToString(adjustedVolume);
    }
    public void SetMusicVolume(){
        SetVolume("Music",musicSlider);
        float adjustedVolume = handleMusic.anchorMin.x;
        volumeLevelMusic.text = convertFloatToString(adjustedVolume);
    }
    public void SetSFXVolume(){
        SetVolume("SFX",sfxSlider);
        float adjustedVolume = handleSFX.anchorMin.x;
        volumeLevelSFX.text = convertFloatToString(adjustedVolume);
    }
    float convertToSlider(float savedValue){
        // this changes the slider not the mixer
        return Mathf.Pow(10, savedValue / 20);
    }
    string convertFloatToString(float value){
        return value.ToString();
    }
} */
