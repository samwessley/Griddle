using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public void PopulateOffsetValues() {
        float scale;

        if (GameManager.Instance.currentLevel <= 30) {
            // 5x5 level
            scale = GameManager.Instance.tileScaleFactors[0];
        } else if (GameManager.Instance.currentLevel > 30 && GameManager.Instance.currentLevel <= 60) {
            // 6x6 level
            scale = GameManager.Instance.tileScaleFactors[1];
        } else if (GameManager.Instance.currentLevel > 60 && GameManager.Instance.currentLevel <= 90) {
            // 7x7 level
            scale = GameManager.Instance.tileScaleFactors[2];
        } else if (GameManager.Instance.currentLevel > 90 && GameManager.Instance.currentLevel <= 120) {
            // 8x8 level
            scale = GameManager.Instance.tileScaleFactors[3];
        } else {
            // 9x9 level
            scale = GameManager.Instance.tileScaleFactors[4];
        }

        xOffset = gameObject.GetComponent<RectTransform>().anchoredPosition.x * scale;
        //Debug.Log(xOffset);
        yOffset = gameObject.GetComponent<RectTransform>().anchoredPosition.y * scale;
        //Debug.Log(yOffset);
    }
}
