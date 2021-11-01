using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler {

    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private GameObject tilePopupTray;
    private RectTransform rectTransform;

    private bool isOnBoard = false;
    private bool isHighlighted = false;

    private void Awake() {
        canvas = this.gameObject.transform.parent.gameObject.GetComponent<Canvas>();
        tilePopupTray = canvas.transform.Find("Tile Popup Tray").gameObject;
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        transform.localScale = new Vector3(0.6f, 0.6f, 1);
    }

    public void OnPointerDown(PointerEventData eventData) {

        // If the tile is on the board when tapped, set it to 'hovering' status
        if (isOnBoard) {
            // Reset the occupied values of the grid cells under this tile to false
            RectTransform[] closestGridCells = gameObject.GetComponent<Tile>().GetClosestCellsArray();
            if (closestGridCells != null) {
                foreach(RectTransform gridcell in closestGridCells)
                gridcell.gameObject.GetComponent<GridCell>().isOccupied = false;
            }

            // Change the position and size
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y + 200);
            transform.localScale = new Vector3(1.2f, 1.2f, 1);

            // Set the sorting order of the tile to its highlighted value
            TileCell[] tileCells = transform.gameObject.GetComponentsInChildren<TileCell>();
            foreach (TileCell tileCell in tileCells)
            tileCell.SetSortingLayer(tileCell.sortingOrder + 5);
        }
        // If it's not on the board when tapped but it is highlighted, put it in 'hovering' status
        else {
            if (isHighlighted) 
            transform.localScale = new Vector3(1.2f, 1.2f, 1);
        }
    }

    public void OnDrag(PointerEventData eventData) {
        // We only want to drag the tile if it was on the board or highlighted when tapped
        if (isOnBoard || isHighlighted)
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnPointerUp(PointerEventData eventData) {

        // If the tile isn't currently on the board, show the tilePopupTray when tapping on the tile
        if (!isOnBoard) {
           tilePopupTray.SetActive(true); 

            if (!isHighlighted) {
                // Set tile size and position to 'highlighted' status
                transform.localScale = new Vector3(1, 1, 1);
                rectTransform.anchoredPosition = tilePopupTray.GetComponent<RectTransform>().anchoredPosition;
                isHighlighted = true;

                // Set the sorting order of the tile to its highlighted value
                TileCell[] cells = transform.gameObject.GetComponentsInChildren<TileCell>();
                foreach (TileCell cell in cells)
                cell.SetSortingLayer(cell.sortingOrder + 5);

                // Stop at this point. We need another tap on the tile to continue further
                return;
            }
        }
        // If tile was tapped and it's not on the board, we'll highlight it. Enable tilePopupTray
        else tilePopupTray.SetActive(true); 

        // If we got to this point, the tile was highlighted or already on the board before being
        // tapped. Set isOnBoard to true before testing if it's in a valid board space.
        isOnBoard = true;

        // Reset the scale back to normal
        transform.localScale = new Vector3(1, 1, 1);

        Tile tile = transform.gameObject.GetComponent<Tile>();

        // First check that the tile is touching the grid. If not, cancel placement and return
        if (tile.GetClosestCellsArray() == null) {
            CancelPlacement(tile);
            return;
        }

        RectTransform[] closestGridCells = tile.GetClosestCellsArray();

        // Now check if the tile is covering any barrier or occupied cells, and if so, cancel placement
        if (closestGridCells != null) {
            if (TilePlacedOverOccupiedOrBarrierGridCells(closestGridCells)) {
                CancelPlacement(tile);
                return;
            }
        } else {
            CancelPlacement(tile);
            return;
        }
        
        // Get the first closest cell and use its distance from its tile to determine how much to adjust the position
        RectTransform gridCellTransform = closestGridCells[0];
        TileCell tileCellTransform = transform.GetChild(0).gameObject.GetComponent<TileCell>();
        Vector2 adjustmentDistance = tile.GetComponent<RectTransform>().anchoredPosition - gridCellTransform.anchoredPosition;

        // Adjust the tile location by the calculated adjustment distance + the tile cell's offset values
        tile.gameObject.GetComponent<RectTransform>().anchoredPosition -= adjustmentDistance + new Vector2(tileCellTransform.xOffset, tileCellTransform.yOffset);
        isOnBoard = true;
        tilePopupTray.SetActive(false);

        // Set the sorting layer of the tile to the appropriate level based on where it is on the board
        int minSortingOrder = 999;

        // Loop through all the closest grid cells to the tile to find the minimum sorting layer under this tile
        foreach(Transform cell in closestGridCells)
        if (cell.GetComponent<Canvas>().sortingOrder < minSortingOrder)
        minSortingOrder = cell.GetComponent<Canvas>().sortingOrder;

        // Set each tileCell's sorting layer to this minimum sorting layer plus its sorting layer inside its tile
        TileCell[] tileCells = tile.GetComponentsInChildren<TileCell>();
        foreach (TileCell tileCell in tileCells)
        tileCell.SetSortingLayer(minSortingOrder + (tileCell.sortingOrder - 14) + 1);

        // Set each grid cell under this tile to 'occupied'
        foreach(RectTransform gridcell in closestGridCells)
        gridcell.gameObject.GetComponent<GridCell>().isOccupied = true;
    }

    private bool TilePlacedOverOccupiedOrBarrierGridCells(Transform[] closestCells) {
        for (int i = 0; i < closestCells.Length; i++) {
            if (closestCells[i].GetComponent<GridCell>().isOccupied || closestCells[i].GetComponent<GridCell>().isBarrier) {
                return true;
            }
        }
        return false;
    }

    private void CancelPlacement(Tile tile) {
        tile.CancelPlacement();
        isOnBoard = false;
        transform.localScale = new Vector3(0.6f, 0.6f, 1);
        tilePopupTray.SetActive(false);
        isHighlighted = false;
        isOnBoard = false;

        // Set the sorting order of the tile to its highlighted value
        TileCell[] cells = transform.gameObject.GetComponentsInChildren<TileCell>();
        foreach (TileCell cell in cells) {
            cell.SetSortingLayer(cell.sortingOrder);
        }
    }
}