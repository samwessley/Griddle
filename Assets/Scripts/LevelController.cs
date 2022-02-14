using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

    [SerializeField] GameObject nextLevelPageButton = null;
    [SerializeField] GameObject previousLevelPageButton = null;

    [SerializeField] GameObject level0A = null;
    [SerializeField] GameObject level0B = null;
    [SerializeField] GameObject level0C = null;
    [SerializeField] GameObject level0D = null;
    [SerializeField] GameObject level0E = null;

    [SerializeField] GameObject level1A = null;
    [SerializeField] GameObject level1B = null;
    [SerializeField] GameObject level1C = null;
    [SerializeField] GameObject level1D = null;
    [SerializeField] GameObject level1E = null;

    [SerializeField] GameObject level2A = null;
    [SerializeField] GameObject level2B = null;
    [SerializeField] GameObject level2C = null;
    [SerializeField] GameObject level2D = null;
    [SerializeField] GameObject level2E = null;

    [SerializeField] GameObject level3A = null;
    [SerializeField] GameObject level3B = null;
    [SerializeField] GameObject level3C = null;
    [SerializeField] GameObject level3D = null;
    [SerializeField] GameObject level3E = null;

    [SerializeField] GameObject level4A = null;
    [SerializeField] GameObject level4B = null;
    [SerializeField] GameObject level4C = null;
    [SerializeField] GameObject level4D = null;
    [SerializeField] GameObject level4E = null;

    [SerializeField] GameObject level5A = null;
    [SerializeField] GameObject level5B = null;
    [SerializeField] GameObject level5C = null;
    [SerializeField] GameObject level5D = null;
    [SerializeField] GameObject level5E = null;

    private int page = 1;

    public void SetLevelPage(int page) {

        level0A.gameObject.GetComponent<LevelSelectButtonScript>().level += page;
        level0A.gameObject.GetComponent<LevelSelectButtonScript>().SetUpButton();
        level0B.gameObject.GetComponent<LevelSelectButtonScript>().level += page;
        level0B.gameObject.GetComponent<LevelSelectButtonScript>().SetUpButton();
        level0C.gameObject.GetComponent<LevelSelectButtonScript>().level += page;
        level0C.gameObject.GetComponent<LevelSelectButtonScript>().SetUpButton();
        level0D.gameObject.GetComponent<LevelSelectButtonScript>().level += page;
        level0D.gameObject.GetComponent<LevelSelectButtonScript>().SetUpButton();
        level0E.gameObject.GetComponent<LevelSelectButtonScript>().level += page;
        level0E.gameObject.GetComponent<LevelSelectButtonScript>().SetUpButton();
        
        level1A.gameObject.GetComponent<LevelSelectButtonScript>().level += page;
        level1A.gameObject.GetComponent<LevelSelectButtonScript>().SetUpButton();
        level1B.gameObject.GetComponent<LevelSelectButtonScript>().level += page;
        level1B.gameObject.GetComponent<LevelSelectButtonScript>().SetUpButton();
        level1C.gameObject.GetComponent<LevelSelectButtonScript>().level += page;
        level1C.gameObject.GetComponent<LevelSelectButtonScript>().SetUpButton();
        level1D.gameObject.GetComponent<LevelSelectButtonScript>().level += page;
        level1D.gameObject.GetComponent<LevelSelectButtonScript>().SetUpButton();
        level1E.gameObject.GetComponent<LevelSelectButtonScript>().level += page;
        level1E.gameObject.GetComponent<LevelSelectButtonScript>().SetUpButton();

        level2A.gameObject.GetComponent<LevelSelectButtonScript>().level += page;
        level2A.gameObject.GetComponent<LevelSelectButtonScript>().SetUpButton();
        level2B.gameObject.GetComponent<LevelSelectButtonScript>().level += page;
        level2B.gameObject.GetComponent<LevelSelectButtonScript>().SetUpButton();
        level2C.gameObject.GetComponent<LevelSelectButtonScript>().level += page;
        level2C.gameObject.GetComponent<LevelSelectButtonScript>().SetUpButton();
        level2D.gameObject.GetComponent<LevelSelectButtonScript>().level += page;
        level2D.gameObject.GetComponent<LevelSelectButtonScript>().SetUpButton();
        level2E.gameObject.GetComponent<LevelSelectButtonScript>().level += page;
        level2E.gameObject.GetComponent<LevelSelectButtonScript>().SetUpButton();

        level3A.gameObject.GetComponent<LevelSelectButtonScript>().level += page;
        level3A.gameObject.GetComponent<LevelSelectButtonScript>().SetUpButton();
        level3B.gameObject.GetComponent<LevelSelectButtonScript>().level += page;
        level3B.gameObject.GetComponent<LevelSelectButtonScript>().SetUpButton();
        level3C.gameObject.GetComponent<LevelSelectButtonScript>().level += page;
        level3C.gameObject.GetComponent<LevelSelectButtonScript>().SetUpButton();
        level3D.gameObject.GetComponent<LevelSelectButtonScript>().level += page;
        level3D.gameObject.GetComponent<LevelSelectButtonScript>().SetUpButton();
        level3E.gameObject.GetComponent<LevelSelectButtonScript>().level += page;
        level3E.gameObject.GetComponent<LevelSelectButtonScript>().SetUpButton();

        level4A.gameObject.GetComponent<LevelSelectButtonScript>().level += page;
        level4A.gameObject.GetComponent<LevelSelectButtonScript>().SetUpButton();
        level4B.gameObject.GetComponent<LevelSelectButtonScript>().level += page;
        level4B.gameObject.GetComponent<LevelSelectButtonScript>().SetUpButton();
        level4C.gameObject.GetComponent<LevelSelectButtonScript>().level += page;
        level4C.gameObject.GetComponent<LevelSelectButtonScript>().SetUpButton();
        level4D.gameObject.GetComponent<LevelSelectButtonScript>().level += page;
        level4D.gameObject.GetComponent<LevelSelectButtonScript>().SetUpButton();
        level4E.gameObject.GetComponent<LevelSelectButtonScript>().level += page;
        level4E.gameObject.GetComponent<LevelSelectButtonScript>().SetUpButton();

        level5A.gameObject.GetComponent<LevelSelectButtonScript>().level += page;
        level5A.gameObject.GetComponent<LevelSelectButtonScript>().SetUpButton();
        level5B.gameObject.GetComponent<LevelSelectButtonScript>().level += page;
        level5B.gameObject.GetComponent<LevelSelectButtonScript>().SetUpButton();
        level5C.gameObject.GetComponent<LevelSelectButtonScript>().level += page;
        level5C.gameObject.GetComponent<LevelSelectButtonScript>().SetUpButton();
        level5D.gameObject.GetComponent<LevelSelectButtonScript>().level += page;
        level5D.gameObject.GetComponent<LevelSelectButtonScript>().SetUpButton();
        level5E.gameObject.GetComponent<LevelSelectButtonScript>().level += page;
        level5E.gameObject.GetComponent<LevelSelectButtonScript>().SetUpButton();
    }

    public void LoadNextLevelPage() {
        if (page < 8) {
            SetLevelPage(30);
            page += 1;
        } else {
            nextLevelPageButton.SetActive(false);
        }

        Debug.Log(page);
    }

    public void LoadPreviousLevelPage() {
        if (page > 1) {
            SetLevelPage(-30);
            page -= 1;
        } else {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }

        if (page == 7)
        nextLevelPageButton.SetActive(true);

        Debug.Log(page);
    }
}
