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

    [SerializeField] GameObject restorePurchasesSuccessModal = null;
    [SerializeField] GameObject restorePurchasesFailedModal = null;

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

    public void ToggleResetProgressModal() {
        settingsPanel.SetActive(false);
        resetProgressModal.SetActive(true);
    }

    public void CancelResetProgress() {
        //settingsPanel.SetActive(true);
        resetProgressModal.SetActive(false);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void CloseRestorePurchasesSuccessModal() {
        settingsPanel.SetActive(false);
        restorePurchasesSuccessModal.SetActive(false);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void CloseRestorePurchasesFailedModal() {
        settingsPanel.SetActive(false);
        restorePurchasesFailedModal.SetActive(false);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
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