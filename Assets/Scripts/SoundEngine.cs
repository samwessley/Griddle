using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEngine : MonoBehaviour {

    public static SoundEngine Instance;
    public AudioSource audioSrc;

    void Awake() {
        if (Instance != null && Instance != this) { 
            Destroy(this); 
        } else { 
            Instance = this; 
            DontDestroyOnLoad(this.gameObject);
            audioSrc = GetComponent<AudioSource>();
        }
    }

    public void PlayButtonSound() {
        //audioSrc.pitch = 0.8f;
        //audioSrc.volume = 1f;
        AudioClip clip = Resources.Load<AudioClip>("button3");
        audioSrc.PlayOneShot(clip);
    }

    public void PlayPopSound() {
        //audioSrc.volume = 1f;
        //audioSrc.pitch = 1f;
        AudioClip clip = Resources.Load<AudioClip>("tile1");
        audioSrc.PlayOneShot(clip);
    }
    
    public void PlaySuccessSound() {
        //audioSrc.pitch = 1f;
        //audioSrc.volume = 0.3f;
        AudioClip clip = Resources.Load<AudioClip>("success1");
        audioSrc.PlayOneShot(clip);
    }
}
