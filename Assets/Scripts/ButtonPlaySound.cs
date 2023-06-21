using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPlaySound : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        gameObject.GetComponent<Button>().onClick.AddListener(ButtonPlay);
    }

    private void ButtonPlay() {
        if (GameManager.Instance.soundsOn)
        SoundEngine.Instance.PlayButtonSound();
    }
}
