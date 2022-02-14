using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPageButtonScript : MonoBehaviour {

    [SerializeField] GameObject levelController = null;

    public void LoadNextLevelPage() {
        levelController.GetComponent<LevelController>().LoadNextLevelPage();
    }

    public void LoadPreviousLevelPage() {
        levelController.GetComponent<LevelController>().LoadPreviousLevelPage();
    }
}
