using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEngine : MonoBehaviour {

    public static SoundEngine Instance;
    private AudioSource audioSrc;
    private AudioClip[] buttonSounds;
    private int randomButtonSound;

    void Start() {
        Instance = this;
        audioSrc = GetComponent<AudioSource>();
        buttonSounds = Resources.LoadAll<AudioClip>("ButtonSounds");
        DontDestroyOnLoad(this.gameObject);
    }

    public void PlaySound() {
        System.Random random = new System.Random();
        randomButtonSound = random.Next(8);
        audioSrc.PlayOneShot(buttonSounds[randomButtonSound]);
    }
}
