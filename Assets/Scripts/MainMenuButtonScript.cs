using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtonScript : MonoBehaviour {
  
    public void LoadClassicPackLevelSeScene() {
        GameManager.Instance.currentLevelPack = 0;
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void LoadBonusPackLevelSeScene() {
        GameManager.Instance.currentLevelPack = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void Load6x6PackLevelSeScene() {
        GameManager.Instance.currentLevelPack = 2;
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void Load7x7PackLevelSeScene() {
        GameManager.Instance.currentLevelPack = 3;
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void Load8x8PackLevelSeScene() {
        GameManager.Instance.currentLevelPack = 4;
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void Load9x9PackLevelSeScene() {
        GameManager.Instance.currentLevelPack = 5;
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void LoadMainMenuScene() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void LoadSettingsScene() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }

}
