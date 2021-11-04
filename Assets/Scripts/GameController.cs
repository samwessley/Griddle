using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    [SerializeField] GameObject levelNumber = null;
    [SerializeField] GameObject canvas = null;

    private GridCell[,] cellGrid = new GridCell[12,12];
    private int boardSize;
    private int numberOfTiles;
    private GameObject[] tiles;

    public GameObject activeTile;

    private void Start() {
        PopulateCellGrid();
        LevelSetup();
    }

    public void LevelSetup() {
        LoadLevelData(GameManager.Instance.currentLevel);
        LoadTileData();
        LoadTiles();
        SetLevelNumber(GameManager.Instance.currentLevel);
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
                cell.SetSortingLayer();
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

        // Populate cellGrid according to level data retrieved from levels board data matrix
        for(int y = 0; y < 12; y++) {
            for (int x = 0; x < 12; x++) {

                // Set the appropriate cell's status to the specification in data matrix
                cellGrid[x,y].SetState(data[x,y]);

                // Update the cell's image
                cellGrid[x,y].UpdateImage();
            }
        }
    }

    private void LoadTileData() {

        numberOfTiles = GameManager.Instance.levelTiles[GameManager.Instance.currentLevel - 1].Length;

        tiles = new GameObject[numberOfTiles];
        float xLocation = -300;

        for (int i = 0; i < tiles.Length; i++) {
            string tileName = GameManager.Instance.levelTiles[GameManager.Instance.currentLevel - 1][i];

            tiles[i] = Instantiate(Resources.Load<GameObject>("Prefabs/Tiles/" + tileName));
            tiles[i].transform.SetParent(canvas.transform);
            tiles[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(xLocation, -600);
            xLocation += 300;
        }
    }

    private void LoadTiles() {

        GameObject[] objects = GameObject.FindGameObjectsWithTag("Tile");
        tiles = new GameObject[objects.Length];

        for (int i = 0; i < objects.Length; i++) {
            tiles[i] = objects[i];

        }
    }
    
    private void SetLevelNumber(int level) {
        levelNumber.GetComponent<Text>().text = level.ToString();
    }
}
