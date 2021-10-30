using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridCell : MonoBehaviour {

    private int[] coords;
    public int xIndex;
    public int yIndex;

    public bool isBarrier;
    private bool isCorrect;
    public bool isOccupied = false;

    private bool isValid;
    private bool isInvalid;

    private Image cellImage;
    private Color barrierColor = new Color(35f/255f,63f/255f,69f/255f);
    private Color occupiedColor = new Color(171f/255f,9f/255f,96f/255f);

    void Start() {
        cellImage = GetComponent<Image>();

        UpdateImage();
    }

    public void UpdateImage() {

        if (isBarrier) {
            cellImage.sprite = null;
            cellImage.color = barrierColor;
        } else if (isOccupied) {
            cellImage.color = occupiedColor;
        }
    }

    public void SetCoordinates(int x, int y) {
        xIndex = x;
        yIndex = y;
        coords = new int[] {x,y};
    }

    public void SetState(int state) {

        if (state == 1) {
            isBarrier = true; 
        } else if (state == 2) {
            isOccupied = true;
        }
    }
}