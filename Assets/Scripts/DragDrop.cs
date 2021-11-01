using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler {

    [SerializeField] private Canvas canvas;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private void Awake() {
        canvas = this.gameObject.transform.parent.gameObject.GetComponent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnPointerDown(PointerEventData eventData) {

        // Reset the occupied values of the grid cells under this tile to false
        RectTransform[] closestGridCells = gameObject.GetComponent<Tile>().GetClosestCellsArray();
        if (closestGridCells != null) {
            foreach(RectTransform gridcell in closestGridCells) {
                gridcell.gameObject.GetComponent<GridCell>().isOccupied = false;
            }
        }

        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y + 200);
        transform.localScale = new Vector3(1.2f, 1.2f, 1);

        TileCell[] tileCells = transform.gameObject.GetComponentsInChildren<TileCell>();
        foreach (TileCell tileCell in tileCells) {
            tileCell.SetSortingLayer(tileCell.sortingOrder);
        }
    }

    public void OnDrag(PointerEventData eventData) {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnPointerUp(PointerEventData eventData) {

        // Reset the scale back to normal
        transform.localScale = new Vector3(1, 1, 1);

        Tile tile = transform.gameObject.GetComponent<Tile>();

        // First check that the tile is touching the grid. If not, cancel placement and return
        if (tile.GetClosestCellsArray() == null) {
            tile.CancelPlacement();
            return;
        }

        RectTransform[] closestGridCells = tile.GetClosestCellsArray();

        // Now check if the tile is covering any barrier or occupied cells, and if so, cancel placement
        if (closestGridCells != null) {
            if (TilePlacedOverOccupiedOrBarrierGridCells(closestGridCells)) {
                tile.CancelPlacement();
                return;
            }
        } else {
            tile.CancelPlacement();
            return;
        }
        
        // Get the first closest cell and use its distance from its tile to determine how much to adjust the position
        RectTransform gridCellTransform = closestGridCells[0];
        TileCell tileCellTransform = transform.GetChild(0).gameObject.GetComponent<TileCell>();
        Vector2 adjustmentDistance = tile.GetComponent<RectTransform>().anchoredPosition - gridCellTransform.anchoredPosition;

        // Adjust the tile location by the calculated adjustment distance + the tile cell's offset values
        tile.gameObject.GetComponent<RectTransform>().anchoredPosition -= adjustmentDistance + new Vector2(tileCellTransform.xOffset, tileCellTransform.yOffset);

        // Set the sorting layer of the tile to the appropriate level based on where it is on the board
        int minSortingOrder = 999;

        // Loop through all the closest grid cells to the tile to find the minimum sorting layer under this tile
        foreach(Transform cell in closestGridCells) {
            if (cell.GetComponent<Canvas>().sortingOrder < minSortingOrder)
                minSortingOrder = cell.GetComponent<Canvas>().sortingOrder;
        }

        // Set each tileCell's sorting layer to this minimum sorting layer plus its sorting layer inside its tile
        TileCell[] tileCells = tile.GetComponentsInChildren<TileCell>();
        foreach (TileCell tileCell in tileCells) {
            tileCell.SetSortingLayer(minSortingOrder + (tileCell.sortingOrder - 14) + 1);
        }

        // Set each grid cell under this tile to 'occupied'
        foreach(RectTransform gridcell in closestGridCells) {
            gridcell.gameObject.GetComponent<GridCell>().isOccupied = true;
        }
    }

    private bool TilePlacedOverOccupiedOrBarrierGridCells(Transform[] closestCells) {
        for (int i = 0; i < closestCells.Length; i++) {
            if (closestCells[i].GetComponent<GridCell>().isOccupied || closestCells[i].GetComponent<GridCell>().isBarrier) {
                return true;
            }
        }
        return false;
    }
}