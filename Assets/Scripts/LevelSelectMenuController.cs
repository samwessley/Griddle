using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectMenuController : MonoBehaviour {

    [SerializeField] GameObject scrollRect = null;
    [SerializeField] GameObject levelPack = null;
    [SerializeField] GameObject levelsCompleted = null;

    void Start() {
        int pack = GameManager.Instance.currentLevelPack + 5;
        levelPack.GetComponent<Text>().text = pack + "x" + pack + " PACK";
        levelsCompleted.GetComponent<Text>().text = (GameManager.Instance.currentLevel - 1) + " / 1000"; 

        SnapToLevel();
    }

    public void SnapToLevel() {
        
        int level = GameManager.Instance.currentLevel + 1;
        string levelString = "Level Select Button ";
        Canvas.ForceUpdateCanvases();
        levelString += level;

        GameObject child = GameObject.Find(levelString);

        var contentPos = (Vector2)scrollRect.transform.InverseTransformPoint( scrollRect.GetComponent<ScrollRect>().content.position );
        var childPos = (Vector2)scrollRect.transform.InverseTransformPoint( child.transform.position );
        var margin = new Vector2(0,200);
        var endPos = contentPos - childPos - margin;
        // If no horizontal scroll, then don't change contentPos.x
        if( !scrollRect.GetComponent<ScrollRect>().horizontal ) endPos.x = contentPos.x;
        // If no vertical scroll, then don't change contentPos.y
        if( !scrollRect.GetComponent<ScrollRect>().vertical ) endPos.y = contentPos.y;
        scrollRect.GetComponent<ScrollRect>().content.anchoredPosition = endPos;
    }
}
