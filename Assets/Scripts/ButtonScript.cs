using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour {

    public void LoadMenuScene() {
        SceneManager.LoadScene(1);
    }

    public void LoadPreviousLevel() {
        // Increment current level
        if (GameManager.Instance.currentLevel > 1) {
            GameManager.Instance.currentLevel -= 1;

            // Reload scene
            Scene scene = SceneManager.GetActiveScene(); 
            SceneManager.LoadScene(scene.name);
        } else {
            SceneManager.LoadScene(0);
        }
    }

    public void LoadNextLevel() {
        // Increment current level
        if (GameManager.Instance.currentLevel < GameManager.Instance.totalLevels) {
            GameManager.Instance.currentLevel += 1;

            // Reload scene
            Scene scene = SceneManager.GetActiveScene(); 
            SceneManager.LoadScene(scene.name);
        } else {
            SceneManager.LoadScene(0);
        }
    }

    public void LoadNextLevelSet() {
        
    }

    public void LevelFinishedLoadNextLevel() {
        // Increment current level
        if (GameManager.Instance.currentLevel < GameManager.Instance.totalLevels) {

            GameManager.Instance.currentLevel += 1;

            // Load next level
            Scene scene = SceneManager.GetActiveScene(); 
            SceneManager.LoadScene(scene.name);
        } else {
            Debug.Log("Jf");
            SceneManager.LoadScene(0);
        }
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

                TileCell[] tileCells = tile.GetComponent<Tile>().tileCells;
                
                // Rotate the Tile transform
                int rotations = tile.GetComponent<Tile>().rotations;
                if (rotations < 3) {
                    tile.transform.eulerAngles = new Vector3(
                        tile.transform.eulerAngles.x,
                        tile.transform.eulerAngles.y,
                        tile.transform.eulerAngles.z + 90
                    );

                    for (int i = 0; i < tileCells.Length; i++) {
                        
                        // Rotate the tile cell transforms
                        if (tile.GetComponent<Tile>().reflected)
                        tileCells[i].GetComponent<RectTransform>().Rotate(0, 0, -270);
                        else
                        tileCells[i].GetComponent<RectTransform>().Rotate(0, 0, -90);

                        // Rotate the tile cell offset values
                        float oldX = tileCells[i].xOffset;
                        float oldY = tileCells[i].yOffset;

                        tileCells[i].xOffset = -oldY;
                        tileCells[i].yOffset = oldX;
                    }
                } else {

                    tile.GetComponent<Tile>().rotations = 0;

                    // Reset the tile back to its default state
                    ResetTile(tile);

                    // If the tile was reflected before reset, reflect it back
                    if (tile.GetComponent<Tile>().reflected) {
                        // Reflect the transform
                        tile.transform.localScale = new Vector2(-1, 1);
            
                        // Reflect the tile cells' offset locations
                        for (int j = 0; j < tileCells.Length; j++) {
                            tileCells[j].xOffset = -tileCells[j].xOffset;
                        }
                    }
                }
                tile.GetComponent<Tile>().rotations += 1;

                // Update the sorting order of each tile cell
                UpdateSortingOrder(tileCells);
            }
        }
    }

    public void ReflectTile() {

        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");

        for (int i = 0; i < tiles.Length; i++) {
            if (tiles[i].GetComponent<DragDrop>().isHighlighted) {
                
                GameObject tile = tiles[i].gameObject;
                TileCell[] tileCells = tile.GetComponent<Tile>().tileCells;
                bool tileReflected = tile.GetComponent<Tile>().reflected;

                // Reset the tile back to its default state
                ResetTile(tile);

                // If the tile isn't reflected, reflect it
                if (!tileReflected) {
                    // Reflect the transform
                    tiles[i].transform.localScale = new Vector2(-1, 1);
        
                    // Reflect the tile cells' offset locations
                    for (int j = 0; j < tileCells.Length; j++) 
                        tileCells[j].xOffset = -tileCells[j].xOffset;
                }

                // Update reflected property
                tiles[i].GetComponent<Tile>().reflected = !tiles[i].GetComponent<Tile>().reflected;

                // Update the sorting order of each tile cell
                UpdateSortingOrder(tileCells);
            }
        }
    }

    private void UpdateSortingOrder(TileCell[] tileCells) {

        List<int> rowList = new List<int>();

        foreach(TileCell cell in tileCells) {
            if (!rowList.Contains((int)cell.yOffset))
            rowList.Add((int)cell.yOffset);
        }
        rowList.Sort();
        rowList.Reverse();

        foreach(TileCell cell in tileCells) {
            cell.SetSortingLayer(19 + rowList.IndexOf((int)cell.yOffset));
            //Debug.Log(cell.xOffset + ", " + cell.yOffset + ": " + cell.GetComponent<Canvas>().sortingOrder);
        }
    }

    private void ResetTile(GameObject tile) {

        // Reset the tile transform back to its default state
        tile.transform.rotation = Quaternion.identity;
        tile.transform.localScale = new Vector2(1, 1);
        tile.GetComponent<Tile>().rotations = 0;

        TileCell[] tileCells = tile.GetComponent<Tile>().tileCells;

        // Reset the tile cells back to their default state
        for (int j = 0; j < tileCells.Length; j++) {
            tileCells[j].transform.localScale = new Vector2(1, 1);
            tileCells[j].transform.rotation = Quaternion.identity;
            tileCells[j].PopulateOffsetValues();
        }
    }
}
