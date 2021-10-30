using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileSlot : MonoBehaviour, IDropHandler {

    public GridCell cell;

    private void Awake() {
        cell = GetComponent<GridCell>();
    }

    public void OnDrop(PointerEventData eventData) {
        
        Tile tile = eventData.pointerDrag.GetComponent<Tile>();

        // If the pointer is over a tile, drop the tile into place
        if (eventData.pointerDrag != null) {

            RaycastHit2D hit = Physics2D.Raycast(CurrentTouchPosition, Vector2.zero);
            GameObject tileCell;

            // Get the tile cell under the pointer and set the tile's position to the grid cell's position
            // minus the offset from the tile cell
            if (hit.collider != null && hit.collider.gameObject.GetComponent<TileCell>() != null) {

                tileCell = hit.collider.gameObject;
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition - tileCell.GetComponent<RectTransform>().anchoredPosition;
            
                Transform[] closestGridCells = tile.GetClosestCellsArray();

                if (closestGridCells != null) {
                    if (TileIsCoveringOccupiedCells(closestGridCells)) {
                        tile.CancelPlacement();
                    }
                } else {
                    Debug.Log("hey");
                    tile.CancelPlacement();
                }
            } else {
                tile.CancelPlacement();
            }
        }
    }

    private bool TileIsCoveringOccupiedCells(Transform[] closestCells) {
        for (int i = 0; i < closestCells.Length; i++) {
            if (closestCells[i].GetComponent<GridCell>().isOccupied) {
                //CancelPlacement();
                return true;
            }
        }
        return false;
    }

    Vector2 CurrentTouchPosition {
        get {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}
