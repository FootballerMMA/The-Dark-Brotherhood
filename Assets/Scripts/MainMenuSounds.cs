using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MainMenuSounds : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("Master");
        SetVolume("Master", savedVolume);
        SetVolume("Music", savedVolume);
    }
    // should move this to the sound script
    void SetVolume(string name, float value){
        PlayerPrefs.SetFloat(name, value);
        mixer.SetFloat(name, value);
    }
}
