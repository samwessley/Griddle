using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class GameController : MonoBehaviour {

    [SerializeField] GameObject levelNumber = null;
    [SerializeField] GameObject canvas = null;
    [SerializeField] GameObject levelCompletePopup = null;
    [SerializeField] GameObject nextLevelButton = null;
    [SerializeField] GameObject hintsLabel = null;

    [SerializeField] GameObject star1 = null;
    [SerializeField] GameObject star2 = null;
    [SerializeField] GameObject star3 = null;
    [SerializeField] GameObject message = null;

    public GridCell[,] cellGrid;
    private int numberOfTiles;
    public GameObject[] tiles;

    public int boardSize;
    public string distinctChars;
    public GameObject activeTile;
    public int tilesRemaining;

    public string distinctCharsRemaining;

    private void Start() {
        PopulateCellGrid();
        LevelSetup();
        GameManager.Instance.SaveAsJSON();
        UpdateHintsLabel();
    }

    public void LevelSetup() {
        Debug.Log(GameManager.Instance.currentLevel);
        ReadLevelData(GameManager.Instance.currentLevel);
        ReadTileData(GameManager.Instance.currentLevel);
        LoadTiles();
        SetLevelNumber(GameManager.Instance.currentLevel);
    }

    private void PopulateCellGrid() {

        cellGrid = new GridCell[boardSize, boardSize];
        
        for(int y = 0; y < boardSize; y++) {
            for (int x = 0; x < boardSize; x++) {

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

    public void CheckForLevelComplete() {
        if (tilesRemaining == 0 && CheckForValidTilePlacement()) {
            StartCoroutine(LevelCompleteAnimation());

            //Save 'completed' status for appropriate level and level pack to the save file
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
            }

            GameManager.Instance.SaveAsJSON();
        }
    }

    IEnumerator LevelCompleteAnimation() {

        SetStarsAndMessage();

        LeanTween.scale(message, Vector2.zero, 0);
        LeanTween.rotateZ(message, -25f,0);

        yield return new WaitForSeconds(0.25f);

        levelCompletePopup.SetActive(true);

        LeanTween.scale(message, new Vector2(1,1), 0.08f);
        LeanTween.rotateZ(message,0,0.08f);
    }

    private void SetStarsAndMessage() {

        System.Random random = new System.Random();
        int randomInt = random.Next(1,9);

        switch(randomInt) {
            
            case 1: {
            message.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Nice!");
            RectTransform message_rt = message.GetComponent<RectTransform>();
            message_rt.sizeDelta = new Vector2 (328, 157);
            break;
            }

            case 2: {
            message.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Great!");
            RectTransform message_rt = message.GetComponent<RectTransform>();
            message_rt.sizeDelta = new Vector2 (412, 161);
            break;
            }

            case 3: {
            message.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Perfect!");
            RectTransform message_rt = message.GetComponent<RectTransform>();
            message_rt.sizeDelta = new Vector2 (505, 175);
            break;
            }

            case 4: {
            message.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Excellent!");
            RectTransform message_rt = message.GetComponent<RectTransform>();
            message_rt.sizeDelta = new Vector2 (616, 187);
            break;
            }

            case 5: {
            message.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Fabulous!");
            RectTransform message_rt = message.GetComponent<RectTransform>();
            message_rt.sizeDelta = new Vector2 (635, 189);
            break;
            }

            case 6: {
            message.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Outstanding!");
            RectTransform message_rt = message.GetComponent<RectTransform>();
            message_rt.sizeDelta = new Vector2 (876, 246);
            break;
            }

            case 7: {
            message.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Wonderful!");
            RectTransform message_rt = message.GetComponent<RectTransform>();
            message_rt.sizeDelta = new Vector2 (729, 199);
            break;
            }

            case 8: {
            message.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Sensational!");
            RectTransform message_rt = message.GetComponent<RectTransform>();
            message_rt.sizeDelta = new Vector2 (818, 209);
            break;
            }

            default:
                break;
        }
    }

    private string GetFilePath(string fileName) {

        return Application.persistentDataPath + "/" + fileName;
    }

    private void ReadLevelData(int level) {
        char[,] data = new char[boardSize,boardSize];

        string boardSizePath = GetLevelPath();
        string levelPath = "Levels/" + boardSizePath + "/" + level.ToString();
        TextAsset txtAsset = (TextAsset)Resources.Load(levelPath, typeof(TextAsset));
        string text = txtAsset.text;

        if (txtAsset != null) {
            using (StringReader reader = new StringReader(text)) {

                for (int y = 0; y < boardSize; y++) {
                    for (int x = 0; x < boardSize; x++) {
                        data[x,y] = (char)reader.Read();

                        // Set the appropriate cell's status to the specification in data matrix
                        cellGrid[x,y].SetState(data[x,y]);

                        // Update the cell's image
                        cellGrid[x,y].UpdateImage();
                    }
                    char unused__ = (char)reader.Read();
                }
            }
        } else {
            Debug.Log("File not found!");
        }
    }

    private void ReadTileData(int level) {

        string boardSize = GetLevelPath();

        string levelPath = "Levels/" + boardSize + "/" + level.ToString();
        TextAsset txtAsset = (TextAsset)Resources.Load(levelPath, typeof(TextAsset));
        string text = txtAsset.text;

        // Get distinct chars from the string of level data
        distinctChars = new string(text.Distinct().ToArray());
        distinctChars = distinctChars.Replace("1","");
        // Remove 0's
        distinctChars = distinctChars.Replace("0","");
        distinctChars = distinctChars.Replace("\n","");
        distinctCharsRemaining = distinctChars;
        Debug.Log(distinctChars);
        int numberOfTiles = distinctChars.Length;
        tilesRemaining = numberOfTiles;
        tiles = new GameObject[numberOfTiles];
        Vector2[] tileLocations = GetTileLocations();

        int i = 0;
        foreach (char c in distinctChars) {

            tiles[i] = Instantiate(Resources.Load<GameObject>("Prefabs/Tiles/" + GameManager.Instance.tileDictionary[c]));
            tiles[i].GetComponent<Tile>().tileColor = GameManager.Instance.tileColorDictionary[c];
            tiles[i].GetComponent<Tile>().tileCode = c;
            tiles[i].GetComponent<Tile>().GetTileCells();

            tiles[i].transform.SetParent(canvas.transform);
            tiles[i].GetComponent<RectTransform>().anchoredPosition = tileLocations[i];

            i++;
        }
        SetTileOrientations(level);
    }

    private void SetTileOrientations(int level) {
        // Get tile orientation file from folder
        string boardSize = GetLevelPath();
        string tileOrientationFilePath = "Levels/" + boardSize + "/" + level.ToString() + "_orientations";
        TextAsset txtAsset = (TextAsset)Resources.Load(tileOrientationFilePath, typeof(TextAsset));
        string text = txtAsset.text;

        // Iterate over each char in text and set appropriate tile's orientation
        for (int i = 0; i < text.Length; i++) {
            for (int j = 0; j < tiles.Length; j++) {
                Tile tile = tiles[j].GetComponent<Tile>();
                Debug.Log(tile.tileCode + ", " + text[i]);
                if (tile.tileCode == text[i]) {
                    if ((int)(text[i + 2] - '0') == 1) {
                        tile.Reflect();
                        Debug.Log("tile reflected");
                    }
                    RotateTileNTimes(tile, (int)(text[i + 1] - '0'));
                    Debug.Log("tile rotated " + (int)(text[i + 1] - '0') + "times");
                }
            }
        }
    }

    private void RotateTileNTimes(Tile tile, int n) {        
        for (int i = 0; i < n; i++) {
            tile.Rotate();
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
        GameManager.Instance.currentLevel = level;
    }

    private string GetLevelPath() {
        string boardSize = "";
        int levelNumber = GameManager.Instance.currentLevel;

        //Set the level pack folder
        if (GameManager.Instance.currentLevelPack == 0) {
            boardSize = "5x5";
        } else if (GameManager.Instance.currentLevelPack == 1) {
            boardSize = "6x6";
        } else if (GameManager.Instance.currentLevelPack == 2) {
            boardSize = "7x7";
        } else if (GameManager.Instance.currentLevelPack == 3) {
            boardSize = "8x8";
        } else if (GameManager.Instance.currentLevelPack == 4) {
            boardSize = "9x9";
        }

        return boardSize;
    }

    private bool CheckForValidTilePlacement() {
        
        // SIMPLE TEST FIRST
        // First check if all the tiles are in the correct position. If so, no need to do
        // the more in-depth logic below.
        bool allTilesInCorrectPosition = true;
        for (int i = 0; i < tiles.Length; i++) {
            Tile tile = tiles[i].GetComponent<Tile>();
            DragDrop tileDragDrop = tiles[i].GetComponent<DragDrop>();
            RectTransform[] closestGridCells = tile.GetClosestCellsArray();

            if (!tileDragDrop.IsInCorrectPosition(tile, closestGridCells))
            allTilesInCorrectPosition = false;
        }

        // If all tiles are in correct position, return true
        if (allTilesInCorrectPosition)
        return true;  
        
        // MORE IN-DEPTH TEST
        // This tests each tile's location according to the game rules manually. This should
        // catch situations that are valid tile placements but not the coded solution
        for (int i = 0; i < tiles.Length; i++) {
            Tile tile = tiles[i].GetComponent<Tile>();

            // Only check valid placement if the tile is on the board. If it's not on the
            // board, we know it was either placed with a hint (and is in a correct position),
            // or hasn't been played yet.
            if (tile.GetComponent<DragDrop>().isOnBoard) {
                RectTransform[] closestGridCellTransforms = tile.GetClosestCellsArray();
                GridCell[] closestGridCells = new GridCell[closestGridCellTransforms.Length];

                for (int j = 0; j < closestGridCellTransforms.Length; j++) {
                    closestGridCells[j] = closestGridCellTransforms[j].gameObject.GetComponent<GridCell>();
                }

                List<GridCell> sideCellsToTest = new List<GridCell>();
                List<GridCell> cornerCellsToTest = new List<GridCell>();

                foreach (GridCell cell in closestGridCells) {
                    
                    sideCellsToTest.AddRange(GetSideAdjacentCells(cell, closestGridCells));
                    cornerCellsToTest.AddRange(GetCornerAdjacentCells(cell, closestGridCells));
                }

                // Remove the side cells that are part of the tile
                GridCell[] sideCellsToTestArray = sideCellsToTest.ToArray();
                for (int g = 0; g < sideCellsToTestArray.Length; g++) {
                    for (int j = 0; j < closestGridCells.Length; j++) {
                        if (sideCellsToTestArray[g] == closestGridCells[j])
                        sideCellsToTest.Remove(sideCellsToTestArray[g]);
                    }
                }

                // Remove the corner cells that are part of the tile
                GridCell[] cornerCellsToTestArray = cornerCellsToTest.ToArray();
                for (int g = 0; g < cornerCellsToTestArray.Length; g++) {
                    for (int j = 0; j < closestGridCells.Length; j++) {
                        if (cornerCellsToTestArray[g] == closestGridCells[j])
                        cornerCellsToTest.Remove(cornerCellsToTestArray[g]);
                    }
                }

                // Loop through cornerCellsToTest and delete any cornerCells that are in sideCellsToTest
                sideCellsToTestArray = sideCellsToTest.ToArray();
                cornerCellsToTestArray = cornerCellsToTest.ToArray();
                for (int g = 0; g < cornerCellsToTestArray.Length; g++) {
                    for (int j = 0; j < sideCellsToTestArray.Length; j++) {
                        if (cornerCellsToTestArray[g] == sideCellsToTestArray[j])
                            cornerCellsToTest.Remove(cornerCellsToTestArray[g]);
                    } 
                }

                // At this point, we have our side adjacent cells and our corner adjacent cells.
                // Time to test that 1) no side adjacent cells are occupied and 2) at least one
                // corner adjacent cell is occupied. If both these conditions pass, return true

                // Loop through remaining side-adjacent cells to test for occupied
                foreach (GridCell cell in sideCellsToTest) {
                    if (cell.isOccupied && cell.colorOccupying == tile.tileColor) {
                        Debug.Log("Side cell test failed: " + tile.tileCode);
                        return false;
                    }
                }
                
                // Loop through corner-adjacent cells and make sure at least one is occupied
                bool oneCellIsValid = false;
                foreach (GridCell cell in cornerCellsToTest) {
                    if (cell.isOccupied && cell.colorOccupying == tile.tileColor)
                        oneCellIsValid = true;
                }
                if (!oneCellIsValid) {
                    Debug.Log("Corner cell test failed: " + tile.tileCode);

                    /*foreach (GridCell cell in cornerCellsToTest) {
                        Debug.Log(cell.xIndex + ", " + cell.yIndex);
                        Debug.Log(cell.isOccupied + ", " + cell.colorOccupying);
                    }*/

                    return false;
                }
            }
        }

        // If we get to this point, none of the tiles have any issues. This is a valid placement of tiles
        return true;
    }

    private List<GridCell> GetSideAdjacentCells(GridCell cell, GridCell[] closestGridCells) {
        
        List<GridCell> sideCellsToTest = new List<GridCell>();

        // Gather all the side adjacent cells for this grid cell
        if (cell.xIndex - 1 >= 0)
            sideCellsToTest.Add(cellGrid[cell.xIndex - 1, cell.yIndex]);
        if (cell.xIndex + 1 < boardSize)
            sideCellsToTest.Add(cellGrid[cell.xIndex + 1, cell.yIndex]);
        if (cell.yIndex - 1 >= 0)
            sideCellsToTest.Add(cellGrid[cell.xIndex, cell.yIndex - 1]);
        if (cell.yIndex + 1 < boardSize)
            sideCellsToTest.Add(cellGrid[cell.xIndex, cell.yIndex + 1]);

        return sideCellsToTest;
    }

    private List<GridCell> GetCornerAdjacentCells(GridCell cell, GridCell[] closestGridCells) {

        List<GridCell> cornerCellsToTest = new List<GridCell>();

        // Gather all the corner adjacent cells for this grid cell
        if (cell.xIndex - 1 >= 0 && cell.yIndex - 1 >= 0)
            cornerCellsToTest.Add(cellGrid[cell.xIndex - 1, cell.yIndex - 1]);
        if (cell.xIndex + 1 < boardSize && cell.yIndex - 1 >= 0)
            cornerCellsToTest.Add(cellGrid[cell.xIndex + 1, cell.yIndex - 1]);
        if (cell.xIndex - 1 >= 0 && cell.yIndex + 1 < boardSize)
            cornerCellsToTest.Add(cellGrid[cell.xIndex - 1, cell.yIndex + 1]);
        if (cell.xIndex + 1 < boardSize && cell.yIndex + 1 < boardSize)
            cornerCellsToTest.Add(cellGrid[cell.xIndex + 1, cell.yIndex + 1]);

        return cornerCellsToTest;
    }

    private Vector2[] GetTileLocations() {
        Vector2[] tileLocations;

        /*if (boardSize == 5) {
            tileLocations = new Vector2[] { new Vector2(-405, -480), new Vector2(-135, -480), new Vector2(135, -480),
                                            new Vector2(405, -480)};
        } */
        
        if (boardSize <= 5) {
            tileLocations = new Vector2[] { new Vector2(-310, -530), new Vector2(0, -530), new Vector2(310, -530),
                                            new Vector2(-310, -800), new Vector2(0, -800), new Vector2(310, -800)};
        } else if (boardSize == 6) {
            tileLocations = new Vector2[] { new Vector2(-310, -380), new Vector2(0, -380), new Vector2(310, -380),
                                            new Vector2(-310, -590), new Vector2(0, -590), new Vector2(310, -590),
                                            new Vector2(-310, -800), new Vector2(0, -800), new Vector2(310, -800)};
        } else if (boardSize == 7) {
            tileLocations = new Vector2[] { new Vector2(-405, -380), new Vector2(-135, -380), new Vector2(135, -380),
                                            new Vector2(405, -380), new Vector2(-300, -600), new Vector2(0, -600),
                                            new Vector2(300, -600)};
        } else if (boardSize == 8) {
            tileLocations = new Vector2[] { new Vector2(-300, -335), new Vector2(0, -335), new Vector2(300, -335),
                                            new Vector2(-300, -495), new Vector2(0, -495), new Vector2(300, -495),
                                            new Vector2(-300, -660), new Vector2(0, -660), new Vector2(300, -660)};
        } else if (boardSize == 9) {
            tileLocations = new Vector2[] { new Vector2(-405, -335), new Vector2(-135, -335), new Vector2(135, -335), new Vector2(405, -335),
                                            new Vector2(-405, -495), new Vector2(-135, -495), new Vector2(135, -495), new Vector2(405, -495),
                                            new Vector2(-405, -660), new Vector2(-135, -660), new Vector2(135, -660), new Vector2(405, -660)};
        } else {
            tileLocations = new Vector2[] { new Vector2(-300, -523), new Vector2(0, -523), new Vector2(300, -523),
                                            new Vector2(-300, -660), new Vector2(0, -660), new Vector2(300, -660),
                                            new Vector2(-300, -800), new Vector2(0, -800), new Vector2(300, -800)};
        }

        return tileLocations;
    }

    public GameObject GetTileWithCode(char code) {
        foreach (GameObject tile in tiles) {
            if (tile.GetComponent<Tile>().tileCode == code)
            return tile;
        }
        return null;
    }

    public void UpdateHintsLabel() {
        hintsLabel.GetComponent<Text>().text = "x " + GameManager.Instance.hintsRemaining.ToString();
    }
}
