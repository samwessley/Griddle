using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuAnimationScript : MonoBehaviour {

    [SerializeField] GameObject griddle = null;
    [SerializeField] GameObject brilliance = null;
    [SerializeField] GameObject brillianceScore = null;
    [SerializeField] GameObject beginnerPack = null;
    [SerializeField] GameObject intermediatePack = null;
    [SerializeField] GameObject advancedPack = null;
    [SerializeField] GameObject expertPack = null;
    [SerializeField] GameObject removeAds = null;
    [SerializeField] GameObject settings = null;
    [SerializeField] GameObject about = null;
    [SerializeField] GameObject line = null;

    private float griddleYPosition = 0;
    private float brillianceYPosition = 0;
    private float brillianceScoreYPosition = 0;
    private float settingsYPosition = 0;
    private float aboutYPosition = 0;
    private float removeAdsYPosition = 0;

    void Start() {
        griddleYPosition = griddle.transform.position.y;
        brillianceYPosition = brilliance.transform.position.y;
        brillianceScoreYPosition = brillianceScore.transform.position.y;
        settingsYPosition = settings.transform.position.y;
        aboutYPosition = about.transform.position.y;
        removeAdsYPosition = removeAds.transform.position.y;

        StartCoroutine(StartAnimation());
    }

    IEnumerator StartAnimation() {
        LeanTween.scale(beginnerPack, Vector2.zero, 0);
        LeanTween.scale(intermediatePack, Vector2.zero, 0);
        LeanTween.scale(advancedPack, Vector2.zero, 0);
        LeanTween.scale(expertPack, Vector2.zero, 0);
        LeanTween.scaleX(line, 0, 0);

        LeanTween.moveY(griddle, griddleYPosition + 2f, 0);
        LeanTween.moveY(brilliance, brillianceScoreYPosition + 2f, 0);
        LeanTween.moveY(brillianceScore, brillianceScoreYPosition + 2f, 0);
        LeanTween.moveY(settings, settingsYPosition - 2f, 0);
        LeanTween.moveY(about, aboutYPosition - 2f, 0);
        LeanTween.moveY(removeAds, removeAdsYPosition - 2f, 0);

        LeanTween.scaleX(line, 1, 0.25f);
        LeanTween.moveY(brilliance, brillianceYPosition, 0.25f).setEase(LeanTweenType.easeOutBack);
        LeanTween.moveY(brillianceScore, brillianceScoreYPosition, 0.25f).setEase(LeanTweenType.easeOutBack);
        LeanTween.moveY(settings, settingsYPosition, 0.25f).setEase(LeanTweenType.easeOutBack);
        LeanTween.moveY(removeAds, removeAdsYPosition, 0.25f).setEase(LeanTweenType.easeOutBack);

        yield return new WaitForSeconds(0.1f);

        LeanTween.scale(beginnerPack, new Vector2(1,1), 0.4f).setEase(LeanTweenType.easeOutBack);
        LeanTween.moveY(about, aboutYPosition, 0.25f).setEase(LeanTweenType.easeOutBack);
        yield return new WaitForSeconds(0.1f);

        LeanTween.moveY(griddle, griddleYPosition, 0.25f).setEase(LeanTweenType.easeOutBack);

        LeanTween.scale(intermediatePack, new Vector2(1,1), 0.4f).setEase(LeanTweenType.easeOutBack);
        yield return new WaitForSeconds(0.1f);

        LeanTween.scale(advancedPack, new Vector2(1,1), 0.4f).setEase(LeanTweenType.easeOutBack);
        yield return new WaitForSeconds(0.1f);
        LeanTween.scale(expertPack, new Vector2(1,1), 0.4f).setEase(LeanTweenType.easeOutBack);
    }

}
