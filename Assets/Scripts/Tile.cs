using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour {
    
    public TileCell[] tileCells;
    public int tileColor;
    private Vector2 startingPosition;
    private Vector2 highlightedPosition;
    [SerializeField] char[] tileCodeRange = null;
    public char tileCode;

    public int rotations = 0;
    public bool reflected = false;
    public int lastManipulationType = 0;

    private void Start() {
        startingPosition = gameObject.GetComponent<RectTransform>().anchoredPosition;
        GetTileCells();
        SetColor();
        tileCode = tileCodeRange[tileColor - 1];
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

    private void GetTileCells() {

        tileCells = new TileCell[transform.childCount];

        // Loop through the tile's children and get each cell component, then add to tileCells list
        for (int i = 0; i < transform.childCount; i++) {
            tileCells[i] = gameObject.transform.GetChild(i).gameObject.GetComponent<TileCell>();
        }
    }

    private void SetColor() {
        //tileColor = Random.Range(1,4);
        Image[] tileImages = gameObject.GetComponentsInChildren<Image>();
        if (tileColor == 1) {
            for (int i = 0; i < tileImages.Length; i++) {
                tileImages[i].sprite = Resources.Load<Sprite>("Sprites/Tile Red");
            }
        } else if (tileColor == 2) {
            for (int i = 0; i < tileImages.Length; i++) {
                tileImages[i].sprite = Resources.Load<Sprite>("Sprites/Tile Blue");
            }
        } else {
            for (int i = 0; i < tileImages.Length; i++) {
                tileImages[i].sprite = Resources.Load<Sprite>("Sprites/Tile Yellow");
            }
        }
    }

    public void CancelPlacement() {
        gameObject.GetComponent<RectTransform>().anchoredPosition = startingPosition;
    }
}
