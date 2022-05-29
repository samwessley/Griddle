using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtonScript : MonoBehaviour {

    public void LoadCurrentLevel() {
        GameManager.Instance.currentLevel = GameManager.Instance.levelsUnlocked;

        // Load new scene
        GameManager.Instance.LoadNewScene();
    }
  
    public void LoadLevelSelectScene() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void LoadMainMenuScene() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void LoadSettingsScene() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }

}
