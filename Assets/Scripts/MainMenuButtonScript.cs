using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtonScript : MonoBehaviour {

    public void Load5x5PackPlayScene() {
        GameManager.Instance.currentLevelPack = 0;
        if (GameManager.Instance.levelsCompleted_5x5 < GameManager.Instance.totalLevels) {
            GameManager.Instance.currentLevel = GameManager.Instance.levelsCompleted_5x5 + 1; 
        } else {
            GameManager.Instance.currentLevel = GameManager.Instance.levelsCompleted_5x5;
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene(5);
    }

    public void Load5x5PackLevelSelectScene() {
        GameManager.Instance.currentLevelPack = 0;
        if (GameManager.Instance.levelsCompleted_5x5 < GameManager.Instance.totalLevels) {
            GameManager.Instance.currentLevel = GameManager.Instance.levelsCompleted_5x5 + 1; 
        } else {
            GameManager.Instance.currentLevel = GameManager.Instance.levelsCompleted_5x5;
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void Load6x6PackPlayScene() {
        GameManager.Instance.currentLevelPack = 1;

        if (GameManager.Instance.levelsCompleted_6x6 < GameManager.Instance.totalLevels) {
            GameManager.Instance.currentLevel = GameManager.Instance.levelsCompleted_6x6 + 1; 
        } else {
            GameManager.Instance.currentLevel = GameManager.Instance.levelsCompleted_6x6;
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene(4);
    }

    public void Load6x6PackLevelSelectScene() {
        GameManager.Instance.currentLevelPack = 1;
        if (GameManager.Instance.levelsCompleted_6x6 < GameManager.Instance.totalLevels) {
            GameManager.Instance.currentLevel = GameManager.Instance.levelsCompleted_6x6 + 1; 
        } else {
            GameManager.Instance.currentLevel = GameManager.Instance.levelsCompleted_6x6;
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void Load7x7PackPlayScene() {
        GameManager.Instance.currentLevelPack = 2;
        if (GameManager.Instance.levelsCompleted_7x7 < GameManager.Instance.totalLevels) {
            GameManager.Instance.currentLevel = GameManager.Instance.levelsCompleted_7x7 + 1; 
        } else {
            GameManager.Instance.currentLevel = GameManager.Instance.levelsCompleted_7x7;
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene(3);
    }

    public void Load7x7PackLevelSelectScene() {
        GameManager.Instance.currentLevelPack = 2;
        if (GameManager.Instance.levelsCompleted_7x7 < GameManager.Instance.totalLevels) {
            GameManager.Instance.currentLevel = GameManager.Instance.levelsCompleted_7x7 + 1; 
        } else {
            GameManager.Instance.currentLevel = GameManager.Instance.levelsCompleted_7x7;
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void Load8x8PackPlayScene() {
        GameManager.Instance.currentLevelPack = 3;
        if (GameManager.Instance.levelsCompleted_8x8 < GameManager.Instance.totalLevels) {
            GameManager.Instance.currentLevel = GameManager.Instance.levelsCompleted_8x8 + 1; 
        } else {
            GameManager.Instance.currentLevel = GameManager.Instance.levelsCompleted_8x8;
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }

    public void Load8x8PackLevelSelectScene() {
        GameManager.Instance.currentLevelPack = 3;
        if (GameManager.Instance.levelsCompleted_8x8 < GameManager.Instance.totalLevels) {
            GameManager.Instance.currentLevel = GameManager.Instance.levelsCompleted_8x8 + 1; 
        } else {
            GameManager.Instance.currentLevel = GameManager.Instance.levelsCompleted_8x8;
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void Load9x9PackPlayScene() {
        GameManager.Instance.currentLevelPack = 4;
        if (GameManager.Instance.levelsCompleted_9x9 < GameManager.Instance.totalLevels) {
            GameManager.Instance.currentLevel = GameManager.Instance.levelsCompleted_9x9 + 1; 
        } else {
            GameManager.Instance.currentLevel = GameManager.Instance.levelsCompleted_9x9;
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene(6);
    }

    public void Load9x9PackLevelSelectScene() {
        GameManager.Instance.currentLevelPack = 4;
        if (GameManager.Instance.levelsCompleted_9x9 < GameManager.Instance.totalLevels) {
            GameManager.Instance.currentLevel = GameManager.Instance.levelsCompleted_9x9 + 1; 
        } else {
            GameManager.Instance.currentLevel = GameManager.Instance.levelsCompleted_9x9;
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void LoadMainMenuScene() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void LoadSettingsScene() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
}
