using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialDragDrop : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler {

    [SerializeField] GameObject tutorialAnimationController = null;
    [SerializeField] GameObject tutorialController = null;

    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private GameObject touchCatcher;
    private RectTransform rectTransform;

    public bool isOnBoard = false;

    private Vector2 pointerLocation;

    private void Start() {
        canvas = this.gameObject.transform.parent.gameObject.GetComponent<Canvas>();
        touchCatcher = canvas.transform.Find("Touch Catcher").gameObject;
        //tilePopupTray = canvas.transform.Find("Tile Popup Tray").gameObject;
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnPointerDown(PointerEventData eventData) {
        
        // Hide pointer animation
        tutorialAnimationController.GetComponent<TutorialAnimationScript>().pointerActive = false;
        tutorialAnimationController.GetComponent<TutorialAnimationScript>().pointer.SetActive(false);

        pointerLocation = eventData.position;

        // If the tile is on the board when tapped, set it to 'hovering' status
        if (isOnBoard) {

            // Vibrate
            if (GameManager.Instance.hapticsOn) {
                Vibration.Init();
                #if UNITY_IOS
                Vibration.VibrateIOS(ImpactFeedbackStyle.Light);
                #endif
                #if UNITY_ANDROID
                Vibration.Vibrate(50);
                #endif
            }

            // Reset the occupied values of the grid cells under this tile to false
            RectTransform[] closestGridCells = gameObject.GetComponent<Tile>().GetClosestCellsArray();
            if (closestGridCells != null) {
                foreach(RectTransform gridcell in closestGridCells) {
                    gridcell.gameObject.GetComponent<GridCell>().isOccupied = false;
                    gridcell.gameObject.GetComponent<GridCell>().colorOccupying = 0;
                    gridcell.gameObject.GetComponent<GridCell>().charOccupying = (char)0;
                }
            }

            // Change the position and size
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y + 200);
            SetHoveringPositionScale();

            // Set the sorting order of the tile to its highlighted value
            TileCell[] tileCells = transform.gameObject.GetComponentsInChildren<TileCell>();
            foreach (TileCell tileCell in tileCells)
            tileCell.SetSortingLayer(tileCell.GetComponent<Canvas>().sortingOrder + 19);
        }
        // If it's not on the board when tapped but it is highlighted, put it in 'hovering' status
        else {
            // Set the sorting order of the tile to its highlighted value
            TileCell[] tileCells = transform.gameObject.GetComponentsInChildren<TileCell>();
            foreach (TileCell tileCell in tileCells)
            tileCell.SetSortingLayer(tileCell.GetComponent<Canvas>().sortingOrder + 19);

            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y + 250);
            SetHoveringPositionScale();
        }
    }

    public void OnDrag(PointerEventData eventData) {
        // We only want to drag the tile if it was on the board or highlighted when tapped
        Vector2 vector = (eventData.delta / canvas.scaleFactor);
        vector *= 1.1f;
        rectTransform.anchoredPosition += vector;

        //AddShadowToGrid();
    }

    public void OnPointerUp(PointerEventData eventData) {

        TutorialTile tile = transform.gameObject.GetComponent<TutorialTile>();

        //Debug.Log(tile.tileCells[0].xOffset + ", " + tile.tileCells[0].yOffset);

        if (eventData.position == pointerLocation && !isOnBoard)
        CancelPlacement(tile);

        // If we got to this point, the tile was highlighted or already on the board before being
        // tapped. Set isOnBoard to true before testing if it's in a valid board space.
        isOnBoard = true;

        // Reset the scale back to normal
        SetScale(GameManager.Instance.tileScaleFactors[0]);

        // First check that the tile is touching the grid. If not, cancel placement and return
        if (tile.GetClosestCellsArray() == null) {
            CancelPlacement(tile);
            Debug.Log("problem");
            return;
        }

        RectTransform[] closestGridCells = tile.GetClosestCellsArray();
        
        // Get the first closest cell and use its distance from its tile to determine how much to adjust the position
        RectTransform gridCellTransform = closestGridCells[0];
        TileCell tileCellTransform = transform.GetChild(0).gameObject.GetComponent<TileCell>();
        Vector2 adjustmentDistance = tile.GetComponent<RectTransform>().anchoredPosition - gridCellTransform.anchoredPosition;

        // Adjust the tile location by the calculated adjustment distance + the tile cell's offset values
        tile.gameObject.GetComponent<RectTransform>().anchoredPosition -= adjustmentDistance + new Vector2(tileCellTransform.xOffset, tileCellTransform.yOffset);
        touchCatcher.GetComponent<CanvasGroup>().blocksRaycasts = false;

        // Set each tileCell's sorting layer to this minimum sorting layer plus its sorting layer inside its tile
        TileCell[] tileCells = tile.GetComponentsInChildren<TileCell>();
        int minTileCellSortingOrder = 999;
        foreach (TileCell tileCell in tileCells) {
            if (tileCell.GetComponent<Canvas>().sortingOrder < minTileCellSortingOrder)
            minTileCellSortingOrder = tileCell.GetComponent<Canvas>().sortingOrder;
        }

        // Hide highlight and show continue button
        if (SceneManager.GetActiveScene().buildIndex == 12) {
            tutorialAnimationController.GetComponent<TutorialAnimationScript>().TutorialFinishedAnimation();
        } else {
            tutorialAnimationController.GetComponent<TutorialAnimationScript>().AnimateContinueButton();
        }
        
        // Play pop sound
        if (GameManager.Instance.soundsOn)
        SoundEngine.Instance.PlayPopSound();

        // Vibrate
        if (GameManager.Instance.hapticsOn) {
            Vibration.Init();
            #if UNITY_IOS
            Vibration.VibrateIOS(ImpactFeedbackStyle.Medium);
            #endif
            #if UNITY_ANDROID
            Vibration.Vibrate(50);
            #endif
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

    public void CancelPlacement(TutorialTile tile) {

        // Reset the occupied values of the grid cells under this tile to false
        RectTransform[] closestGridCells = gameObject.GetComponent<TutorialTile>().GetClosestCellsArray();
        if (closestGridCells != null) {
            foreach(RectTransform gridcell in closestGridCells) {
                if (gridcell.gameObject.GetComponent<GridCell>().colorOccupying == gameObject.GetComponent<TutorialTile>().tileColor) {
                    gridcell.gameObject.GetComponent<GridCell>().isOccupied = false;
                    gridcell.gameObject.GetComponent<GridCell>().colorOccupying = 0;
                    gridcell.gameObject.GetComponent<GridCell>().charOccupying = (char)0;
                }
            }
        }

        tile.CancelPlacement();
        isOnBoard = false;

        // Reset tiles back to their initial size
        SetScale(0.8f);
        
        touchCatcher.GetComponent<CanvasGroup>().blocksRaycasts = false;
        isOnBoard = false;
    }

    private void SetScale(float scale) {
        if (transform.localScale.x < 0) {
            if (transform.localScale.y < 0) {
                transform.localScale = new Vector3(-scale, -scale, 1);
            } else {
                transform.localScale = new Vector3(-scale, scale, 1);
            }
        } else {
            if (transform.localScale.y < 0) {
                transform.localScale = new Vector3(scale, -scale, 1);
            } else {
                transform.localScale = new Vector3(scale, scale, 1);
            }
        }
    }

    private void SetHoveringPositionScale() {
        SetScale(1.733333f);
    }

    private void AddShadowToGrid() {

        // Add shadow to the closest grid cells to the tile
        TutorialTile tile = transform.gameObject.GetComponent<TutorialTile>();
        RectTransform[] closestGridCells = tile.GetClosestCellsArray();

        bool allGridCellsOpen = true;
        if (closestGridCells != null) {
            foreach (RectTransform cell in closestGridCells) {
                if (cell.GetComponent<GridCell>().isOccupied || cell.GetComponent<GridCell>().isBarrier)
                allGridCellsOpen = false;
            }

            if (closestGridCells.Length == gameObject.transform.childCount && allGridCellsOpen) {
                foreach (RectTransform cell in closestGridCells) {
                    cell.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/GridCellShadow");
                }
            } 
        }
    }
}
