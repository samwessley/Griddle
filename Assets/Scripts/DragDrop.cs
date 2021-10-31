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

        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnPointerDown(PointerEventData eventData) {

        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y + 200);
        transform.localScale = new Vector3(1.2f, 1.2f, 1);

        TileCell[] tileCells = transform.gameObject.GetComponentsInChildren<TileCell>();
        foreach (TileCell tileCell in tileCells) {
            tileCell.SetSortingLayer(14);
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

        Transform[] closestGridCells = tile.GetClosestCellsArray();
        var totalX = 0f;
        var totalY = 0f;
        
        // Get the average/center location of all the grid cells under the tile
        foreach(Transform gridCell in closestGridCells) {
            totalX += gridCell.gameObject.GetComponent<RectTransform>().anchoredPosition.x;
            totalY += gridCell.gameObject.GetComponent<RectTransform>().anchoredPosition.y;
        }
        var centerX = totalX / closestGridCells.Length;
        var centerY = totalY / closestGridCells.Length;

        // Set the tile location to the average location of all the grid cells under it
        tile.GetComponent<RectTransform>().anchoredPosition = new Vector2(centerX, centerY);

        // Set the sorting layer of the tile to the appropriate level based on where it is on the board
        int maxSortingOrder = 1;

        // Loop through all the closest grid cells to the tile to find the maximum sorting layer
        foreach(Transform cell in closestGridCells) {
            if (cell.GetComponent<Canvas>().sortingOrder > maxSortingOrder)
                maxSortingOrder = cell.GetComponent<Canvas>().sortingOrder;
        }

        // Set each tileCell's sorting layer to this maximum sorting layer
        TileCell[] tileCells = tile.GetComponentsInChildren<TileCell>();
        foreach (TileCell tileCell in tileCells) {
            tileCell.SetSortingLayer(maxSortingOrder + 1);
        }

        // Now check if the tile is covering any barrier or occupied cells, and if so, cancel placement
        if (closestGridCells != null) {
            if (TilePlacedOverOccupiedOrBarrierGridCells(closestGridCells)) {
                tile.CancelPlacement();
            }
        } else {
            tile.CancelPlacement();
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
