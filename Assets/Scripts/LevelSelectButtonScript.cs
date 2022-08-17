using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectButtonScript : MonoBehaviour {

    public int level;

    private void Awake() {
        SetUpButton();
    }

    public void SetUpButton() {

        gameObject.GetComponentInChildren<Text>().text = level.ToString();
            gameObject.GetComponent<Button>().interactable = true;
            gameObject.GetComponentInChildren<Text>().enabled = true;

            if (GameManager.Instance.levelsCompleted[level - 1] == 1) {
                gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Level Button Complete");
            } else {
                gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Level Button");
            }
    }

    public void LoadLevel() {
        GameManager.Instance.currentLevel = level;
        GameManager.Instance.LoadNewScene();
    }
}