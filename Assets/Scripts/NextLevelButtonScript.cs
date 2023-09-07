using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextLevelButtonScript : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        if (GameManager.Instance.currentLevel == GameManager.Instance.totalLevels) {
            gameObject.GetComponent<Button>().enabled = false;
            gameObject.GetComponent<Image>().color = new Color(1,1,1,0.25f);
        }
    }
}
