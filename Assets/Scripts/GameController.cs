using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    private GridCell[,] cellGrid = new GridCell[12,12];
    private int boardSize;

    private Tile[] tiles;

    // Start is called before the first frame update
    void Start() {
        PopulateCellGrid();
        LoadLevelData(GameManager.GM.currentLevel);
        LoadTiles();
    }

    private void PopulateCellGrid() {
        
        for(int i = 0; i < 12; i++) {
            for (int j = 0; j < 12; j++) {

                // Find the cell in the grid and get its GridCell component
                GridCell cell = GameObject.Find("GridCell " + i + "," + j).gameObject.GetComponent<GridCell>();

                // Set the appropriate index in cellGrid to this cell
                cellGrid[j,i] = cell;

                // Set the cell's coordinate properties
                cell.SetCoordinates(j,i);
            }
        }
    }

    private void LoadLevelData(int level) {

        int levelToLoadIndex = level - 1;
        int [,] data = new int[12,12];

        // Retrieve the 2-dimensional array of level data from the 3D array of levels data
        for(int i = 0; i < 12; i++) {
            for (int j = 0; j < 12; j++) {

                data[i,j] = GameManager.GM.levelBoardData[levelToLoadIndex,i,j];
            }
        }

        // Populate cellGrid according to level data retrieved from levels data matrix
        for(int i = 0; i < 12; i++) {
            for (int j = 0; j < 12; j++) {

                // Set the appropriate cell's status to the specification in data matrix
                cellGrid[i,j].SetState(data[i,j]);

                // Update the cell's image
                cellGrid[i,j].UpdateImage();
            }
        }
    }

    private void LoadTiles() {

        GameObject[] objects = GameObject.FindGameObjectsWithTag("Tile");
        tiles = new Tile[objects.Length];

        for (int i = 0; i < objects.Length; i++) {
            tiles[i] = objects[i].GetComponent<Tile>();
        }

        Debug.Log(tiles.Length);
    }
}
