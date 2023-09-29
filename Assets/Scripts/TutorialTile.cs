using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTile : MonoBehaviour {
    
    public TileCell[] tileCells;
    public int tileColor;
    private Vector2 startingPosition;
    private Vector2 highlightedPosition;

    public int rotations = 0;
    public bool reflected = false;

    private void Start() {
        startingPosition = gameObject.GetComponent<RectTransform>().anchoredPosition;
        GetTileCells();
        SetColor();
    }

    public RectTransform[] GetClosestCellsArray() {
        
        RectTransform[] closestGridCells = new RectTransform[transform.childCount];

        // Loop through all the grid cells touching every tile cell in the tile and determine which are closest,
        // then add the closest ones to the closestGridCells array
        for (int i = 0; i < tileCells.Length; i++) {
            // First check that the tile is touching at least one grid cell to prevent index out of bounds error
            if (tileCells[i].GetTouchingCells().Count == 0)
                return null;
            
            // Get all grid cells touching the tile cell
            List<RectTransform> touchingCells = tileCells[i].GetTouchingCells();
            var currentCell = touchingCells[0];
            var distance = Vector2.Distance(tileCells[i].transform.position, touchingCells[0].position);

            // Loop through all the grid cells touching the tile cell and find the closest one
            foreach (RectTransform cell in touchingCells) {
                if (Vector2.Distance(tileCells[i].transform.position, cell.position) < distance) {
                    currentCell = cell;
                    distance = Vector2.Distance(tileCells[i].transform.position, cell.position);
                }
            }
            
            // Add this closest grid cell to the closestGridCells array
            closestGridCells[i] = currentCell;
            
            //Debug.Log(currentCell.GetComponent<GridCell>().xIndex + ", " + currentCell.GetComponent<GridCell>().yIndex);
        }

        return closestGridCells;
    }

    public void GetTileCells() {

        tileCells = new TileCell[transform.childCount];

        // Loop through the tile's children and get each cell component, then add to tileCells list
        for (int i = 0; i < transform.childCount; i++) {
            tileCells[i] = gameObject.transform.GetChild(i).gameObject.GetComponent<TileCell>();
        }
    }

    private void SetColor() {
        Image[] tileImages = gameObject.GetComponentsInChildren<Image>();

        if (tileColor == 1) {
            for (int i = 0; i < tileImages.Length; i++) {
                if (tileImages[i].sprite != null) {
                    tileImages[i].sprite = Resources.Load<Sprite>("Sprites/Tile Red Hover");
                } else {
                    tileImages[i].color = new Color(203f/255f,62f/255f,53f/255f);
                }
            }
        } else if (tileColor == 2) {
            for (int i = 0; i < tileImages.Length; i++) {
                if (tileImages[i].sprite != null) {
                    tileImages[i].sprite = Resources.Load<Sprite>("Sprites/Tile Blue Hover");
                } else {
                    tileImages[i].color = new Color(25f/255f,73f/255f,77f/255f);
                }
            }
        } else {
            for (int i = 0; i < tileImages.Length; i++) {
                if (tileImages[i].sprite != null) {
                    tileImages[i].sprite = Resources.Load<Sprite>("Sprites/Tile Yellow Hover");
                } else {
                    tileImages[i].color = new Color(101f/255f,137f/255f,62f/255f);
                }
            }
        }
    }

    public void CancelPlacement() {
        gameObject.GetComponent<RectTransform>().anchoredPosition = startingPosition;
    }

    public void ResetTile() {

        // Reset the tile transform back to its default state
        transform.rotation = Quaternion.identity;
        transform.localScale = new Vector2(0.8f, 0.8f);
        rotations = 0;

        // Reset the tile cells back to their default state
        for (int i = 0; i < tileCells.Length; i++) {
            tileCells[i].transform.localScale = new Vector2(1, 1);
            tileCells[i].transform.rotation = Quaternion.identity;

            tileCells[i].PopulateOffsetValues();
        }
    }

    public void Rotate() {

        // Rotate the Tile transform
        if (rotations < 3) {
            gameObject.transform.eulerAngles = new Vector3(
            gameObject.transform.eulerAngles.x,
            gameObject.transform.eulerAngles.y,
            gameObject.transform.eulerAngles.z + 90
            );

            for (int i = 0; i < tileCells.Length; i++) {
                        
                // Rotate the tile cell transforms
                if (reflected)
                tileCells[i].GetComponent<RectTransform>().Rotate(0, 0, -270);
                else
                tileCells[i].GetComponent<RectTransform>().Rotate(0, 0, -90);

                // Rotate the tile cell offset values
                float oldX = tileCells[i].xOffset;
                float oldY = tileCells[i].yOffset;

                tileCells[i].xOffset = -oldY;
                tileCells[i].yOffset = oldX;
            }

            rotations += 1;
        } else {
                    
            rotations = 0;

            // Reset the tile back to its default state
            ResetTile();

            // If the tile was reflected before reset, reflect it back
            if (reflected) {
                // Reflect the transform
                transform.localScale = new Vector2(-1, 1);
            
                // Reflect the tile cells' offset locations
                for (int j = 0; j < tileCells.Length; j++) {
                    tileCells[j].xOffset = -tileCells[j].xOffset;
                }
            }
        }
    
        // Update the sorting order of each tile cell
        UpdateSortingOrder();
    }

    public void Reflect() {

        // Reset the tile back to its default state
        ResetTile();

        // If the tile isn't reflected, reflect it
        if (!reflected) {
            // Reflect the transform
            float tileXScale = Mathf.Abs(transform.localScale.x);
            float tileYScale = transform.localScale.y;
            transform.localScale = new Vector2(-tileXScale, tileYScale);
        
            // Reflect the tile cells' offset locations
            for (int j = 0; j < tileCells.Length; j++) 
                tileCells[j].xOffset = -tileCells[j].xOffset;
        }

        // Update reflected property
        reflected = !reflected;

        // Update the sorting order of each tile cell
        UpdateSortingOrder();
    }

    private void UpdateSortingOrder() {

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
}
