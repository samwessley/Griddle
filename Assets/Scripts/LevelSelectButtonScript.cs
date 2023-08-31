using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectButtonScript : MonoBehaviour {

    public int level;
    private GameObject completed; 
    private GameObject star1;
    private GameObject star2;
    private GameObject star3;
    private GameObject star4;
    private GameObject star5;

    private void Awake() {
        SetUpButton();
        SetCompletedStatus();
        //SetStars();

        if (GameManager.Instance.currentLevel == level) {
            GetComponent<Image>().color = new Color(.95f, .95f, .95f, 1);
            completed.GetComponent<Text>().text = "PLAY";
            completed.GetComponent<Text>().color = Color.black;
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

    private void SetStars() {
        star1 = transform.Find("Star 1").gameObject;
        star2 = transform.Find("Star 2").gameObject;
        star3 = transform.Find("Star 3").gameObject;
        star4 = transform.Find("Star 4").gameObject;
        star5 = transform.Find("Star 5").gameObject;

        //if (GameManager.Instance.five_by_five_stars[GameManager.Instance.])
        star1.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Star 2");
    }

    public void SetUpButton() {

        gameObject.GetComponentInChildren<Text>().text = "#" + level.ToString();
        gameObject.GetComponent<Button>().interactable = true;

        if (GameManager.Instance.currentLevel < level)
        gameObject.GetComponent<Button>().interactable = false;
    }

    public void LoadLevel() {
        GameManager.Instance.currentLevel = level;
        GameManager.Instance.LoadNewScene();
    }
}