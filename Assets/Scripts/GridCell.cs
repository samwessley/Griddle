using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridCell : MonoBehaviour {

    public int[] coords;
    public int xIndex;
    public int yIndex;

    public bool isBarrier = false;
    public bool isOccupied = false;
    public int colorOccupying = 0;
    public bool isStar = false;

    private bool isValid;
    private bool isInvalid;

    private Color barrierColor = new Color(255f/255f,253f/255f,244f/255f);
    private Color barrierSideColor = new Color(213f/255f,211f/255f,201f/255f);
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
            childObj.sprite = Resources.Load<Sprite>("Sprites/Tile");
            childObj.color = new Color(1,1,1,1);

            Canvas canvas = gameObject.GetComponent<Canvas>();
            canvas.sortingOrder += 2;
        }
        else if (isStar) {
            gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Star Tile");
        }
    }

    public void SetCoordinates(int x, int y) {
        xIndex = x;
        yIndex = y;
        coords = new int[] {x,y};
    }

    public void SetState(char state) {

        if (state == '1') {
            isBarrier = true; 
        } else if (state == '2') {
            isOccupied = true;
        } else if (state == '3') {
            isStar = true;
        }
    }
}
