using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    [SerializeField] GameObject levelNumber = null;
    [SerializeField] GameObject canvas = null;
    [SerializeField] GameObject levelCompletePopup = null;
    [SerializeField] GameObject nextLevelButton = null;

    [SerializeField] GameObject star1 = null;
    [SerializeField] GameObject star2 = null;
    [SerializeField] GameObject star3 = null;
    [SerializeField] GameObject message = null;

    private GridCell[,] cellGrid = new GridCell[12,12];
    private int boardSize;
    private int numberOfTiles;
    private GameObject[] tiles;

    public GameObject activeTile;
    public int tilesRemaining;
    public int starsCollected = 0;

    private void Start() {
        PopulateCellGrid();
        LevelSetup();
        GameManager.Instance.SaveAsJSON();
    }

    public void LevelSetup() {
        LoadLevelData(GameManager.Instance.currentLevel);
        LoadTileData(GameManager.Instance.currentLevel);
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

    public void CheckForLevelComplete() {
        if (tilesRemaining == 0 && CheckForValidTilePlacement()) {
            StartCoroutine(LevelCompleteAnimation());

            GameManager.Instance.stars[GameManager.Instance.currentLevel - 1] = starsCollected;

            if (GameManager.Instance.levelsUnlocked < GameManager.Instance.totalLevels)
            GameManager.Instance.levelsUnlocked += 1;

            GameManager.Instance.SaveAsJSON();
        }
    }

    IEnumerator LevelCompleteAnimation() {

        SetStarsAndMessage();

        LeanTween.scale(star1, Vector2.zero, 0);
        LeanTween.scale(star2, Vector2.zero, 0);
        LeanTween.scale(star3, Vector2.zero, 0);

        LeanTween.scale(message, Vector2.zero, 0);
        LeanTween.rotateZ(message, -25f,0);

        yield return new WaitForSeconds(0.2f);

        levelCompletePopup.SetActive(true);

        yield return new WaitForSeconds(0.1f);
        LeanTween.scale(star1, new Vector2 (1,1), 0.07f);
        yield return new WaitForSeconds(0.07f);
        LeanTween.scale(star2, new Vector2 (1,1), 0.07f);
        yield return new WaitForSeconds(0.07f);
        LeanTween.scale(star3, new Vector2 (1,1), 0.07f);

        yield return new WaitForSeconds(0.25f);

        LeanTween.scale(message, new Vector2(1,1), 0.08f);
        LeanTween.rotateZ(message,0,0.08f);
    }

    private void SetStarsAndMessage() {
        if (starsCollected > 0) {
            star1.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Star");
            RectTransform star_rt = star1.GetComponent<RectTransform>();
            star_rt.sizeDelta = new Vector2 (595, 597);

            message.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Nice!");
            RectTransform message_rt = message.GetComponent<RectTransform>();
            message_rt.sizeDelta = new Vector2 (353, 134);
        }
        if (starsCollected > 1) {
            star2.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Star");
            RectTransform star_rt = star2.GetComponent<RectTransform>();
            star_rt.sizeDelta = new Vector2 (595, 597);

            message.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Great!");
            RectTransform message_rt = message.GetComponent<RectTransform>();
            message_rt.sizeDelta = new Vector2 (454, 143);
        }
        if (starsCollected > 2) {
            star3.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Star");
            RectTransform star_rt = star3.GetComponent<RectTransform>();
            star_rt.sizeDelta = new Vector2 (595, 597);

            message.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Perfect!");
            RectTransform message_rt = message.GetComponent<RectTransform>();
            message_rt.sizeDelta = new Vector2 (585, 159);
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

    private void LoadTileData(int level) {

        numberOfTiles = GameManager.Instance.levelTiles[level - 1].Length;
        tilesRemaining = numberOfTiles;

        tiles = new GameObject[numberOfTiles];
        //float xLocation = -300;
        Vector2[] tileLocations = new Vector2[] { new Vector2(-300, -560), new Vector2(0, -560), new Vector2(300, -560),
                                                new Vector2(-300, -780), new Vector2(0, -780), new Vector2(300, -780)};

        for (int i = 0; i < tiles.Length; i++) {
            string tileName = GameManager.Instance.levelTiles[level - 1][i];

            tiles[i] = Instantiate(Resources.Load<GameObject>("Prefabs/Tiles/" + tileName));
            tiles[i].transform.SetParent(canvas.transform);
            tiles[i].GetComponent<RectTransform>().anchoredPosition = tileLocations[i];
            //xLocation += 300;
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

    private bool CheckForValidTilePlacement() {

        for (int i = 0; i < tiles.Length; i++) {
            Tile tile = tiles[i].GetComponent<Tile>();
            RectTransform[] closestGridCellTransforms = tile.GetClosestCellsArray();
            GridCell[] closestGridCells = new GridCell[closestGridCellTransforms.Length];

            for (int j = 0; j < closestGridCellTransforms.Length; j++) {
                closestGridCells[j] = closestGridCellTransforms[j].gameObject.GetComponent<GridCell>();
            }

            GetStarsCollected(closestGridCells);

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
                if (cell.isOccupied) {
                    return false;
                }
            }
            
            // Loop through corner-adjacent cells and make sure at least one is occupied
            bool oneCellIsValid = false;
            foreach (GridCell cell in cornerCellsToTest) {
                if (cell.isOccupied)
                    oneCellIsValid = true;
            }
            if (!oneCellIsValid)
                return false;
        }

        // If we get to this point, none of the tiles have any issues. This is a valid placement of tiles
        return true;
    }

    private List<GridCell> GetSideAdjacentCells(GridCell cell, GridCell[] closestGridCells) {
        
        List<GridCell> sideCellsToTest = new List<GridCell>();

        // Gather all the side adjacent cells for this grid cell
        if (cell.xIndex - 1 >= 0)
            sideCellsToTest.Add(cellGrid[cell.xIndex - 1, cell.yIndex]);
        if (cell.xIndex + 1 <= 11)
            sideCellsToTest.Add(cellGrid[cell.xIndex + 1, cell.yIndex]);
        if (cell.yIndex - 1 >= 0)
            sideCellsToTest.Add(cellGrid[cell.xIndex, cell.yIndex - 1]);
        if (cell.yIndex + 1 <= 11)
            sideCellsToTest.Add(cellGrid[cell.xIndex, cell.yIndex + 1]);

        return sideCellsToTest;
    }

    private List<GridCell> GetCornerAdjacentCells(GridCell cell, GridCell[] closestGridCells) {

        List<GridCell> cornerCellsToTest = new List<GridCell>();

        // Gather all the corner adjacent cells for this grid cell
        if (cell.xIndex - 1 >= 0 && cell.yIndex - 1 >= 0)
            cornerCellsToTest.Add(cellGrid[cell.xIndex - 1, cell.yIndex - 1]);
        if (cell.xIndex + 1 <= 11 && cell.yIndex - 1 >= 0)
            cornerCellsToTest.Add(cellGrid[cell.xIndex + 1, cell.yIndex - 1]);
        if (cell.xIndex - 1 >= 0 && cell.yIndex + 1 <= 11)
            cornerCellsToTest.Add(cellGrid[cell.xIndex - 1, cell.yIndex + 1]);
        if (cell.xIndex + 1 <= 11 && cell.yIndex + 1 <= 11)
            cornerCellsToTest.Add(cellGrid[cell.xIndex + 1, cell.yIndex + 1]);

        return cornerCellsToTest;
    }

    private void GetStarsCollected(GridCell[] closestGridCells) {
        foreach (GridCell cell in closestGridCells) {
            if (cell.isStar)
            starsCollected += 1;
        }
    }
}
