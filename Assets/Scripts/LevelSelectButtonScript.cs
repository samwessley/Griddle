using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectButtonScript : MonoBehaviour {

    private int level;
    //private bool isLevelCompleted = false;
    //private bool isNextLevel = false;

    private void Awake() {
        level = System.Convert.ToInt32(gameObject.GetComponentInChildren<Text>().text);
    }

    public void LoadLevel() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        GameManager.Instance.currentLevel = level;
    }
}
