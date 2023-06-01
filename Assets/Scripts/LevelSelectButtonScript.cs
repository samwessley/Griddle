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
        if (GameManager.Instance.currentLevelPack == 0 && GameManager.Instance.levelsCompleted_5x5 >= level) {
            gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Level Button Complete");
        } else if (GameManager.Instance.currentLevelPack == 1 && GameManager.Instance.levelsCompleted_6x6 >= level) {
            gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Level Button Complete");
        } else if (GameManager.Instance.currentLevelPack == 2 && GameManager.Instance.levelsCompleted_7x7 >= level) { 
            gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Level Button Complete");
        } else if (GameManager.Instance.currentLevelPack == 3 && GameManager.Instance.levelsCompleted_8x8 >= level) { 
            gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Level Button Complete");
        } else if (GameManager.Instance.currentLevelPack == 4 && GameManager.Instance.levelsCompleted_9x9 >= level) {   
            gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Level Button Complete");
        } else {

            if (GameManager.Instance.currentLevelPack == 0 && GameManager.Instance.levelsCompleted_5x5 == level - 1) {  
                gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Level Button Light Blue");
                transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            } else if (GameManager.Instance.currentLevelPack == 1 && GameManager.Instance.levelsCompleted_6x6 == level - 1) {  
                gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Level Button Yellow");
                transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            } else if (GameManager.Instance.currentLevelPack == 2 && GameManager.Instance.levelsCompleted_7x7 == level - 1) { 
                gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Level Button Orange");
                transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            } else if (GameManager.Instance.currentLevelPack == 3 && GameManager.Instance.levelsCompleted_8x8 == level - 1) {
                gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Level Button Red");
                transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            } else if (GameManager.Instance.currentLevelPack == 4 && GameManager.Instance.levelsCompleted_9x9 == level - 1) { 
                gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Level Button Cranberry");
                transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            } else {
                gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Level Button Locked");
                gameObject.GetComponentInChildren<Text>().color = new Color(.682f, .682f, .682f, 1);
                gameObject.GetComponent<Button>().interactable = false;
            }
        }
    }

    public void LoadLevel() {
        GameManager.Instance.currentLevel = level;
        GameManager.Instance.LoadNewScene();
    }
}