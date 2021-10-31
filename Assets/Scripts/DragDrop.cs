using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler {

    [SerializeField] private Canvas canvas;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private void Awake() {

        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnPointerDown(PointerEventData eventData) {

        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y + 100);
    }

    public void OnDrag(PointerEventData eventData) {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnPointerUp(PointerEventData eventData) {

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
