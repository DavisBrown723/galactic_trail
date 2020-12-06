using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeValueChange : MonoBehaviour
{


   // reference the audio source component
   private AudioSource audioSource;

   // this is the music volume variable that will be modified
   // by dragging the slider knob
   private float musicVolume = 1f;

   // initialization 
   void Start(){

       // assign audio source component to be able to control it
       audioSource = GetComponent<AudioSource>();
   }// end Start()


   void Update(){
       // setting the volume option of the audio source to be 
       // equal to musicVolume
       audioSource.volume = musicVolume;
   }// end Update()


   // this method is called by slider game object
   // this method takes vol value passed by slider
   // and sets it as musicValue
   public void SetVolume(float vol){
       musicVolume = vol;
   }// end SetVolume(float)
}// end class VolumeValueChange
