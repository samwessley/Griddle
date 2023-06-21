using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEngine : MonoBehaviour {

    public static SoundEngine Instance;
    public AudioSource audioSrc;
    public bool musicPlaying;

    void Awake() {
        if (Instance != null && Instance != this) { 
            Destroy(this); 
        } else { 
            Instance = this; 
            Instance.musicPlaying = false;
        }
        audioSrc = GetComponent<AudioSource>();
        DontDestroyOnLoad(this.gameObject);
    }

    public void PlayMusic() {
        if (GameManager.Instance.musicOn && !Instance.musicPlaying) {
            AudioClip clip = Resources.Load<AudioClip>("Dominoes");
            audioSrc.clip = clip;
            audioSrc.loop = true;
            audioSrc.Play();
            Instance.musicPlaying = true;
        }
    }

    public void PlayButtonSound() {
        //audioSrc.pitch = 0.8f;
        //audioSrc.volume = 1f;
        AudioClip clip = Resources.Load<AudioClip>("Button");
        audioSrc.PlayOneShot(clip);
    }

    public void PlayPopSound() {
        //audioSrc.volume = 1f;
        //audioSrc.pitch = 1f;
        AudioClip clip = Resources.Load<AudioClip>("Pop");
        audioSrc.PlayOneShot(clip);
    }
    
    public void PlaySuccessSound() {
        //audioSrc.pitch = 1f;
        //audioSrc.volume = 0.3f;
        AudioClip clip = Resources.Load<AudioClip>("Success3");
        audioSrc.PlayOneShot(clip);
    }
}
