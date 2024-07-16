using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audios : MonoBehaviour
{

    public AudioSource MusicaSource;
    public AudioSource SFXSource;
    public AudioClip sfxTiro;
    public AudioClip sfxMoeda;
    public AudioClip sfxMunicao;
    public AudioClip sfxVida;
    public AudioClip sfxGameOver;
    public AudioClip sfxWin;
   

    public void SFXManager(AudioClip sfxClip, float volume)
    {
        SFXSource.PlayOneShot(sfxClip, volume);
    }
}
