using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridCell : MonoBehaviour {

    public int[] coords;
    public int xIndex;
    public int yIndex;
    public char state;

    public bool isBarrier = false;
    public bool isOccupied = false;
    public int colorOccupying = 0;
    public char charOccupying;

    private bool isValid;
    private bool isInvalid;

    private Color barrierColor = new Color(201f/255f,201f/255f,201f/255f);
    private Color barrierSideColor = new Color(30f/255f,30f/255f,32f/255f);
    private Color occupiedColor = new Color(171f/255f,9f/255f,96f/255f);
    private Color shadowColor = new Color(129f/255f,6f/255f,72f/255f);

    void Awake() {
        UpdateImage();
    }

    public void SetSortingLayer() {
        Canvas canvas = gameObject.GetComponent<Canvas>();
        canvas.sortingOrder = yIndex + 1;
    }

    public void UpdateImage() {

        if (isBarrier) {
            gameObject.GetComponent<Image>().sprite = null;
            gameObject.GetComponent<Image>().color = barrierSideColor;

            Image childObj = this.gameObject.transform.GetChild(0).GetComponent<Image>();
            childObj.sprite = null;
            childObj.color = barrierColor;
            
            Canvas canvas = gameObject.GetComponent<Canvas>();
            canvas.sortingOrder += 1;
        }
        else if (isOccupied) {

            gameObject.GetComponent<Image>().sprite = null;
            gameObject.GetComponent<Image>().color = shadowColor;

            Image childObj = this.gameObject.transform.GetChild(0).GetComponent<Image>();
            if (colorOccupying == 1) {
                childObj.sprite = Resources.Load<Sprite>("Sprites/Tile Red");
            } else if (colorOccupying == 2) {
                childObj.sprite = Resources.Load<Sprite>("Sprites/Tile Blue");
            } else {
                childObj.sprite = Resources.Load<Sprite>("Sprites/Tile Yellow");
            }
            childObj.color = new Color(1,1,1,1);

            Canvas canvas = gameObject.GetComponent<Canvas>();
            canvas.sortingOrder += 1;
        }
    }

    public void SetCoordinates(int x, int y) {
        xIndex = x;
        yIndex = y;
        coords = new int[] {x,y};
    }

    public void SetState(char character) {

        state = character;

        if (character == '1') {
            isBarrier = true; 
        } else if (character == '2') {
            isOccupied = true;
        }
    }
}
