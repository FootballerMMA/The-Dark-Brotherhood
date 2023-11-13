using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource entranceMusic;
    [SerializeField] AudioSource cardSelectionSound;
    [SerializeField] AudioSource cardRevealSound;
    [SerializeField] AudioSource gameWinnerSound;
    [SerializeField] AudioSource gameLoserSound;

    [SerializeField] AudioSource fireElementSound;
    [SerializeField] AudioSource snowElementSound;
    [SerializeField] AudioSource waterElementSound;

    [SerializeField] AudioSource elementsCancelSound; // Sound to be played when elements cancel eachother out

    public void PlayEntranceMusic(){ entranceMusic.Play(); }
    public void PlayCardPickedSound(){ cardSelectionSound.Play(); }
    public void PlayCardRevealSound(){ cardRevealSound.Play(); }
    public void PlayGameWinnerSound(){ gameWinnerSound.Play(); }
    public void PlayGameLoserSound(){ gameLoserSound.Play(); }
    public void PlayFireElementSound(){ fireElementSound.Play(); }
    public void PlaySnowElementSound(){ snowElementSound.Play(); }
    public void PlayWaterElementSound(){ waterElementSound.Play(); }
    public void PlayElementsCancelSound(){ elementsCancelSound.Play(); }
    public void PlayElementSound(int element)
    {
        switch(element)
        {
            case 0:
                PlaySnowElementSound();
                break;
            case 1:
                PlayFireElementSound();
                break;
            case 2:
                PlayWaterElementSound();
                break;
        }
    }
}
