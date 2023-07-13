using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuAnimationScript : MonoBehaviour {

    [SerializeField] GameObject settingsButton = null;
    [SerializeField] GameObject settingsBackground = null;
    [SerializeField] GameObject settingsPanel = null;
    [SerializeField] GameObject fiveByFivePackButton = null;
    [SerializeField] GameObject sixBySixPackButton = null;
    [SerializeField] GameObject sevenBySevenPackButton = null;
    [SerializeField] GameObject eightByEightPackButton = null;
    [SerializeField] GameObject nineByNinePackButton = null;
    [SerializeField] GameObject removeAdsButton = null;
    [SerializeField] GameObject logo = null;

    float packButtonXPosition = 0;
    float settingsPanelXPosition = 0;
    bool menuOpen = false;

    // Start is called before the first frame update
    void Start() {
        packButtonXPosition = fiveByFivePackButton.transform.position.x;
        settingsPanelXPosition = settingsPanel.transform.position.x;
        LeanTween.moveX(settingsPanel, -6f, 0);
        settingsPanel.SetActive(true);
    }

    public void ToggleSettingsMenu() {
        if (!menuOpen) {
            StartCoroutine(SlidePackButtonsOut());
            StartCoroutine(SlideSettingsPanelIn());
            removeAdsButton.SetActive(false);
            settingsBackground.SetActive(true);
            menuOpen = true;
        } else {
            StartCoroutine(SlideSettingsPanelOut());
            StartCoroutine(SlidePackButtonsIn());
            if (!GameManager.Instance.adsRemoved) {
                removeAdsButton.SetActive(true);
            }
            settingsBackground.SetActive(false);
            menuOpen = false;
        }
    }

    IEnumerator SlidePackButtonsOut() {
        settingsButton.GetComponent<Button>().interactable = false;
        LeanTween.moveX(fiveByFivePackButton, 5f, 0.1f);
        yield return new WaitForSeconds(.04f);
        LeanTween.moveX(sixBySixPackButton, 5f, 0.1f);
        yield return new WaitForSeconds(.04f);
        LeanTween.moveX(sevenBySevenPackButton, 5f, 0.1f);
        yield return new WaitForSeconds(.04f);
        LeanTween.moveX(eightByEightPackButton, 5f, 0.1f);
        yield return new WaitForSeconds(.04f);
        LeanTween.moveX(nineByNinePackButton, 5f, 0.1f);
        yield return null;
    }

    IEnumerator SlidePackButtonsIn() {
        yield return new WaitForSeconds(.1f);
        LeanTween.moveX(fiveByFivePackButton, packButtonXPosition - .15f, 0.08f);
        yield return new WaitForSeconds(.08f);
        LeanTween.moveX(fiveByFivePackButton, packButtonXPosition, 0.03f);
        LeanTween.moveX(sixBySixPackButton, packButtonXPosition - .15f, 0.08f);
        yield return new WaitForSeconds(.08f);
        LeanTween.moveX(sixBySixPackButton, packButtonXPosition, 0.03f);
        LeanTween.moveX(sevenBySevenPackButton, packButtonXPosition - .15f, 0.08f);
        yield return new WaitForSeconds(.08f);
        LeanTween.moveX(sevenBySevenPackButton, packButtonXPosition, 0.03f);
        LeanTween.moveX(eightByEightPackButton, packButtonXPosition - .15f, 0.08f);
        yield return new WaitForSeconds(.08f);
        LeanTween.moveX(eightByEightPackButton, packButtonXPosition, 0.03f);
        LeanTween.moveX(nineByNinePackButton, packButtonXPosition - .15f, 0.08f);
        yield return new WaitForSeconds(.08f);
        LeanTween.moveX(nineByNinePackButton, packButtonXPosition, 0.03f);
        settingsButton.GetComponent<Button>().interactable = true;
        yield return null;
    }

    IEnumerator SlideSettingsPanelOut() {
        settingsButton.GetComponent<Button>().interactable = false;
        LeanTween.moveX(settingsPanel, -6f, 0.12f);
        yield return new WaitForSeconds(.12f);
        settingsPanel.SetActive(false);
    }

    IEnumerator SlideSettingsPanelIn() {
        settingsPanel.SetActive(true);
        yield return new WaitForSeconds(.2f);
        LeanTween.moveX(settingsPanel, settingsPanelXPosition + .25f, 0.12f);
        yield return new WaitForSeconds(.12f);
        LeanTween.moveX(settingsPanel, settingsPanelXPosition, 0.08f);
        settingsButton.GetComponent<Button>().interactable = true;
    }

}
