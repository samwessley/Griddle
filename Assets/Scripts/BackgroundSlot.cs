using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BackgroundSlot : MonoBehaviour, IDropHandler {
    
    public void OnDrop(PointerEventData eventData) {

        // Check that the pointer is over a tile
        if (eventData.pointerDrag.GetComponent<Tile>() != null) {
            eventData.pointerDrag.GetComponent<Tile>().CancelPlacement();
        }
    }
}
