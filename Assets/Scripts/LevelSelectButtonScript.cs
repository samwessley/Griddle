using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectButtonScript : MonoBehaviour {

    [SerializeField] int level;
    private bool isLevelCompleted = false;
    private bool isNextLevel = false;

    public void LoadLevel() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        GameManager.GM.currentLevel = level;
    }
}
