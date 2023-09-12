using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AboutMenuAnimationScript : MonoBehaviour {

    [SerializeField] GameObject aboutButton = null;
    [SerializeField] GameObject aboutPanel = null;
    [SerializeField] GameObject removeAdsButton = null;
    
    float aboutPanelXPosition = 0;
    bool menuOpen = false;

    // Start is called before the first frame update
    void Start() {
        aboutPanelXPosition = aboutPanel.transform.position.x;
        LeanTween.moveX(aboutPanel, -9f, 0);
        aboutPanel.SetActive(true);
    }

    public void OpenAboutMenu() {
        if (!menuOpen) {
            aboutButton.GetComponent<Button>().interactable = false;
            StartCoroutine(SlideAboutPanelIn());
            removeAdsButton.SetActive(false);
            menuOpen = true;
        }
    }

    public void CloseAboutMenu() {
        if (menuOpen) {
            StartCoroutine(SlideAboutPanelOut());
            aboutButton.GetComponent<Button>().interactable = true;
            if (!GameManager.Instance.adsRemoved) {
                removeAdsButton.SetActive(true);
            }
            menuOpen = false;
        }
    }

    IEnumerator SlideAboutPanelOut() {
        aboutButton.GetComponent<Button>().interactable = false;
        LeanTween.moveX(aboutPanel, -9f, 0.2f).setEase(LeanTweenType.easeInOutBounce);
        yield return new WaitForSeconds(.2f);
        aboutPanel.SetActive(false);
    }

    IEnumerator SlideAboutPanelIn() {
        aboutPanel.SetActive(true);
        LeanTween.moveX(aboutPanel, aboutPanelXPosition, 0.2f).setEase(LeanTweenType.easeInOutBounce);
        aboutButton.GetComponent<Button>().interactable = true;
        yield return new WaitForSeconds(0);
    }

}
