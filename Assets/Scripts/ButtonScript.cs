using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour {

    public void LoadMainMenuScene() {
        SceneManager.LoadScene(0);
    }

    public void LoadLevelSelectScene() {
        SceneManager.LoadScene(1);
    }

    public void LoadNextLevel() {
        // Increment current level
        if (GameManager.Instance.currentLevel < GameManager.Instance.totalLevels) {
            GameManager.Instance.currentLevel += 1;

            // Load new scene
            GameManager.Instance.LoadNewScene();
        } else {
            SceneManager.LoadScene(0);
        }
    }

    public void LevelFinishedLoadNextLevel() {
        // Increment current level
        if (GameManager.Instance.currentLevel < GameManager.Instance.totalLevels) {

            GameManager.Instance.currentLevel += 1;

            // Show interstitial ad
            AdManager.Instance.ShowAd();

            // Load new scene
            GameManager.Instance.LoadNewScene();
        } else {
            SceneManager.LoadScene(0);
        }
    }

    public void RestartLevel() {

        // Reload scene
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }

    public void Hint() {
        if (GameManager.Instance.hintsRemaining > 0) {
            GameController gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();

            string distinctCharsRemaining = gameController.distinctCharsRemaining;
            System.Random random = new System.Random();
            int randomCharIndex = random.Next(distinctCharsRemaining.Length - 1);
            char randomChar = distinctCharsRemaining[randomCharIndex];
            string charsOccupied = "";

            GridCell[,] cellGrid = gameController.cellGrid;

            // Hide the tile that was just played via hint
            GameObject tileHinted = gameController.GetTileWithCode(randomChar);
            tileHinted.SetActive(false);

            if (tileHinted.GetComponent<DragDrop>().isOnBoard) {
                tileHinted.GetComponent<Tile>().CancelPlacement();
                ResetOccupiedCells(gameController, tileHinted.GetComponent<Tile>().tileCode.ToString());
                Debug.Log("Reset cell " + tileHinted.GetComponent<Tile>().tileCode.ToString());
                gameController.distinctCharsRemaining += tileHinted.GetComponent<Tile>().tileCode.ToString();
            }

            for(int y = 0; y < gameController.boardSize; y++) {
                for (int x = 0; x < gameController.boardSize; x++) {

                    // Find the cell in the grid and get its GridCell component
                    GridCell cell = GameObject.Find("GridCell " + x + "," + y).gameObject.GetComponent<GridCell>();

                    if (cell.state == randomChar) {
                        if (cell.isOccupied) {
                            
                            if (charsOccupied.Length == 0) {
                                charsOccupied += cell.charOccupying;
                            } else {
                                bool shouldAddChar = true;
                                for (int i = 0; i < charsOccupied.Length; i++) {
                                    if (charsOccupied[i].Equals(cell.charOccupying)) {
                                        shouldAddChar = false;
                                    }
                                }
                                if (shouldAddChar)
                                charsOccupied += cell.charOccupying;
                            }

                            GameObject tileInSpace = gameController.GetTileWithCode(cell.charOccupying);
                            tileInSpace.GetComponent<DragDrop>().CancelPlacement(tileInSpace.GetComponent<Tile>());
                            
                        }
                        cell.isOccupied = true;
                        int value = 0;
                        if (GameManager.Instance.tileColorDictionary.TryGetValue(randomChar, out value)) {
                            cell.colorOccupying = value;
                            cell.charOccupying = randomChar;
                        } else {
                            Debug.Log("Value not found in tileColorDictionary");
                        }

                        cell.UpdateImage();
                    }
                }
            }
            ResetOccupiedCells(gameController, charsOccupied);
            Debug.Log("charsOccupied:" + charsOccupied);
            gameController.tilesRemaining += charsOccupied.Length;
            gameController.distinctCharsRemaining = gameController.distinctCharsRemaining.Replace(randomChar.ToString(),"");

            if (!gameController.GetTileWithCode(randomChar).GetComponent<DragDrop>().isOnBoard) {
                gameController.tilesRemaining -= 1;
                Debug.Log("tile was not on board");
            }

            if (tileHinted.GetComponent<DragDrop>().isOnBoard) {
                tileHinted.GetComponent<DragDrop>().CancelPlacement(tileHinted.GetComponent<Tile>());
                //tileHinted.GetComponent<DragDrop>().isOnBoard = false;
            }

            gameController.CheckForLevelComplete();
            Debug.Log(gameController.tilesRemaining);
            GameManager.Instance.hintsRemaining -= 1;
            gameController.UpdateHintsLabel();
        }
    }

    public void Skip() {
        GameController gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();

        if (GameManager.Instance.skipsRemaining > 0) {
            GameManager.Instance.skipsRemaining -= 1;

            // Increment current level
            if (GameManager.Instance.currentLevel < 200) {
                GameManager.Instance.currentLevel += 1;

                // Increment the levels completed for the appropriate level pack
                if (GameManager.Instance.currentLevelPack == 0) {
                    GameManager.Instance.levelsCompleted_5x5 += 1;
                } else if (GameManager.Instance.currentLevelPack == 1) {
                    GameManager.Instance.levelsCompleted_6x6 += 1;
                } else if (GameManager.Instance.currentLevelPack == 2) {
                    GameManager.Instance.levelsCompleted_7x7 += 1;
                } else if (GameManager.Instance.currentLevelPack == 3) {
                    GameManager.Instance.levelsCompleted_8x8 += 1;
                } else if (GameManager.Instance.currentLevelPack == 4) {
                    GameManager.Instance.levelsCompleted_9x9 += 1;
                } else {
                    Debug.Log("Error with level pack.");
                }

                // Load new scene
                GameManager.Instance.LoadNewScene();
            } else {
                SceneManager.LoadScene(0);
            }
        }
    }

    private void ResetOccupiedCells(GameController gameController, string tileCodes) {
        GridCell[,] cellGrid = gameController.cellGrid;

        for(int y = 0; y < gameController.boardSize; y++) {
            for (int x = 0; x < gameController.boardSize; x++) {
                for (int i = 0; i < tileCodes.Length; i++) {
                    if (cellGrid[x,y].charOccupying == tileCodes[i]) {
                        cellGrid[x,y].charOccupying = (char)0;
                        cellGrid[x,y].colorOccupying = 0;
                        cellGrid[x,y].isOccupied = false;
                        Debug.Log("tileCode reset:" + tileCodes[i]);
                    }
                }
            }
        }
    }

    public void RotateTile() {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
    
        foreach(GameObject tile in tiles) {
            if (tile.GetComponent<DragDrop>().isHighlighted)
            tile.GetComponent<Tile>().Rotate();
        }
    }

    public void ReflectTile() {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");

        for (int i = 0; i < tiles.Length; i++) {
            if (tiles[i].GetComponent<DragDrop>().isHighlighted) {
                tiles[i].GetComponent<Tile>().Reflect();
            }
        }
    }
}
