using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PageSwiper : MonoBehaviour, IDragHandler, IEndDragHandler {

    [SerializeField] GameObject levelController = null;
    [SerializeField] GameObject pageIndicator1 = null;
    [SerializeField] GameObject pageIndicator2 = null;
    [SerializeField] GameObject pageIndicator3 = null;
    [SerializeField] GameObject pageIndicator4 = null;
    [SerializeField] GameObject pageIndicator5 = null;

    private int page = 1;
    
    private enum DraggedDirection {
        Right,
        Left
    }
    
    public void OnDrag(PointerEventData eventData) {

    }

    public void OnEndDrag(PointerEventData eventData) {
        Vector3 dragVectorDirection = (eventData.position - eventData.pressPosition).normalized;

        if (GetDragDirection(dragVectorDirection) == DraggedDirection.Left) {
            if (page < 5) {
                levelController.GetComponent<LevelController>().LoadNextLevelPage();
                page += 1;
                UpdatePageIndicator();
            }
        } else {
            if (page > 1) {
                levelController.GetComponent<LevelController>().LoadPreviousLevelPage(); 
                page -= 1;
                UpdatePageIndicator();
            }
        }
    }

    private DraggedDirection GetDragDirection(Vector3 dragVector) {
        DraggedDirection draggedDir = (dragVector.x > 0) ? DraggedDirection.Right : DraggedDirection.Left;
        return draggedDir;
    }

    private void UpdatePageIndicator() {
        pageIndicator1.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Page Button");
        pageIndicator2.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Page Button");
        pageIndicator3.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Page Button");
        pageIndicator4.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Page Button");
        pageIndicator5.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Page Button");

        switch (page) {
            case 1:
                pageIndicator1.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Page Button Current");
                break;
            case 2:
                pageIndicator2.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Page Button Current");
                break;
            case 3:
                pageIndicator3.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Page Button Current");
                break;
            case 4:
                pageIndicator4.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Page Button Current");
                break;            
            case 5:
                pageIndicator5.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Page Button Current");
                break;
        }
    }
}
