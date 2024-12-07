using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundSettings : MonoBehaviour
{
 [SerializeField] AudioMixer  audioMixer;

 [SerializeField] Slider masterVolumeSlider;
 [SerializeField] Slider sfxVolumeSlider;
 [SerializeField] Slider tuneVolumeSlider;


string masterVolume = "MasterVolume";
string sfxVolume = "SFXVolume";
string tuneVolume = "TuneVolume";
void Start(){
    masterVolumeSlider.value = PlayerPrefs.GetFloat(masterVolume, 0f);
     sfxVolumeSlider.value = PlayerPrefs.GetFloat(sfxVolume,0f);
      tuneVolumeSlider.value = PlayerPrefs.GetFloat(tuneVolume,0f);

    SetMasterVolume();
    SetSFXVolume();
    SetTuneVolume();
}


 public void SetMasterVolume(){

    SetVolume(masterVolume, masterVolumeSlider.value);
    PlayerPrefs.SetFloat(masterVolume, masterVolumeSlider.value);

 }

 


 public void SetSFXVolume(){
    SetVolume(sfxVolume, sfxVolumeSlider.value);
    PlayerPrefs.SetFloat(sfxVolume, sfxVolumeSlider.value);
 }


  public void SetTuneVolume(){
    SetVolume(tuneVolume, tuneVolumeSlider.value);
    PlayerPrefs.SetFloat(tuneVolume, tuneVolumeSlider.value);
 }

void SetVolume(string groupName, float value){

    
        float adjustedVolume = Mathf.Log10(value) * 20;
        if(value ==0){
            adjustedVolume = -80;
        }
        audioMixer.SetFloat(groupName, adjustedVolume);
    }
}

