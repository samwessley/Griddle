using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialAnimationScript : MonoBehaviour {

    public GameObject highlight = null;
    public GameObject pointer = null;
    public GameObject message = null;
    public GameObject continueButton = null;
    public GameObject levelCompleteMessage = null;
    public GameObject skip = null;
    
    public GameObject redTwoTile = null;

    public bool pointerActive = true;

    private Vector3 pointerStartLocation = new Vector3(0,0,0);
    private Vector3 pointerEndLocation = new Vector3(0,0,0);

    // Start is called before the first frame update
    void Start() {
        pointerStartLocation = pointer.transform.position;

        if (SceneManager.GetActiveScene().buildIndex == 6) {
            pointerEndLocation = new Vector3(0,1.1f,0);
        } else if (SceneManager.GetActiveScene().buildIndex == 7) {
            pointerEndLocation = new Vector3(1.4f,0.5f,0);
        } else if (SceneManager.GetActiveScene().buildIndex == 8) {
            pointerEndLocation = new Vector3(1.4f,1.5f,0);
        } else if (SceneManager.GetActiveScene().buildIndex == 9) {
            pointerEndLocation = new Vector3(1.4f,1.7f,0);
        } else if (SceneManager.GetActiveScene().buildIndex == 10) {
            pointerEndLocation = new Vector3(0.5f,0.8f,0);
        } else if (SceneManager.GetActiveScene().buildIndex == 11) {
            pointerEndLocation = new Vector3(-0.5f,-0.1f,0);
        } else if (SceneManager.GetActiveScene().buildIndex == 12) {
            pointerEndLocation = new Vector3(1f,-0.2f,0);
        }

        StartCoroutine(AnimateHighlight());
        StartCoroutine(AnimatePointer());
    }

    public void AnimateContinueButton() {
        StartCoroutine(AnimateContinue());
    }

    public void TutorialFinishedAnimation() {
        StartCoroutine(TutorialFinished());
    }

    IEnumerator AnimateHighlight() {
        LeanTween.scale(highlight, Vector2.zero,0);
        LeanTween.scale(highlight, new Vector2(1,1), 0.2f).setEase(LeanTweenType.easeOutBack);
        yield return new WaitForSeconds(0);
    }

    IEnumerator AnimatePointer() {
        while (pointerActive) {
            LeanTween.color(pointer.GetComponent<RectTransform>(), new Color(1,1,1,0), 0);
            LeanTween.move(pointer, pointerStartLocation, 0);
            LeanTween.scale(pointer, new Vector2(1.5f,1.5f),0);
            LeanTween.color(pointer.GetComponent<RectTransform>(), new Color(1,1,1,1), 0.2f);
            LeanTween.scale(pointer, new Vector2(1,1), 0.2f).setEase(LeanTweenType.easeOutBack);
            yield return new WaitForSeconds(0.2f);
            LeanTween.move(pointer, pointerEndLocation, 1f);
            yield return new WaitForSeconds(1.5f);
            LeanTween.color(pointer.GetComponent<RectTransform>(), new Color(1,1,1,0), 0.1f);
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator AnimateContinue() {

        highlight.SetActive(false);
        continueButton.SetActive(true);
        if (message != null)
        message.SetActive(true);

        LeanTween.scale(continueButton, new Vector2(0.5f,0.5f),0);
        LeanTween.scale(continueButton, new Vector2(1,1), 0.2f).setEase(LeanTweenType.easeOutBack);

        if (message != null) {
            if (SceneManager.GetActiveScene().buildIndex == 10) {
                LeanTween.color(message.GetComponent<RectTransform>(), new Color(1,1,1,0), 0.2f);
                LeanTween.scale(message, new Vector2(0.5f,0.5f),0.2f).setEase(LeanTweenType.easeOutBack);
            } else {
                LeanTween.color(message.GetComponent<RectTransform>(), new Color(1,1,1,0), 0);
                LeanTween.scale(message, new Vector2(0.5f,0.5f),0);
                LeanTween.color(message.GetComponent<RectTransform>(), new Color(1,1,1,1), 0.2f);
                LeanTween.scale(message, new Vector2(1,1), 0.2f).setEase(LeanTweenType.easeOutBack);
            }
        }

        if (redTwoTile != null) {
            Vector2 redTwoTilePosition = new Vector2(redTwoTile.transform.position.x, redTwoTile.transform.position.y);
            Vector2 redTWoTilePositionNegative = new Vector2(redTwoTilePosition.x - 0.01f, redTwoTilePosition.y);
            Vector2 redTWoTilePositionPositive = new Vector2(redTwoTilePosition.x + 0.01f, redTwoTilePosition.y);

            while (true) {
                LeanTween.move(redTwoTile, redTWoTilePositionNegative, 0.06f).setEase(LeanTweenType.easeInOutBack);
                yield return new WaitForSeconds(0.06f);
                LeanTween.move(redTwoTile, redTWoTilePositionPositive, 0.06f).setEase(LeanTweenType.easeInOutBack);
                yield return new WaitForSeconds(0.06f);
            }
        }
        
        yield return new WaitForSeconds(0);
    }

    IEnumerator TutorialFinished() {
        highlight.SetActive(false);
        skip.SetActive(false);

        levelCompleteMessage.SetActive(true);
        LeanTween.scale(levelCompleteMessage, new Vector2(0.5f,0.5f), 0);
        LeanTween.color(levelCompleteMessage.GetComponent<RectTransform>(), new Color(1,1,1,0), 0);

        LeanTween.scale(levelCompleteMessage, new Vector2(1,1), 0.12f).setEase(LeanTweenType.easeInOutBounce);
        LeanTween.rotateZ(levelCompleteMessage,5f,0.12f);
        LeanTween.color(levelCompleteMessage.GetComponent<RectTransform>(), new Color(1,1,1,1), 0.12f);

        yield return new WaitForSeconds(0.5f);

        continueButton.SetActive(true);
        LeanTween.scale(continueButton, new Vector2(0.5f,0.5f),0);
        LeanTween.color(continueButton.GetComponent<RectTransform>(), new Color(1,1,1,0), 0);
        LeanTween.scale(continueButton, new Vector2(1,1), 0.2f).setEase(LeanTweenType.easeOutBack);
        LeanTween.color(continueButton.GetComponent<RectTransform>(), new Color(1,1,1,1), 0.2f);
    }
}
