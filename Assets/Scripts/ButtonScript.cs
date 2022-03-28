using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour {

    public void LoadMenuScene() {
        SceneManager.LoadScene(1);
    }

    public void LoadPreviousLevel() {
        // Increment current level
        if (GameManager.Instance.currentLevel > 1) {
            GameManager.Instance.currentLevel -= 1;

            // Load new scene
            GameManager.Instance.LoadNewScene();
        } else {
            SceneManager.LoadScene(0);
        }
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
        GameController gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();

        string distinctCharsRemaining = gameController.distinctCharsRemaining;
        System.Random random = new System.Random();
        int randomCharIndex = random.Next(distinctCharsRemaining.Length);
        char randomChar = distinctCharsRemaining[randomCharIndex];
        string charsOccupied = "";

        GridCell[,] cellGrid = gameController.cellGrid;

        // Hide the tile that was just played via hint
        GameObject tileHinted = gameController.GetTileWithCode(randomChar);
        tileHinted.SetActive(false);

        if (tileHinted.GetComponent<DragDrop>().isOnBoard) {
            //tileHinted.GetComponent<DragDrop>().CancelPlacement(tileHinted.GetComponent<Tile>());
            //tileHinted.GetComponent<DragDrop>().isOnBoard = false;
            tileHinted.GetComponent<Tile>().CancelPlacement();
            ResetOccupiedCells(gameController, tileHinted.GetComponent<Tile>().tileCode.ToString());
            Debug.Log("Reset cell " + tileHinted.GetComponent<Tile>().tileCode.ToString());
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

            if (tile.GetComponent<DragDrop>().isHighlighted) {

                TileCell[] tileCells = tile.GetComponent<Tile>().tileCells;
                
                // Rotate the Tile transform
                int rotations = tile.GetComponent<Tile>().rotations;
                if (rotations < 3) {
                    tile.transform.eulerAngles = new Vector3(
                        tile.transform.eulerAngles.x,
                        tile.transform.eulerAngles.y,
                        tile.transform.eulerAngles.z + 90
                    );

                    for (int i = 0; i < tileCells.Length; i++) {
                        
                        // Rotate the tile cell transforms
                        if (tile.GetComponent<Tile>().reflected)
                        tileCells[i].GetComponent<RectTransform>().Rotate(0, 0, -270);
                        else
                        tileCells[i].GetComponent<RectTransform>().Rotate(0, 0, -90);

                        // Rotate the tile cell offset values
                        float oldX = tileCells[i].xOffset;
                        float oldY = tileCells[i].yOffset;

                        tileCells[i].xOffset = -oldY;
                        tileCells[i].yOffset = oldX;
                    }

                    tile.GetComponent<Tile>().rotations += 1;
                } else {
                    
                    tile.GetComponent<Tile>().rotations = 0;

                    // Reset the tile back to its default state
                    ResetTile(tile);

                    // If the tile was reflected before reset, reflect it back
                    if (tile.GetComponent<Tile>().reflected) {
                        // Reflect the transform
                        tile.transform.localScale = new Vector2(-1, 1);
            
                        // Reflect the tile cells' offset locations
                        for (int j = 0; j < tileCells.Length; j++) {
                            tileCells[j].xOffset = -tileCells[j].xOffset;
                        }
                    }
                }
    
                // Update the sorting order of each tile cell
                UpdateSortingOrder(tileCells);
            }
        }
    }

    public void ReflectTile() {

        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");

        for (int i = 0; i < tiles.Length; i++) {
            if (tiles[i].GetComponent<DragDrop>().isHighlighted) {
                
                GameObject tile = tiles[i].gameObject;
                TileCell[] tileCells = tile.GetComponent<Tile>().tileCells;
                bool tileReflected = tile.GetComponent<Tile>().reflected;

                // Reset the tile back to its default state
                ResetTile(tile);

                // If the tile isn't reflected, reflect it
                if (!tileReflected) {
                    // Reflect the transform
                    float tileXScale = Mathf.Abs(tiles[i].transform.localScale.x);
                    float tileYScale = tiles[i].transform.localScale.y;
                    tiles[i].transform.localScale = new Vector2(-tileXScale, tileYScale);
        
                    // Reflect the tile cells' offset locations
                    for (int j = 0; j < tileCells.Length; j++) 
                        tileCells[j].xOffset = -tileCells[j].xOffset;
                }

                // Update reflected property
                tiles[i].GetComponent<Tile>().reflected = !tiles[i].GetComponent<Tile>().reflected;

                // Update the sorting order of each tile cell
                UpdateSortingOrder(tileCells);
            }
        }
    }

    private void UpdateSortingOrder(TileCell[] tileCells) {

        List<int> rowList = new List<int>();

        foreach(TileCell cell in tileCells) {
            if (!rowList.Contains((int)cell.yOffset))
            rowList.Add((int)cell.yOffset);
        }
        rowList.Sort();
        rowList.Reverse();

        foreach(TileCell cell in tileCells) {
            cell.SetSortingLayer(19 + rowList.IndexOf((int)cell.yOffset));
        }
    }

    private void ResetTile(GameObject tile) {

        // Reset the tile transform back to its default state
        tile.transform.rotation = Quaternion.identity;
        tile.transform.localScale = new Vector2(1, 1);
        tile.GetComponent<Tile>().rotations = 0;

        TileCell[] tileCells = tile.GetComponent<Tile>().tileCells;

        // Reset the tile cells back to their default state
        for (int j = 0; j < tileCells.Length; j++) {
            tileCells[j].transform.localScale = new Vector2(1, 1);
            tileCells[j].transform.rotation = Quaternion.identity;

            tileCells[j].PopulateOffsetValues();
        }
    }
}
