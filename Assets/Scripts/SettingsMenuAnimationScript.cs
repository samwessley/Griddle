using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuAnimationScript : MonoBehaviour {

    [SerializeField] GameObject settingsButton = null;
    [SerializeField] GameObject settingsPanel = null;
    [SerializeField] GameObject removeAdsButton = null;
    
    float settingsPanelXPosition = 0;
    bool menuOpen = false;

    // Start is called before the first frame update
    void Start() {
        settingsPanelXPosition = settingsPanel.transform.position.x;
        LeanTween.moveX(settingsPanel, -9f, 0);
        settingsPanel.SetActive(true);
    }

    public void OpenSettingsMenu() {
        if (!menuOpen) {
            settingsButton.GetComponent<Button>().interactable = false;
            StartCoroutine(SlideSettingsPanelIn());
            removeAdsButton.SetActive(false);
            menuOpen = true;
        }
    }

    public void CloseSettingsMenu() {
        if (menuOpen) {
            StartCoroutine(SlideSettingsPanelOut());
            settingsButton.GetComponent<Button>().interactable = true;
            if (!GameManager.Instance.adsRemoved) {
                removeAdsButton.SetActive(true);
            }
            menuOpen = false;
        }
    }

    IEnumerator SlideSettingsPanelOut() {
        settingsButton.GetComponent<Button>().interactable = false;
        LeanTween.moveX(settingsPanel, -9f, 0.12f);
        yield return new WaitForSeconds(.12f);
        settingsPanel.SetActive(false);
    }

    IEnumerator SlideSettingsPanelIn() {
        settingsPanel.SetActive(true);
        LeanTween.moveX(settingsPanel, settingsPanelXPosition, 0.12f);
        settingsButton.GetComponent<Button>().interactable = true;
        yield return new WaitForSeconds(0);
    }

}
