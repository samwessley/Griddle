using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour {

    public void LoadMenuScene() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void LoadNextLevel() {
        // Increment current level
        GameManager.Instance.currentLevel += 1;

        // Reload scene
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }

    public void LoadPreviousLevel() {
        // Increment current level
        if (GameManager.Instance.currentLevel > 1)
            GameManager.Instance.currentLevel -= 1;

        // Reload scene
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }

    public void RestartLevel() {

        // Reload scene
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }

    public void RotateTile() {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");

        foreach(GameObject tile in tiles) {

            if (tile.GetComponent<DragDrop>().isHighlighted) {
                // Rotate the cell object
                tile.GetComponent<RectTransform>().Rotate(0, 0, 90);

                // Rotate the tile cells' offset locations to account for rotation
                TileCell[] tileCells = tile.GetComponent<Tile>().tileCells;
                
                // Get the maximum Y offset of the tile cells so we know where the
                // minimum sorting order starts

                int tileCellMaxYOffset = -999;
                
                for (int i = 0; i < tileCells.Length; i++) {

                    tileCells[i].GetComponent<RectTransform>().Rotate(0, 0, -90);

                    float oldX = tileCells[i].xOffset;
                    float oldY = tileCells[i].yOffset;

                    tileCells[i].xOffset = -oldY;
                    tileCells[i].yOffset = oldX;

                    if (tileCells[i].yOffset > tileCellMaxYOffset) {
                        tileCellMaxYOffset = (int)tileCells[i].yOffset;
                    }
                }

                int numberOfRows;
                List<int> rowList = new List<int>();

                foreach(TileCell cell in tileCells) {
                    if (!rowList.Contains((int)cell.yOffset))
                    rowList.Add((int)cell.yOffset);
                }
                rowList.Sort();
                rowList.Reverse();
                numberOfRows = rowList.Count;

                for (int i = 0; i < tileCells.Length; i++) {
                    
                    int offsetMargin = (tileCellMaxYOffset - (int)tileCells[i].yOffset)/45;
                    Debug.Log(offsetMargin);
                    tileCells[i].SetSortingLayer(19 + rowList.IndexOf((int)tileCells[i].yOffset));
                    Debug.Log(tileCells[i].xOffset + ", " + tileCells[i].yOffset + ": " + tileCells[i].GetComponent<Canvas>().sortingOrder);
                }
            }
        }
    }
}
