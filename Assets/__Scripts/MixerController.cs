using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class MixerController : MonoBehaviour
{
   public AudioMixer audioMixer;
   
   public void SetMusicVolume(float volume){
       audioMixer.SetFloat("MusicVolume", volume);

   }// end SetMusicVolume(float)

   
   public void SetFXVolume(float volume){
       audioMixer.SetFloat("SoundFXVolume", volume);
       
   }// end SetMusicVolume(float)

}// end Class MixerController
