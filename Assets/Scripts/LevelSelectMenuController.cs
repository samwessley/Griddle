using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LionStudios.Suite.Debugging; 

public class LevelSelectMenuController : MonoBehaviour {

    [SerializeField] GameObject scrollRect = null;
    [SerializeField] GameObject levelPack = null;
    [SerializeField] GameObject levelsCompleted = null;
    [SerializeField] GameObject buttons = null;

    void Awake() {
        CreateButtons();
        int pack = GameManager.Instance.currentLevelPack + 5;
        levelPack.GetComponent<Text>().text = pack + "x" + pack + " Pack";

        if (pack == 5) {
            GameManager.Instance.currentLevel = GameManager.Instance.levelsCompleted_5x5 + 1;
            levelsCompleted.GetComponent<Text>().text = (GameManager.Instance.levelsCompleted_5x5) + " / 1000";
            
            if (GameManager.Instance.levelsCompleted_5x5 < GameManager.Instance.totalLevels) {
                SnapToLevel(GameManager.Instance.levelsCompleted_5x5 + 1);
            } else {
                SnapToLevel(GameManager.Instance.levelsCompleted_5x5);
            }
        } else if (pack == 6) {
            GameManager.Instance.currentLevel = GameManager.Instance.levelsCompleted_6x6 + 1;
            levelsCompleted.GetComponent<Text>().text = (GameManager.Instance.levelsCompleted_6x6) + " / 1000";

            if (GameManager.Instance.levelsCompleted_6x6 < GameManager.Instance.totalLevels) {
                SnapToLevel(GameManager.Instance.levelsCompleted_6x6 + 1);
            } else {
                SnapToLevel(GameManager.Instance.levelsCompleted_6x6);
            }
        } else if (pack == 7) {
            GameManager.Instance.currentLevel = GameManager.Instance.levelsCompleted_7x7 + 1;
            levelsCompleted.GetComponent<Text>().text = (GameManager.Instance.levelsCompleted_7x7) + " / 1000";

            if (GameManager.Instance.levelsCompleted_7x7 < GameManager.Instance.totalLevels) {
                SnapToLevel(GameManager.Instance.levelsCompleted_7x7 + 1);
            } else {
                SnapToLevel(GameManager.Instance.levelsCompleted_7x7);
            }
        } else if (pack == 8) {
            GameManager.Instance.currentLevel = GameManager.Instance.levelsCompleted_8x8 + 1;
            levelsCompleted.GetComponent<Text>().text = (GameManager.Instance.levelsCompleted_8x8) + " / 1000";

            if (GameManager.Instance.levelsCompleted_8x8 < GameManager.Instance.totalLevels) {
                SnapToLevel(GameManager.Instance.levelsCompleted_8x8 + 1);
            } else {
                SnapToLevel(GameManager.Instance.levelsCompleted_8x8);
            }
        } else if (pack == 9) {
            GameManager.Instance.currentLevel = GameManager.Instance.levelsCompleted_9x9 + 1;
            levelsCompleted.GetComponent<Text>().text = (GameManager.Instance.levelsCompleted_9x9) + " / 1000";

            if (GameManager.Instance.levelsCompleted_9x9 < GameManager.Instance.totalLevels) {
                SnapToLevel(GameManager.Instance.levelsCompleted_9x9 + 1);
            } else {
                SnapToLevel(GameManager.Instance.levelsCompleted_9x9);
            }
        }
    }

    void Start() {
        LionDebugger.Hide();
    }

    private void SnapToLevel(int currentLevel) {
        
        int level = currentLevel;
        string levelString = "Level Select Button ";
        Canvas.ForceUpdateCanvases();
        levelString += level;

        GameObject child = GameObject.Find(levelString);

        var contentPos = (Vector2)scrollRect.transform.InverseTransformPoint( scrollRect.GetComponent<ScrollRect>().content.position );
        var childPos = (Vector2)scrollRect.transform.InverseTransformPoint( child.transform.position );
        var margin = new Vector2(0,100);
        var endPos = contentPos - childPos - margin;
        // If no horizontal scroll, then don't change contentPos.x
        if( !scrollRect.GetComponent<ScrollRect>().horizontal ) endPos.x = contentPos.x;
        // If no vertical scroll, then don't change contentPos.y
        if( !scrollRect.GetComponent<ScrollRect>().vertical ) endPos.y = contentPos.y;
        scrollRect.GetComponent<ScrollRect>().content.anchoredPosition = endPos;
    }

    private void CreateButtons() {

        for (int i = 0; i < GameManager.Instance.totalLevels; i++) {
            GameObject tile = Instantiate(Resources.Load<GameObject>("Prefabs/Level Select Button"));
            tile.name = "Level Select Button " + (i + 1);
            tile.GetComponent<LevelSelectButtonScript>().level = (i+1);
            tile.transform.SetParent(buttons.transform);
            tile.transform.localScale = new Vector3(1,1,1);
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(buttons.GetComponent<RectTransform>());
    }
}
