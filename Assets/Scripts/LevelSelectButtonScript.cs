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
            gameObject.GetComponent<Image>().color = new Color(1,1,1);

            //Set level button images based on which pack is displaying
            if (GameManager.Instance.currentLevelPack == 0 && GameManager.Instance.levelsCompleted_ClassicPack[level - 1] == 1) {
                    gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Level Button Dark Blue");
                    gameObject.GetComponentInChildren<Text>().color = Color.white;
            } else if (GameManager.Instance.currentLevelPack == 1 && GameManager.Instance.levelsCompleted_BonusPack[level - 1] == 1) {
                gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Level Button Light Blue");
                gameObject.GetComponentInChildren<Text>().color = Color.white;
            } else if (GameManager.Instance.currentLevelPack == 2 && GameManager.Instance.levelsCompleted_6x6[level - 1] == 1) {
                gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Level Button Yellow");
                gameObject.GetComponentInChildren<Text>().color = Color.white;
            } else if (GameManager.Instance.currentLevelPack == 3 && GameManager.Instance.levelsCompleted_7x7[level - 1] == 1) {
                gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Level Button Orange");
                gameObject.GetComponentInChildren<Text>().color = Color.white;
            } else if (GameManager.Instance.currentLevelPack == 4 && GameManager.Instance.levelsCompleted_8x8[level - 1] == 1) {
                gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Level Button Red");
                    gameObject.GetComponentInChildren<Text>().color = Color.white;
            } else if (GameManager.Instance.currentLevelPack == 5 && GameManager.Instance.levelsCompleted_9x9[level - 1] == 1) {
                gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Level Button Cranberry");
                gameObject.GetComponentInChildren<Text>().color = Color.white;
            } else {
                gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Level Button Incomplete");
                gameObject.GetComponentInChildren<Text>().color = Color.black;
            }
    }

    public void LoadLevel() {
        GameManager.Instance.currentLevel = level;
        GameManager.Instance.LoadNewScene();
    }
}