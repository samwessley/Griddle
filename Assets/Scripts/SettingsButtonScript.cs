using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsButtonScript : MonoBehaviour {

    [SerializeField] GameObject restorePurchases = null;
    [SerializeField] GameObject resetProgress = null;
    [SerializeField] GameObject hapticsToggle = null;
    [SerializeField] GameObject soundsToggle = null;
    [SerializeField] GameObject musicToggle = null;
    [SerializeField] GameObject settingsButton = null;

    [SerializeField] GameObject resetProgressModal = null;
    [SerializeField] GameObject settingsPanel = null;

    /*public void CloseSettingsScene() {
        StartCoroutine(CloseSettingsScreenAnimation());
    }

    IEnumerator CloseSettingsScreenAnimation() {
        LeanTween.moveX(restorePurchases, -4f,0.08f);
        yield return new WaitForSeconds(0.05f);

        LeanTween.moveX(resetProgress, -4f,0.08f);    
        yield return new WaitForSeconds(0.05f); 

        LeanTween.moveX(hapticsToggle, -4f,0.08f);    
        yield return new WaitForSeconds(0.05f);

        LeanTween.moveX(soundsToggle, -4f,0.08f);    
        yield return new WaitForSeconds(0.05f);

        LeanTween.rotateZ(settingsButton, 0,0.1f);
        yield return new WaitForSeconds(0.1f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }*/

    public void ToggleSounds() {
        if (GameManager.Instance.soundsOn) {
            soundsToggle.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Toggle Off");
        } else {
            soundsToggle.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Toggle On");
        }

        GameManager.Instance.soundsOn = !GameManager.Instance.soundsOn;
    }

    public void ToggleHaptics() {
        if (GameManager.Instance.hapticsOn) {
            hapticsToggle.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Toggle Off");
        } else {
            hapticsToggle.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Toggle On");
        }

        GameManager.Instance.hapticsOn = !GameManager.Instance.hapticsOn;
    }

    public void ToggleMusic() {
        if (SoundEngine.Instance.musicPlaying) {
            //SoundEngine.Instance.audioSrc.Pause();
            SoundEngine.Instance.audioSrc.clip = null;
            SoundEngine.Instance.musicPlaying = false;
            GameManager.Instance.musicOn = false;

            musicToggle.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Toggle Off");
        } else {
            AudioClip clip = Resources.Load<AudioClip>("Dominoes");
            SoundEngine.Instance.audioSrc.clip = clip;
            SoundEngine.Instance.audioSrc.loop = true;
            SoundEngine.Instance.audioSrc.Play();
            SoundEngine.Instance.musicPlaying = true;
            GameManager.Instance.musicOn = true;

            musicToggle.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Toggle On");
        }

        GameManager.Instance.SaveAsJSON();
    }

    public void ToggleResetProgressModal() {
        settingsPanel.SetActive(false);
        resetProgressModal.SetActive(true);
    }

    public void CancelResetProgress() {
        settingsPanel.SetActive(true);
        resetProgressModal.SetActive(false);
    }

    public void ResetProgress() {
        GameManager.Instance.currentLevel = 1;
        GameManager.Instance.levelsCompleted_5x5 = 0;
        GameManager.Instance.levelsCompleted_6x6 = 0;
        GameManager.Instance.levelsCompleted_7x7 = 0;
        GameManager.Instance.levelsCompleted_8x8 = 0;
        GameManager.Instance.levelsCompleted_9x9 = 0;

        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}