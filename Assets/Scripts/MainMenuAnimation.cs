using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuAnimation : MonoBehaviour {

    [SerializeField] GameObject scrollRect = null;
    [SerializeField] GameObject contentPanel = null;
    [SerializeField] GameObject line = null;

    [SerializeField] GameObject water1 = null;
    [SerializeField] GameObject water2 = null;
    [SerializeField] GameObject water3 = null;
    [SerializeField] GameObject water4 = null;

    [SerializeField] GameObject tree1 = null; 
    [SerializeField] GameObject tree2 = null; 
    [SerializeField] GameObject tree3 = null; 
    [SerializeField] GameObject grass = null; 
    [SerializeField] GameObject flower1 = null; 
    [SerializeField] GameObject flower2 = null;   

    [SerializeField] GameObject cloud1 = null; 
    [SerializeField] GameObject cloud2 = null;
    [SerializeField] GameObject cloud3 = null;
    [SerializeField] GameObject cloud4 = null;

    // Start is called before the first frame update
    void Start() {

        if (SceneManager.GetActiveScene().name == "LevelSelectScene") {
            SnapToLevel();
        }

        StartCoroutine(AnimateWater());
        StartCoroutine(AnimateTrees());
        StartCoroutine(AnimateGrass(grass));
        StartCoroutine(AnimateFlowers());
        StartCoroutine(AnimateClouds());
    }

    public void SnapToLevel() {
        
        int level = 0;
        string levelString = "Level Select Button ";
        
        Canvas.ForceUpdateCanvases();

        if (GameManager.Instance.currentLevelPack == 0) {
            level = GameManager.Instance.levelsCompleted_5x5 + 1;
            line.GetComponent<Image>().color = new Color(.396f,.655f,.657f,1);
        } else if (GameManager.Instance.currentLevelPack == 1) {
            level = GameManager.Instance.levelsCompleted_6x6 + 1;
            line.GetComponent<Image>().color = new Color(.933f,.757f,.298f,1);
        } else if (GameManager.Instance.currentLevelPack == 2) {
            level = GameManager.Instance.levelsCompleted_7x7 + 1;
            line.GetComponent<Image>().color = new Color(.894f,.404f,.247f,1);
        } else if (GameManager.Instance.currentLevelPack == 3) {
            level = GameManager.Instance.levelsCompleted_8x8 + 1;
            line.GetComponent<Image>().color = new Color(.933f,.376f,.333f,1);
        } else {
            level = GameManager.Instance.levelsCompleted_9x9 + 1;
            line.GetComponent<Image>().color = new Color(.663f,.18f,.294f,1);
        }
        levelString += level;

        GameObject child = GameObject.Find(levelString);

        var contentPos = (Vector2)scrollRect.transform.InverseTransformPoint( scrollRect.GetComponent<ScrollRect>().content.position );
        var childPos = (Vector2)scrollRect.transform.InverseTransformPoint( child.transform.position );
        var endPos = contentPos - childPos;
        // If no horizontal scroll, then don't change contentPos.x
        if( !scrollRect.GetComponent<ScrollRect>().horizontal ) endPos.x = contentPos.x;
        // If no vertical scroll, then don't change contentPos.y
        if( !scrollRect.GetComponent<ScrollRect>().vertical ) endPos.y = contentPos.y;
        scrollRect.GetComponent<ScrollRect>().content.anchoredPosition = endPos;

        /*Vector2 viewportLocalPosition = instance.viewport.localPosition;
        Vector2 childLocalPosition = child.localPosition;
        Vector2 result = new Vector2(
                0 - (viewportLocalPosition.x + childLocalPosition.x),
                0 - (viewportLocalPosition.y + childLocalPosition.y)
            );
        contentPanel.localPosition = result;*/
    }

    IEnumerator AnimateWater() {
        StartCoroutine(AnimateWaterLayer(water1));
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(AnimateWaterLayer(water2));
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(AnimateWaterLayer(water3));
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(AnimateWaterLayer(water4));
    }

    IEnumerator AnimateTrees() {
        yield return new WaitForSeconds(4f);
        StartCoroutine(AnimateTree(tree1));
        yield return new WaitForSeconds(3f);
        StartCoroutine(AnimateTree(tree2));
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(AnimateTree(tree3));
        yield return new WaitForSeconds(6f);
    }

    IEnumerator AnimateWaterLayer(GameObject waterLayer) {

        Vector3 startPos = new Vector3(0,waterLayer.transform.localPosition.y - 6f,0);
        Vector3 endPos = new Vector3(0,waterLayer.transform.localPosition.y, 0);

        while (true) {
            LeanTween.moveLocal(waterLayer, endPos, .5f);
            yield return new WaitForSeconds(.5f);
            LeanTween.moveLocal(waterLayer, startPos, .5f);
            yield return new WaitForSeconds(.5f);
        }
    }

    IEnumerator AnimateTree(GameObject tree) {
        float originalScale = tree.transform.localScale.y;

        while (true) {
            LeanTween.scaleY(tree, originalScale * .97f, 0.1f);
            yield return new WaitForSeconds(.1f);
            LeanTween.scaleY(tree, originalScale, 0.1f);
            yield return new WaitForSeconds(9f);
        }
    }

    IEnumerator AnimateGrass(GameObject grass) {
        float originalScale = grass.transform.localScale.x;

        while (true) {
            LeanTween.scaleX(grass, originalScale * .9f, 2f);
            yield return new WaitForSeconds(2f);
            LeanTween.scaleX(grass, originalScale, 2f);
            yield return new WaitForSeconds(2f);
        }
    }

    IEnumerator AnimateFlower(GameObject flower) {
        while (true) {
            LeanTween.rotateZ(flower, flower.transform.localEulerAngles.z + 20f, 0.3f);
            yield return new WaitForSeconds(0.3f);
            LeanTween.rotateZ(flower, flower.transform.localEulerAngles.z - 20f, 0.3f);
            yield return new WaitForSeconds(8f);
        }
    }

    IEnumerator AnimateFlowers() {
        yield return new WaitForSeconds(2f);
        StartCoroutine(AnimateFlower(flower1));
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(AnimateFlower(flower2));
    }

    IEnumerator AnimateClouds() {
        StartCoroutine(AnimateCloud1());
        StartCoroutine(AnimateCloud2());
        StartCoroutine(AnimateCloud3());
        StartCoroutine(AnimateCloud4());
        yield return null;
    }


    IEnumerator AnimateCloud1() {
        LeanTween.moveX(cloud1, 3.5f, 13f);
        yield return new WaitForSeconds(13f);
        LeanTween.moveX(cloud1, -9f, 0);
        yield return new WaitForSeconds(8f);

        while (true) {
            LeanTween.moveX(cloud1, 3.5f, 29f);
            yield return new WaitForSeconds(29f);
            LeanTween.moveX(cloud1, -9f, 0);
            yield return new WaitForSeconds(9f);
        }
    }

    IEnumerator AnimateCloud2() {
        LeanTween.moveX(cloud2, 3.5f, 25f);
        yield return new WaitForSeconds(25f);
        LeanTween.moveX(cloud2, -9f, 0);
        yield return new WaitForSeconds(9.5f);

        while (true) {
            LeanTween.moveX(cloud2, 3.5f, 39f);
            yield return new WaitForSeconds(39f);
            LeanTween.moveX(cloud2, -9f, 0);
            yield return new WaitForSeconds(7f);
        }
    }

    IEnumerator AnimateCloud3() {
        LeanTween.moveX(cloud3, 3.5f, 52f);
        yield return new WaitForSeconds(52f);
        LeanTween.moveX(cloud3, -9f, 0);
        yield return new WaitForSeconds(2.5f);

        while (true) {
            LeanTween.moveX(cloud3, 3.5f, 55f);
            yield return new WaitForSeconds(55f);
            LeanTween.moveX(cloud3, -9f, 0);
            yield return new WaitForSeconds(7.5f);
        }
    }

    IEnumerator AnimateCloud4() {
        LeanTween.moveX(cloud4, 3.5f, 48f);
        yield return new WaitForSeconds(48f);
        LeanTween.moveX(cloud4, -9f, 0);
        yield return new WaitForSeconds(2.5f);

        while (true) {
            LeanTween.moveX(cloud4, 235f, 55f);
            yield return new WaitForSeconds(55f);
            LeanTween.moveX(cloud4, -9f, 0);
            yield return new WaitForSeconds(6f);
        }
    }
}