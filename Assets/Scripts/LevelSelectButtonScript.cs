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
            if (GameManager.Instance.levelsCompleted_ClassicPack[level - 1] == 1) {
                if (GameManager.Instance.currentLevelPack == 0) {
                    gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Level Button Dark Blue");
                } else if (GameManager.Instance.currentLevelPack == 1) {
                    gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Level Button Light Blue");
                } else if (GameManager.Instance.currentLevelPack == 2) {
                    gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Level Button Yellow");
                } else if (GameManager.Instance.currentLevelPack == 3) {
                    gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Level Button Orange");
                } else if (GameManager.Instance.currentLevelPack == 4) {
                    gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Level Button Red");
                } else if (GameManager.Instance.currentLevelPack == 2) {
                    gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Level Button Yellow");
                }
            }

            if (GameManager.Instance.currentLevelPack == 0) {
                if (GameManager.Instance.levelsCompleted_ClassicPack[level - 1] == 1) {
                    gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Level Button Dark Blue");
                } else {
                    gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Level Button Incomplete Dark Blue");
                }
            } else if (GameManager.Instance.currentLevelPack == 1) {
                if (GameManager.Instance.levelsCompleted_BonusPack[level - 1] == 1) {
                    gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Level Button Light Blue");
                } else {
                    gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Level Button Incomplete Light Blue");
                }
            } else if (GameManager.Instance.currentLevelPack == 2) {
                if (GameManager.Instance.levelsCompleted_6x6[level - 1] == 1) {
                    gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Level Button Yellow");
                } else {
                    gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Level Button Incomplete Yellow");
                }
            } else if (GameManager.Instance.currentLevelPack == 3) {
                if (GameManager.Instance.levelsCompleted_7x7[level - 1] == 1) {
                    gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Level Button Orange");
                } else {
                    gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Level Button Incomplete Orange");
                }
            } else if (GameManager.Instance.currentLevelPack == 4) {
                if (GameManager.Instance.levelsCompleted_8x8[level - 1] == 1) {
                    gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Level Button Red");
                } else {
                    gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Level Button Incomplete Red");
                }
            } else {
                if (GameManager.Instance.levelsCompleted_9x9[level - 1] == 1) {
                    gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Level Button Cranberry");
                } else {
                    gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Level Button Incomplete Cranberry");
                }
            }
            
            /*if (GameManager.Instance.currentLevelPack == 0 && GameManager.Instance.levelsCompleted_ClassicPack[level - 1] == 1) {
                gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Level Button Dark Blue");
            } else if (GameManager.Instance.currentLevelPack == 1 && GameManager.Instance.levelsCompleted_BonusPack[level - 1] == 1) {
                gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Level Button Light Blue");
            } else if (GameManager.Instance.currentLevelPack == 2 && GameManager.Instance.levelsCompleted_6x6[level - 1] == 1) {
                gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Level Button Yellow");
            } else if (GameManager.Instance.currentLevelPack == 3 && GameManager.Instance.levelsCompleted_7x7[level - 1] == 1) {
                gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Level Button Orange");
            } else if (GameManager.Instance.currentLevelPack == 4 && GameManager.Instance.levelsCompleted_8x8[level - 1] == 1) {
                gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Level Button Red");
            } else if (GameManager.Instance.currentLevelPack == 5 && GameManager.Instance.levelsCompleted_9x9[level - 1] == 1) {
                gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Level Button Cranberry");
            } else {
                gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/White 1x1");
                gameObject.GetComponent<Image>().color = new Color(.1922f,.2667f,.3176f);
            }*/
    }

    public void LoadLevel() {
        GameManager.Instance.currentLevel = level;
        GameManager.Instance.LoadNewScene();
    }
}