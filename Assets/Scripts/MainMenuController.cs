using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

    [SerializeField] GameObject levelNumberLabel5x5 = null;
    [SerializeField] GameObject levelNumberLabel6x6 = null;
    [SerializeField] GameObject levelNumberLabel7x7 = null;
    [SerializeField] GameObject levelNumberLabel8x8 = null;
    [SerializeField] GameObject levelNumberLabel9x9 = null;

    [SerializeField] GameObject check5x5 = null;
    [SerializeField] GameObject check6x6 = null;
    [SerializeField] GameObject check7x7 = null;
    [SerializeField] GameObject check8x8 = null;
    [SerializeField] GameObject check9x9 = null;

    private void Start() {
        levelNumberLabel5x5.GetComponent<Text>().text = "LVL " + (GameManager.Instance.levelsCompleted_5x5 + 1);
        levelNumberLabel6x6.GetComponent<Text>().text = "LVL " + (GameManager.Instance.levelsCompleted_6x6 + 1);
        levelNumberLabel7x7.GetComponent<Text>().text = "LVL " + (GameManager.Instance.levelsCompleted_7x7 + 1);
        levelNumberLabel8x8.GetComponent<Text>().text = "LVL " + (GameManager.Instance.levelsCompleted_8x8 + 1);
        levelNumberLabel9x9.GetComponent<Text>().text = "LVL " + (GameManager.Instance.levelsCompleted_9x9 + 1);
    
        if (GameManager.Instance.levelsCompleted_5x5 == 200) {
            check5x5.SetActive(true);
        } else {
            check5x5.SetActive(false);
        }

        if (GameManager.Instance.levelsCompleted_6x6 == 200) {
            check6x6.SetActive(true);
        } else {
            check6x6.SetActive(false);
        }

        if (GameManager.Instance.levelsCompleted_7x7 == 200) {
            check7x7.SetActive(true);
        } else {
            check7x7.SetActive(false);
        }

        if (GameManager.Instance.levelsCompleted_8x8 == 200) {
            check8x8.SetActive(true);
        } else {
            check8x8.SetActive(false);
        }

        if (GameManager.Instance.levelsCompleted_9x9 == 200) {
            check9x9.SetActive(true);
        } else {
            check9x9.SetActive(false);
        }
    }
}
