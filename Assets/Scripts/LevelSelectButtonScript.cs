using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectButtonScript : MonoBehaviour {

    private int level;
    private bool isLevelUnlocked = false;

    private void Awake() {
        level = System.Convert.ToInt32(gameObject.GetComponentInChildren<Text>().text);

        gameObject.GetComponentInChildren<Text>().enabled = false;
        gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Lock");

        if (GameManager.Instance.levelsUnlocked >= level)
        isLevelUnlocked = true;

        SetUpButton();
    }

    private void SetUpButton() {
        if (isLevelUnlocked) {
            int buttonColor = GameManager.Instance.levelButtonColors[level - 1];

            gameObject.GetComponent<Button>().interactable = true;
            gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Level Button " + buttonColor);
            gameObject.GetComponentInChildren<Text>().enabled = true;
        }
    }

    public void LoadLevel() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        GameManager.Instance.currentLevel = level;
    }
}
