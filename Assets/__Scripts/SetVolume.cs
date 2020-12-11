using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class SetVolume : MonoBehaviour
{
   public AudioMixer mixer;
  

  
    public void SetLevel (float sliderValue){
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
    }// end SetLevel(float)

    
    public void SetFXLevel (float sliderValue){
        mixer.SetFloat("SFXVolume", Mathf.Log10(sliderValue) * 20);
    }// end SetFXLevel(float)


}// end class SetVolume
