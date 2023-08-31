using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoreGameModesAnimationScript : MonoBehaviour {
    
    [SerializeField] GameObject moreGameModesPanel = null;
    [SerializeField] GameObject background = null;
    
    float moreGameModesPanelYPosition = 0;
    bool menuOpen = false;

    void Start() {
        moreGameModesPanelYPosition = moreGameModesPanel.transform.position.y;
        LeanTween.moveY(moreGameModesPanel, -10f, 0);
        moreGameModesPanel.SetActive(true);
    }

    public void OpenMoreGameModesMenu() {
        if (!menuOpen) {
            StartCoroutine(SlideMoreGameModesPanelIn());
            menuOpen = true;
        }
    }

    public void CloseMoreGameModesMenu() {
        if (menuOpen) {
            StartCoroutine(SlideMoreGameModesPanelOut());
            menuOpen = false;
        }
    }

    IEnumerator SlideMoreGameModesPanelOut() {
        LeanTween.moveY(moreGameModesPanel, -10f, 0.12f);
        yield return new WaitForSeconds(.12f);
        moreGameModesPanel.SetActive(false);
        background.SetActive(false);
    }

    IEnumerator SlideMoreGameModesPanelIn() {
        background.SetActive(true);
        moreGameModesPanel.SetActive(true);
        LeanTween.moveY(moreGameModesPanel, moreGameModesPanelYPosition + .25f, 0.12f);
        yield return new WaitForSeconds(.12f);
        LeanTween.moveY(moreGameModesPanel, moreGameModesPanelYPosition, 0.08f);
    }
}
