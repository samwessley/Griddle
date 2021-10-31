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
        LoadLevelData(GameManager.Instance.currentLevel);
        LoadTiles();
    }

    private void PopulateCellGrid() {
        
        for(int y = 0; y < 12; y++) {
            for (int x = 0; x < 12; x++) {

                // Find the cell in the grid and get its GridCell component
                GridCell cell = GameObject.Find("GridCell " + x + "," + y).gameObject.GetComponent<GridCell>();

                // Set the appropriate index in cellGrid to this cell
                cellGrid[x,y] = cell;

                // Set the cell's coordinate properties
                cell.SetCoordinates(x,y);
            }
        }
    }

    private void LoadLevelData(int level) {

        int levelToLoadIndex = level - 1;
        int [,] data = new int[12,12];

        // Retrieve the 2-dimensional array of level data from the 3D array of levels data
        for(int y = 0; y < 12; y++) {
            for (int x = 0; x < 12; x++) {

                data[x,y] = GameManager.Instance.levelBoardData[levelToLoadIndex,y,x];
            }
        }

        // Populate cellGrid according to level data retrieved from levels data matrix
        for(int y = 0; y < 12; y++) {
            for (int x = 0; x < 12; x++) {

                // Set the appropriate cell's status to the specification in data matrix
                cellGrid[x,y].SetState(data[x,y]);

                // Update the cell's image
                cellGrid[x,y].UpdateImage();
            }
        }
    }

    private void LoadTiles() {

        GameObject[] objects = GameObject.FindGameObjectsWithTag("Tile");
        tiles = new Tile[objects.Length];

        for (int i = 0; i < objects.Length; i++) {
            tiles[i] = objects[i].GetComponent<Tile>();
        }
    }
}
