using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    public AudioSource UIMusicPlayer;
     public AudioSource GameMusicPlayer;
    [Header("UI")]
    public AudioClip UIMenuMusic;


    [Header("GAME")]
    public AudioClip[] LoadScreenMusic;
    public AudioClip[] LevelMusic;


   
    }// end class SoundController 
