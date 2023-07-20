using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler {

    private GameController gameController;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private GameObject touchCatcher;
    //private GameObject tilePopupTray;
    private RectTransform rectTransform;

    public bool isOnBoard = false;
    public bool isHighlighted = false;

    private Vector2 pointerLocation;

    private void Start() {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        canvas = this.gameObject.transform.parent.gameObject.GetComponent<Canvas>();
        touchCatcher = canvas.transform.Find("Touch Catcher").gameObject;
        //tilePopupTray = canvas.transform.Find("Tile Popup Tray").gameObject;
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        if (gameController.boardSize == 5) {
            SetScale(0.8f);
        } else if (gameController.boardSize == 6) {
            SetScale(0.6f);
        } else {
            SetScale(0.5f);
        }
    }

    public void OnPointerDown(PointerEventData eventData) {

        pointerLocation = eventData.position;

        // If the tile is on the board when tapped, set it to 'hovering' status
        if (isOnBoard) {

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

            gameController.tilesRemaining += 1;
        }
        // If it's not on the board when tapped but it is highlighted, put it in 'hovering' status
        else {
            // Set the sorting order of the tile to its highlighted value
            TileCell[] tileCells = transform.gameObject.GetComponentsInChildren<TileCell>();
            foreach (TileCell tileCell in tileCells)
            tileCell.SetSortingLayer(tileCell.GetComponent<Canvas>().sortingOrder + 19);

            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y + 250);
            SetHoveringPositionScale();

            /*if (isHighlighted) {
                rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y + 250);
                SetHoveringPositionScale();
            }*/
        }
    }

    public void OnDrag(PointerEventData eventData) {
        // We only want to drag the tile if it was on the board or highlighted when tapped
    
            Vector2 vector = (eventData.delta / canvas.scaleFactor);
            vector *= 1.2f;
            rectTransform.anchoredPosition += vector;
    
    }

    public void OnPointerUp(PointerEventData eventData) {

        Tile tile = transform.gameObject.GetComponent<Tile>();

        //Debug.Log(tile.tileCells[0].xOffset + ", " + tile.tileCells[0].yOffset);

        if (eventData.position == pointerLocation && !isOnBoard)
        CancelPlacement(tile);

        // If the tile isn't currently on the board, show the touchCatcher when tapping on the tile
        /*if (!isOnBoard) {
            touchCatcher.GetComponent<CanvasGroup>().blocksRaycasts = true;
            //tilePopupTray.gameObject.SetActive(true); 

            if (!isHighlighted) {
                // Set tile size and position to 'highlighted' status
                SetScale(1f);

                rectTransform.anchoredPosition = tilePopupTray.GetComponent<RectTransform>().anchoredPosition;
                isHighlighted = true;
                gameController.activeTile = this.gameObject;

                // Set the sorting order of the tile to its highlighted value
                TileCell[] cells = transform.gameObject.GetComponentsInChildren<TileCell>();
                foreach (TileCell cell in cells) {
                    cell.SetSortingLayer(cell.GetComponent<Canvas>().sortingOrder + 5);
                    //Debug.Log(cell.xOffset + ", " + cell.yOffset + ": " + cell.GetComponent<Canvas>().sortingOrder);
                }

                // Stop at this point. We need another tap on the tile to continue further
                return;
            }
        }*/

        // If we got to this point, the tile was highlighted or already on the board before being
        // tapped. Set isOnBoard to true before testing if it's in a valid board space.
        isOnBoard = true;

        // Reset the scale back to normal
        if (gameController.boardSize == 5) {
            SetScale(GameManager.Instance.tileScaleFactors[0]);
        } else if (gameController.boardSize == 6) {
            SetScale(GameManager.Instance.tileScaleFactors[1]);
        } else if (gameController.boardSize == 7) {
            SetScale(GameManager.Instance.tileScaleFactors[2]);
        } else if (gameController.boardSize == 8) {
            SetScale(GameManager.Instance.tileScaleFactors[3]);
        } else if (gameController.boardSize == 9) {
            SetScale(GameManager.Instance.tileScaleFactors[4]);
        } else {
            SetScale(GameManager.Instance.tileScaleFactors[5]);
        }

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
        touchCatcher.GetComponent<CanvasGroup>().blocksRaycasts = false;
        //tilePopupTray.gameObject.SetActive(false);

        // Set the sorting layer of the tile to the appropriate level based on where it is on the board
        int minSortingOrder = 999;

        // Loop through all the closest grid cells to the tile to find the minimum sorting layer under this tile
        foreach(Transform cell in closestGridCells)
        if (cell.GetComponent<Canvas>().sortingOrder < minSortingOrder)
        minSortingOrder = cell.GetComponent<Canvas>().sortingOrder;

        // Set each tileCell's sorting layer to this minimum sorting layer plus its sorting layer inside its tile
        TileCell[] tileCells = tile.GetComponentsInChildren<TileCell>();
        int minTileCellSortingOrder = 999;
        foreach (TileCell tileCell in tileCells) {
            if (tileCell.GetComponent<Canvas>().sortingOrder < minTileCellSortingOrder)
            minTileCellSortingOrder = tileCell.GetComponent<Canvas>().sortingOrder;
        }

        foreach (TileCell tileCell in tileCells) {
            tileCell.SetSortingLayer(tileCell.GetComponent<Canvas>().sortingOrder - minTileCellSortingOrder + minSortingOrder + 1);
        }

        // Set each grid cell under this tile to 'occupied'
        foreach(RectTransform gridcell in closestGridCells) {
            gridcell.gameObject.GetComponent<GridCell>().isOccupied = true;
            gridcell.gameObject.GetComponent<GridCell>().colorOccupying = tile.tileColor;
            gridcell.gameObject.GetComponent<GridCell>().charOccupying = tile.tileCode;
        }

        isHighlighted = false;
        gameController.activeTile = null;
        gameController.tilesRemaining -= 1;
        
        // Play pop sound
        if (GameManager.Instance.soundsOn)
        SoundEngine.Instance.PlayPopSound();

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

        bool isInCorrectPosition = IsInCorrectPosition(tile, closestGridCells);
        if (isInCorrectPosition)
        gameController.distinctCharsRemaining = gameController.distinctCharsRemaining.Replace(tile.tileCode.ToString(),"");

        gameController.CheckForLevelComplete();
    }

    public bool IsInCorrectPosition(Tile tile, RectTransform[] closestGridCells) {
        bool isInCorrectPosition = true;

        if (closestGridCells == null)
        return true;

        foreach (RectTransform gridCell in closestGridCells) {
            if (gridCell.GetComponent<GridCell>().state != tile.tileCode)
            isInCorrectPosition = false;
        }

        return isInCorrectPosition;
    }

    private bool TilePlacedOverOccupiedOrBarrierGridCells(Transform[] closestCells) {
        for (int i = 0; i < closestCells.Length; i++) {
            if (closestCells[i].GetComponent<GridCell>().isOccupied || closestCells[i].GetComponent<GridCell>().isBarrier) {
                return true;
            }
        }
        return false;
    }

    public void CancelPlacement(Tile tile) {

        // Reset the occupied values of the grid cells under this tile to false
        RectTransform[] closestGridCells = gameObject.GetComponent<Tile>().GetClosestCellsArray();
        if (closestGridCells != null) {
            foreach(RectTransform gridcell in closestGridCells) {
                if (gridcell.gameObject.GetComponent<GridCell>().colorOccupying == gameObject.GetComponent<Tile>().tileColor) {
                    gridcell.gameObject.GetComponent<GridCell>().isOccupied = false;
                    gridcell.gameObject.GetComponent<GridCell>().colorOccupying = 0;
                    gridcell.gameObject.GetComponent<GridCell>().charOccupying = (char)0;
                }
            }
        }

        tile.CancelPlacement();
        isOnBoard = false;

        // Reset tiles back to their initial size depending on board size
        if (gameController.boardSize == 5) {
            SetScale(0.8f);
        } else if (gameController.boardSize == 6) {
            SetScale(0.6f);
        } else {
            SetScale(0.5f);
        }
        
        touchCatcher.GetComponent<CanvasGroup>().blocksRaycasts = false;
        //tilePopupTray.gameObject.SetActive(false);
        isHighlighted = false;
        isOnBoard = false;
        gameController.activeTile = null;

        // Set the sorting order of the tile to its default value
        TileCell[] cells = transform.gameObject.GetComponentsInChildren<TileCell>();
        int minSortingOrder = 999;

        for (int i = 0; i < cells.Length; i++) {
            if (cells[i].GetComponent<Canvas>().sortingOrder < minSortingOrder)
            minSortingOrder = cells[i].GetComponent<Canvas>().sortingOrder;
        }

        int sortingOrderIncrement = 14 - minSortingOrder;

        foreach (TileCell cell in cells) {
            cell.SetSortingLayer(cell.GetComponent<Canvas>().sortingOrder + sortingOrderIncrement);
        }
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
        if (gameController.boardSize == 5) {
            SetScale(2.5f);
        } else if (gameController.boardSize == 6) {
            SetScale(2.1f);
        } else if (gameController.boardSize == 7) {
            SetScale(1.8f);
        } else if (gameController.boardSize == 8) {
            SetScale(1.6f);
        } else if (gameController.boardSize == 9) {
            SetScale(1.4f);
        } else {
            SetScale(1.2f);
        }
    }
}