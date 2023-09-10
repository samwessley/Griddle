using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToMainMenuScript : MonoBehaviour {
    public void Back() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
