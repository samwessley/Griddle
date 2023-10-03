using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using LionStudios.Suite.Debugging; 

public class TutorialController : MonoBehaviour {

    public int index = 0;

    public GameObject tile1 = null;
    public GameObject tile2 = null;
    public GameObject tile3 = null;
    public GameObject tile4 = null;
    public GameObject tile5 = null;
    public GameObject tile6 = null;

    // Start is called before the first frame update
    void Start() {
        SetUpPopup1Tiles();
        LionDebugger.Hide();
    }

    private void SetUpPopup1Tiles() {
        TutorialTile tile1Object = tile1.GetComponent<TutorialTile>();
        TutorialTile tile2Object = tile2.GetComponent<TutorialTile>();
        TutorialTile tile3Object = tile3.GetComponent<TutorialTile>();
        TutorialTile tile4Object = tile4.GetComponent<TutorialTile>();
        TutorialTile tile5Object = tile5.GetComponent<TutorialTile>();
        TutorialTile tile6Object = tile6.GetComponent<TutorialTile>();

        tile1Object.ResetTile();
        tile2Object.ResetTile();
        tile3Object.ResetTile();
        tile4Object.ResetTile();
        tile5Object.ResetTile();
        tile6Object.ResetTile();

        tile4Object.Reflect();
        tile6Object.Reflect();

        RotateTileNTimes(tile1Object, 3);
        RotateTileNTimes(tile2Object, 1);
        RotateTileNTimes(tile3Object, 2);
        RotateTileNTimes(tile4Object, 3);
        RotateTileNTimes(tile6Object, 2);
    }

    private void RotateTileNTimes(TutorialTile tile, int n) {        
        for (int i = 0; i < n; i++) {
            tile.Rotate();
        }
    }

    public void Continue() {
        index = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(index);
    }

    public void ExitToMainMenu() {
        GameManager.Instance.tutorialShown = true;
        GameManager.Instance.SaveAsJSON();
        SceneManager.LoadScene(0);
    }
}
