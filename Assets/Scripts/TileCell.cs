using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCell : MonoBehaviour {

    public float xOffset;
    public float yOffset;

    private void Awake() {
        PopulateOffsetValues();
    }

    public List<RectTransform> GetTouchingCells() {
        List<RectTransform> touchingCells = new List<RectTransform>();
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.zero);

        foreach (RaycastHit2D hit in hits) {
            if (hit.collider != null && hit.collider.gameObject.GetComponent<GridCell>() != null) {
                touchingCells.Add(hit.collider.gameObject.GetComponent<RectTransform>());
            }
        }

        return touchingCells;
    }

    public void SetSortingLayer(int layer) {
        Canvas canvas = gameObject.GetComponent<Canvas>();
        canvas.sortingOrder = layer;
    }

    private void PopulateOffsetValues() {
        xOffset = gameObject.GetComponent<RectTransform>().anchoredPosition.x;
        yOffset = gameObject.GetComponent<RectTransform>().anchoredPosition.y;
    }
}
