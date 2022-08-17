using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelButtonScript : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        if (GameManager.Instance.currentLevel == GameManager.Instance.totalLevels)
        gameObject.SetActive(false);
    }
}
