using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class GameController : MonoBehaviour {

    [SerializeField] GameObject levelNumber = null;
    [SerializeField] GameObject canvas = null;
    [SerializeField] GameObject hintsLabel = null;
    [SerializeField] GameObject skipsLabel = null;
    [SerializeField] GameObject moreHints = null;
    [SerializeField] GameObject moreSkips = null;

    [SerializeField] GameObject levelCompletePopup = null;
    [SerializeField] GameObject message = null;
    [SerializeField] GameObject brillianceModal = null;
    [SerializeField] GameObject brillianceScoreContainer = null;
    [SerializeField] GameObject brillianceScore = null;
    [SerializeField] GameObject nextLevelButton = null;

    public GridCell[,] cellGrid;
    public GameObject[] gridCells;
    private int numberOfTiles;
    public GameObject[] tiles;

    public int boardSize;
    public string distinctChars;
    public GameObject activeTile;
    public int tilesRemaining;

    public string distinctCharsRemaining;

    private void Awake() {
        GatherGridCells();
        PopulateCellGrid();
        LevelSetup();
        GameManager.Instance.SaveAsJSON();
        UpdateHintsLabel();
        UpdateSkipsLabel();
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
                if (GameManager.Instance.levelsCompleted_5x5 < GameManager.Instance.totalLevels)
                GameManager.Instance.levelsCompleted_5x5 += 1;
            } else if (GameManager.Instance.currentLevelPack == 1) {
                if (GameManager.Instance.levelsCompleted_6x6 < GameManager.Instance.totalLevels)
                GameManager.Instance.levelsCompleted_6x6 += 1;
            } else if (GameManager.Instance.currentLevelPack == 2) {
                if (GameManager.Instance.levelsCompleted_7x7 < GameManager.Instance.totalLevels)
                GameManager.Instance.levelsCompleted_7x7 += 1;
            } else if (GameManager.Instance.currentLevelPack == 3) {
                if (GameManager.Instance.levelsCompleted_8x8 < GameManager.Instance.totalLevels)
                GameManager.Instance.levelsCompleted_8x8 += 1;
            } else if (GameManager.Instance.currentLevelPack == 4) {
                if (GameManager.Instance.levelsCompleted_9x9 < GameManager.Instance.totalLevels)
                GameManager.Instance.levelsCompleted_9x9 += 1;
            }

            GameManager.Instance.SaveAsJSON();

            // Disable movement for all tiles on board
            foreach(GameObject tile in tiles) {
                tile.GetComponent<DragDrop>().enabled = false;
            }

            // Play level complete sound
            if (GameManager.Instance.soundsOn)
            SoundEngine.Instance.PlaySuccessSound();
        }
    }

    IEnumerator LevelCompleteAnimation() {

        SetMessage();

        // Hide message, next level button and brilliance modal
        LeanTween.scale(message, Vector2.zero, 0);
        LeanTween.scale(nextLevelButton, Vector2.zero, 0);
        LeanTween.scale(brillianceModal, Vector2.zero, 0);

        yield return new WaitForSeconds(0.25f);

        // Show the level complete screen
        levelCompletePopup.SetActive(true);

        // Calculate the brilliance score
        brillianceScore.GetComponent<Text>().text = (10*GameManager.Instance.levelsCompleted_5x5 + 30*GameManager.Instance.levelsCompleted_6x6 + 60*GameManager.Instance.levelsCompleted_7x7 + 90*GameManager.Instance.levelsCompleted_8x8).ToString("n0");
        LayoutRebuilder.ForceRebuildLayoutImmediate(brillianceScoreContainer.GetComponent<RectTransform>());

        // Show the message
        LeanTween.scale(message, new Vector2(1,1), 0.12f).setEase(LeanTweenType.easeInOutBounce);
        LeanTween.rotateZ(message,5f,0.12f);
        yield return new WaitForSeconds(1f);

        // Hide the message
        LeanTween.scale(message, new Vector2(0,0), 0.05f);
        yield return new WaitForSeconds(0.15f);

        // Show the brilliance modal
        LeanTween.scale(brillianceModal, new Vector2(1,1), 0.25f).setEase(LeanTweenType.easeInOutBounce);
        LeanTween.moveY(brillianceModal, 0.25f, 0.1f);
        AddBrillianceScore();

        yield return new WaitForSeconds(1.8f);

        // Show the next level button
        LeanTween.scale(nextLevelButton, new Vector2(1,1), 0.05f).setEase(LeanTweenType.easeInOutBounce);

        // Vibrate
        if (GameManager.Instance.hapticsOn) {
            Vibration.Init();
            #if UNITY_IOS
            Vibration.VibrateIOS(ImpactFeedbackStyle.Medium);
            #endif
            #if UNITY_ANDROID
            Vibration.Vibrate(100);
            #endif
        }
    }

    private void AddBrillianceScore() {

        int newScore = 10*GameManager.Instance.levelsCompleted_5x5 + 30*GameManager.Instance.levelsCompleted_6x6 + 60*GameManager.Instance.levelsCompleted_7x7 + 90*GameManager.Instance.levelsCompleted_8x8;
        int oldScore = 0;

        // Calculate old and new brilliance score
        if (GameManager.Instance.currentLevelPack == 0) {
            if (GameManager.Instance.levelsCompleted_5x5 < GameManager.Instance.totalLevels) {
                oldScore = newScore - 10;
            } else {
                oldScore = newScore;
            }
        } else if (GameManager.Instance.currentLevelPack == 1) {
            if (GameManager.Instance.levelsCompleted_6x6 < GameManager.Instance.totalLevels) {
                oldScore = newScore - 30;
            } else {
                oldScore = newScore;
            }
        } else if (GameManager.Instance.currentLevelPack == 2) {
            if (GameManager.Instance.levelsCompleted_7x7 < GameManager.Instance.totalLevels) {
                oldScore = newScore - 60;
            } else {
                oldScore = newScore;
            }
        } else if (GameManager.Instance.currentLevelPack == 3) {
            if (GameManager.Instance.levelsCompleted_8x8 < GameManager.Instance.totalLevels) {
                oldScore = newScore - 90;
            } else {
                oldScore = newScore;
            }
        }

        StartCoroutine(AddBrillianceAnimation(oldScore, newScore));
        StartCoroutine(PlayTickerSound());
    }

    IEnumerator AddBrillianceAnimation(int oldScore, int newScore) {
        
        int countFPS = newScore - oldScore;
        int diff = newScore - oldScore;

        while (diff >= 0) {
            brillianceScore.GetComponent<Text>().text = (newScore - diff).ToString("n0");
            yield return new WaitForSeconds(0.8f / countFPS);
            diff -= 1;
            LayoutRebuilder.ForceRebuildLayoutImmediate(brillianceScoreContainer.GetComponent<RectTransform>());
        }
    }

    IEnumerator PlayTickerSound() {

        int countFPS = 10;
        int number = 10;

        while (number >= 0) {
            if (GameManager.Instance.soundsOn)
            SoundEngine.Instance.PlayPopSound();

            yield return new WaitForSeconds(0.8f / countFPS);
            number -= 1;
        }
    }

    private void SetMessage() {

        System.Random random = new System.Random();
        string[] phrases = {"Great!", "Perfect!", "Excellent!", "Fabulous!", "Outstanding!", "Wonderful!", "Sensational!", "Amazing!",
                            "Astonishing!", "Stunning!", "Spectacular!", "Breathtaking!", "Fantastic!", "Incredible!", "Terrific!",
                            "Superb!", "Dazzling!", "Tremendous!", "Awesome!", "Brilliant!", "Impressive!", "Remarkable!", "Cool!",
                            "Exceptional!", "Marvelous!", "Legendary!", "Phenomenal!", "Wondrous!", "Smashing!", "Sweet!", "Extraordinary!",
                            "Magnificent!"};
        int randomInt = random.Next(0,phrases.Length);
        message.GetComponent<Text>().text = phrases[randomInt];
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
                //Debug.Log(tile.tileCode + ", " + text[i]);
                if (tile.tileCode == text[i]) {
                    if ((int)(text[i + 2] - '0') == 1) {
                        tile.Reflect();
                        //Debug.Log("tile reflected");
                    }
                    RotateTileNTimes(tile, (int)(text[i + 1] - '0'));
                    //Debug.Log("tile rotated " + (int)(text[i + 1] - '0') + "times");
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
        
        if (boardSize == 5) {
            tileLocations = new Vector2[] { new Vector2(-310, -370), new Vector2(0, -370), new Vector2(310, -370),
                                            new Vector2(-310, -720), new Vector2(0, -720), new Vector2(310, -720)};
        } else if (boardSize == 6) {
            tileLocations = new Vector2[] { new Vector2(-390, -310), new Vector2(-80, -310), new Vector2(230, -310),
                                            new Vector2(-230, -540), new Vector2(80, -540), new Vector2(390, -540),
                                            new Vector2(-390, -770), new Vector2(-80, -770), new Vector2(230, -770)};
        } else if (boardSize == 7) {
            tileLocations = new Vector2[] { new Vector2(-405, -400), new Vector2(-135, -400), new Vector2(135, -400),
                                            new Vector2(405, -400), new Vector2(-300, -720), new Vector2(0, -720),
                                            new Vector2(300, -720)};
        } else if (boardSize == 8) {
            tileLocations = new Vector2[] { new Vector2(-390, -310), new Vector2(-80, -310), new Vector2(230, -310),
                                            new Vector2(-230, -540), new Vector2(80, -540), new Vector2(390, -540),
                                            new Vector2(-390, -770), new Vector2(-80, -770), new Vector2(230, -770)};
        } else if (boardSize == 9) {
            tileLocations = new Vector2[] { new Vector2(-405, -320), new Vector2(-135, -320), new Vector2(135, -320), new Vector2(405, -320),
                                            new Vector2(-405, -550), new Vector2(-135, -550), new Vector2(135, -550), new Vector2(405, -550),
                                            new Vector2(-405, -780), new Vector2(-135, -780), new Vector2(135, -780), new Vector2(405, -780)};
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

        if (GameManager.Instance.hintsRemaining == 0) {
            moreHints.SetActive(true);
        } else {
            moreHints.SetActive(false);
        }
    }

    public void UpdateSkipsLabel() {
        skipsLabel.GetComponent<Text>().text = "x " + GameManager.Instance.skipsRemaining.ToString();

        if (GameManager.Instance.skipsRemaining == 0) {
            moreSkips.SetActive(true);
        } else {
            moreSkips.SetActive(false);
        }
    }

    private void GatherGridCells() {
        gridCells = GameObject.FindGameObjectsWithTag("GridCell");
    }
}
