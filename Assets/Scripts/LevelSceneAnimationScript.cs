using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSceneAnimationScript : MonoBehaviour {

    public GameObject[] gridCells;
    public float[] gridCellYPositions;
    public GameObject[] tiles;
    private Vector2[] tileScales;

    [SerializeField] GameObject line = null;
    [SerializeField] GameObject backButton = null;
    [SerializeField] GameObject menuButton = null;
    [SerializeField] GameObject restartButton = null;
    [SerializeField] GameObject hintButton = null;
    [SerializeField] GameObject skipButton = null;
    [SerializeField] GameObject hintLabel = null;
    [SerializeField] GameObject skipLabel = null;
    [SerializeField] GameObject levelNumber = null;
    [SerializeField] GameObject grid = null;

    private float backButtonYPosition = 0;
    private float menuButtonYPosition = 0;
    private float restartButtonYPosition = 0;
    private float hintButtonYPosition = 0;
    private float skipButtonYPosition = 0;
    private float hintLabelYPosition = 0;
    private float skipLabelYPosition = 0;
    private float levelNumberYPosition = 0;
    private float gridYPosition = 0;

    // Start is called before the first frame update
    void Start() {
        backButtonYPosition = backButton.transform.position.y;
        menuButtonYPosition = menuButton.transform.position.y;
        restartButtonYPosition = restartButton.transform.position.y;
        hintButtonYPosition = hintButton.transform.position.y;
        skipButtonYPosition = skipButton.transform.position.y;
        hintLabelYPosition = hintLabel.transform.position.y;
        skipLabelYPosition = skipLabel.transform.position.y;
        levelNumberYPosition = levelNumber.transform.position.y;
        gridYPosition = grid.transform.position.y;

        //GatherGridCells();
        GatherTiles();
        gridCellYPositions = new float[gridCells.Length];

        tileScales = new Vector2[tiles.Length];
        GetGridCellYPositions();
        GetTileScales();
        StartCoroutine(StartAnimation());
    }

    IEnumerator StartAnimation() {

        // Shrink all the tiles
        for (int i = 0; i < tiles.Length; i++) {
            LeanTween.scale(tiles[i], Vector2.zero, 0);
        }

        // Move all grid cells off the screen
        /*for (int i = 0; i < gridCellYPositions.Length; i++) {
            LeanTween.scale(gridCells[i], Vector2.zero, 0);
        }*/
        LeanTween.moveY(grid, gridYPosition + 6f, 0);

        // Move all buttons off screen
        LeanTween.moveY(backButton, backButtonYPosition - 2f, 0);
        LeanTween.moveY(menuButton, menuButtonYPosition - 2f, 0);
        LeanTween.moveY(restartButton, restartButtonYPosition - 2f, 0);
        LeanTween.moveY(hintButton, hintButtonYPosition - 2f, 0);
        LeanTween.moveY(skipButton, skipButtonYPosition - 2f, 0);
        LeanTween.moveY(hintLabel, hintLabelYPosition - 2f, 0);
        LeanTween.moveY(skipLabel, skipLabelYPosition - 2f, 0);
        LeanTween.moveY(levelNumber, levelNumberYPosition + 2f, 0); 

        // Animate line
        LeanTween.scaleX(line, 0, 0);
        LeanTween.scaleX(line, 1, 0.25f);

        // Animate grid
        //StartCoroutine(GridAnimation());
        LeanTween.moveY(grid, gridYPosition, 0.35f).setEase(LeanTweenType.easeOutBack);

        // Animate buttons
        LeanTween.moveY(levelNumber, levelNumberYPosition, 0.2f).setEase(LeanTweenType.easeOutBack);
        LeanTween.moveY(backButton, backButtonYPosition, 0.2f).setEase(LeanTweenType.easeOutBack);
        yield return new WaitForSeconds(0.05f);
        LeanTween.moveY(menuButton, menuButtonYPosition, 0.2f).setEase(LeanTweenType.easeOutBack);
        yield return new WaitForSeconds(0.05f);
        LeanTween.moveY(restartButton, restartButtonYPosition, 0.2f).setEase(LeanTweenType.easeOutBack);
        yield return new WaitForSeconds(0.05f);
        LeanTween.moveY(hintButton, hintButtonYPosition, 0.2f).setEase(LeanTweenType.easeOutBack);
        LeanTween.moveY(hintLabel, hintLabelYPosition, 0.2f).setEase(LeanTweenType.easeOutBack);
        yield return new WaitForSeconds(0.05f);
        LeanTween.moveY(skipButton, skipButtonYPosition, 0.2f).setEase(LeanTweenType.easeOutBack);
        LeanTween.moveY(skipLabel, skipLabelYPosition, 0.2f).setEase(LeanTweenType.easeOutBack);

        // Animate tiles
        for (int i = 0; i < tiles.Length; i++) {
            LeanTween.scale(tiles[i], tileScales[i], 0.1f).setEase(LeanTweenType.easeOutBack);
            yield return new WaitForSeconds(0.03f);
        }
    }

    IEnumerator GridAnimation() {
    
        int numberPerRow = 5 + GameManager.Instance.currentLevelPack;
        for (int i = gridCells.Length; i > 0; i--) {
            for (int j = 0; j < numberPerRow; j++) {
                LeanTween.scale(gridCells[i - 1], new Vector2(1,1), 0.2f).setEase(LeanTweenType.easeOutBack);
            }
            yield return new WaitForSeconds(0.005f);
        }
    }   

    private void GatherGridCells() {
        gridCells = GameObject.FindGameObjectsWithTag("GridCell");
    }

    private void GetGridCellYPositions() {
        for (int i = 0; i < gridCellYPositions.Length; i++) {
            gridCellYPositions[i] = gridCells[i].transform.position.y;
        }
    }

    private void GatherTiles() {
        tiles = GameObject.FindGameObjectsWithTag("Tile");
    }

    private void GetTileScales() {

        float scaleFactor = 1;

        if (GameManager.Instance.currentLevelPack == 0) {
            scaleFactor = 0.8f;
        } else {
            scaleFactor = 0.7f;
        }

        for (int i = 0; i < tiles.Length; i++) {

            if (tiles[i].transform.localScale.x < 0) {
                if (tiles[i].transform.localScale.y < 0) {
                    tileScales[i] = new Vector2(-1 * scaleFactor,-1 * scaleFactor);
                } else {
                    tileScales[i] = new Vector2(-1 * scaleFactor,1 * scaleFactor);
                }
            } else {
                if (tiles[i].transform.localScale.y < 0) {
                    tileScales[i] = new Vector2(1*scaleFactor,-1*scaleFactor);
                } else {
                    tileScales[i] = new Vector2(1*scaleFactor,1*scaleFactor);
                }
            }
        }
    }
}
