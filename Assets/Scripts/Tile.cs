using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    
    private TileCell[] tileCells;
    private Vector2 startingPosition;
    private Vector2 highlightedPosition;

    private void Awake() {
        startingPosition = transform.position;
        GetTileCells();
    }

    public Transform[] GetClosestCellsArray() {
        
        Transform[] closestGridCells = new Transform[transform.childCount];

        // Loop through all the grid cells touching every tile cell in the tile and determine which are closest,
        // then add the closest ones to the closestGridCells array
        for (int i = 0; i < tileCells.Length; i++) {
            // Get all grid cells touching the tile cell
            List<Transform> touchingCells = tileCells[i].GetTouchingCells();
            Debug.Log(touchingCells.Count);
            var currentCell = touchingCells[0];
            var distance = Vector2.Distance(tileCells[i].transform.position, touchingCells[0].position);

            // Loop through all the grid cells touching the tile cell and find the closest one
            foreach (Transform cell in touchingCells) {
                if (Vector2.Distance(tileCells[i].transform.position, cell.position) < distance) {
                    currentCell = cell;
                    distance = Vector2.Distance(tileCells[i].transform.position, cell.position);
                }
            }
            
            // Add this closest grid cell to the closestGridCells array
            closestGridCells[i] = currentCell;
            Debug.Log(currentCell.GetComponent<GridCell>().xIndex + ", " + currentCell.GetComponent<GridCell>().yIndex);
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

    public void CancelPlacement() {
        transform.position = startingPosition;
    }
}
