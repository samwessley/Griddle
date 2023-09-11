using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectButtonScript : MonoBehaviour {

    public int level;
    private GameObject levelNumber;
    private GameObject completed; 

    private void Start() {
        SetUpButton();
        SetCompletedStatus();

        if (GameManager.Instance.currentLevel == level) {
            //GetComponent<Image>().color = new Color(.95f, .95f, .95f, 1);
            completed.GetComponent<Text>().text = "PLAY";
            completed.GetComponent<Text>().color = new Color(0, .22f, .267f, 1);
            completed.GetComponent<Text>().fontStyle = FontStyle.Bold;
            levelNumber.GetComponent<Text>().fontStyle = FontStyle.Bold;
        }
    }

    private void SetCompletedStatus() {
        completed = transform.Find("Completed").gameObject;
        if (GameManager.Instance.currentLevel > level) {
            completed.GetComponent<Text>().text = "COMPLETED";
        } else {
            completed.GetComponent<Text>().text = "INCOMPLETE";
            completed.GetComponent<Text>().color = Color.gray;
        }
    }

    public void SetUpButton() {
        levelNumber = transform.Find("Label").gameObject;
        gameObject.GetComponentInChildren<Text>().text = level.ToString();
        gameObject.GetComponent<Button>().interactable = true;

        if (GameManager.Instance.currentLevel < level)
        gameObject.GetComponent<Button>().interactable = false;
    }

    public void LoadLevel() {
        GameManager.Instance.currentLevel = level;
        GameManager.Instance.LoadNewScene();
    }
}