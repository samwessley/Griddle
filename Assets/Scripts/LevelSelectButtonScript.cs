using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectButtonScript : MonoBehaviour {

    public int level;
    private bool isLevelUnlocked = true;

    private void Awake() {
        SetUpButton();
    }

    public void SetUpButton() {

        gameObject.GetComponentInChildren<Text>().text = level.ToString();
            gameObject.GetComponent<Button>().interactable = true;
            gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Level Button");
            gameObject.GetComponentInChildren<Text>().enabled = true;

    }

    public void LoadLevel() {
        GameManager.Instance.currentLevel = level;
        GameManager.Instance.LoadNewScene();
    }
}