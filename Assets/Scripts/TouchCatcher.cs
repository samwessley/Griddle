using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchCatcher : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    [SerializeField] GameObject gameController = null;
    [SerializeField] GameObject tileTray = null;

    public void OnPointerDown(PointerEventData eventData) {
    }

    public void OnPointerUp(PointerEventData eventData) {

        tileTray.SetActive(false);
        GameObject activeTile = gameController.GetComponent<GameController>().activeTile;
        activeTile.GetComponent<DragDrop>().CancelPlacement(activeTile.GetComponent<Tile>());
    }
}
