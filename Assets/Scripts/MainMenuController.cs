using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class MainMenuController : MonoBehaviour {

    [SerializeField] GameObject brillianceScore = null;
    [SerializeField] GameObject brillianceScoreContainer = null;

    [SerializeField] GameObject levelNumberLabel5x5 = null;
    [SerializeField] GameObject levelNumberLabel6x6 = null;
    [SerializeField] GameObject levelNumberLabel7x7 = null;
    [SerializeField] GameObject levelNumberLabel8x8 = null;

    [SerializeField] GameObject levelNumberContainer5x5 = null;
    [SerializeField] GameObject levelNumberContainer6x6 = null;
    [SerializeField] GameObject levelNumberContainer7x7 = null;
    [SerializeField] GameObject levelNumberContainer8x8 = null;

    [SerializeField] GameObject check5x5 = null;
    [SerializeField] GameObject check6x6 = null;
    [SerializeField] GameObject check7x7 = null;
    [SerializeField] GameObject check8x8 = null;

    [SerializeField] GameObject soundsToggle = null;
    [SerializeField] GameObject hapticsToggle = null;

    [SerializeField] GameObject removeAdsButton = null;
    [SerializeField] GameObject settingsPanel = null;

    private void Awake() {
        settingsPanel.SetActive(false);
    }

    private void Start() {
        HideShowAdsButton();
        SetUpSettingsToggles();
        SetLevelNumbers();

        // Set brilliance score
        brillianceScore.GetComponent<Text>().text = (10*GameManager.Instance.levelsCompleted_5x5 + 30*GameManager.Instance.levelsCompleted_6x6 + 60*GameManager.Instance.levelsCompleted_7x7 + 90*GameManager.Instance.levelsCompleted_8x8).ToString("n0");
        LayoutRebuilder.ForceRebuildLayoutImmediate(brillianceScoreContainer.GetComponent<RectTransform>());
    }

    private void HideShowAdsButton() {
        // Hide/show ads button according to ads removed status
        if (GameManager.Instance.adsRemoved) {
            removeAdsButton.SetActive(false);
        } else {
            removeAdsButton.SetActive(true);
        }
    }

    private void SetLevelNumbers() {
        // Set 5x5 level number
        if (GameManager.Instance.levelsCompleted_5x5 < GameManager.Instance.totalLevels) {
            levelNumberLabel5x5.GetComponent<Text>().text = (GameManager.Instance.levelsCompleted_5x5 + 1).ToString();
        } else {
            levelNumberLabel5x5.GetComponent<Text>().text = (GameManager.Instance.levelsCompleted_5x5).ToString();
        }

        // Set 6x6 level number
        if (GameManager.Instance.levelsCompleted_6x6 < GameManager.Instance.totalLevels) {
            levelNumberLabel6x6.GetComponent<Text>().text = (GameManager.Instance.levelsCompleted_6x6 + 1).ToString();
        } else {
            levelNumberLabel6x6.GetComponent<Text>().text = (GameManager.Instance.levelsCompleted_6x6).ToString();
        }

        // Set 7x7 level number
        if (GameManager.Instance.levelsCompleted_7x7 < GameManager.Instance.totalLevels) {
            levelNumberLabel7x7.GetComponent<Text>().text = (GameManager.Instance.levelsCompleted_7x7 + 1).ToString();
        } else {
            levelNumberLabel7x7.GetComponent<Text>().text = (GameManager.Instance.levelsCompleted_7x7).ToString();
        }

        // Set 8x8 level number
        if (GameManager.Instance.levelsCompleted_8x8 < GameManager.Instance.totalLevels) {
            levelNumberLabel8x8.GetComponent<Text>().text = (GameManager.Instance.levelsCompleted_8x8 + 1).ToString();
        } else {
            levelNumberLabel8x8.GetComponent<Text>().text = GameManager.Instance.levelsCompleted_8x8.ToString();
        }

        // Set 5x5 checkmark
        if (GameManager.Instance.levelsCompleted_5x5 == GameManager.Instance.totalLevels) {
            check5x5.SetActive(true);
        } else {
            check5x5.SetActive(false);
        }

        // Set 6x6 checkmark
        if (GameManager.Instance.levelsCompleted_6x6 == GameManager.Instance.totalLevels) {
            check6x6.SetActive(true);
        } else {
            check6x6.SetActive(false);
        }

        // Set 7x7 checkmark
        if (GameManager.Instance.levelsCompleted_7x7 == GameManager.Instance.totalLevels) {
            check7x7.SetActive(true);
        } else {
            check7x7.SetActive(false);
        }

        // Set 8x8 checkmark
        if (GameManager.Instance.levelsCompleted_8x8 == GameManager.Instance.totalLevels) {
            check8x8.SetActive(true);
        } else {
            check8x8.SetActive(false);
        }

        // Rebuild level number containers
        LayoutRebuilder.ForceRebuildLayoutImmediate(levelNumberContainer5x5.GetComponent<RectTransform>());
        LayoutRebuilder.ForceRebuildLayoutImmediate(levelNumberContainer6x6.GetComponent<RectTransform>());
        LayoutRebuilder.ForceRebuildLayoutImmediate(levelNumberContainer7x7.GetComponent<RectTransform>());
        LayoutRebuilder.ForceRebuildLayoutImmediate(levelNumberContainer8x8.GetComponent<RectTransform>());
    }

    private void SetUpSettingsToggles() {
        if (GameManager.Instance.soundsOn) {
            soundsToggle.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Toggle On");
        } else {
            soundsToggle.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Toggle Off");
        }

        if (GameManager.Instance.hapticsOn) {
            hapticsToggle.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Toggle On");
        } else {
            hapticsToggle.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Toggle Off");
        }
    }
}
