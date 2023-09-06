using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class MainMenuController : MonoBehaviour {

    [SerializeField] GameObject levelNumberLabel5x5 = null;
    [SerializeField] GameObject levelNumberLabel6x6 = null;
    [SerializeField] GameObject levelNumberLabel7x7 = null;
    [SerializeField] GameObject levelNumberLabel8x8 = null;
    [SerializeField] GameObject levelNumberLabel9x9 = null;

    [SerializeField] GameObject check5x5 = null;
    [SerializeField] GameObject check6x6 = null;
    [SerializeField] GameObject check7x7 = null;
    [SerializeField] GameObject check8x8 = null;
    [SerializeField] GameObject check9x9 = null;

    [SerializeField] GameObject musicToggle = null;
    [SerializeField] GameObject soundsToggle = null;
    [SerializeField] GameObject hapticsToggle = null;

    [SerializeField] GameObject removeAdsButton = null;
    [SerializeField] GameObject settingsBackground = null;
    [SerializeField] GameObject settingsPanel = null;

    [SerializeField] GameObject playText = null;

    [SerializeField] GameObject levelNumber = null;

    public GridCell[,] cellGrid;

    private bool musicPlaying;

    private void Awake() {
        GameManager.Instance.currentLevelPack = 1;
        GameManager.Instance.currentLevel = GameManager.Instance.levelsCompleted_6x6 + 1;
        settingsBackground.SetActive(false);
        settingsPanel.SetActive(false);
    }

    private void Start() {
        PopulateCellGrid();
        LevelSetup();
        SetLevelNumber();

        if (GameManager.Instance.currentLevel > 1) {
            playText.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("Sprites/ContinueText");
            playText.GetComponentInChildren<Image>().SetNativeSize();
        }

        if (GameManager.Instance.adsRemoved) {
            removeAdsButton.SetActive(false);
        } else {
            removeAdsButton.SetActive(true);
        }

        levelNumberLabel5x5.GetComponent<Text>().text = "#" + (GameManager.Instance.levelsCompleted_5x5 + 1);
        levelNumberLabel6x6.GetComponent<Text>().text = "#" + (GameManager.Instance.levelsCompleted_6x6 + 1);
        levelNumberLabel7x7.GetComponent<Text>().text = "#" + (GameManager.Instance.levelsCompleted_7x7 + 1);
        levelNumberLabel8x8.GetComponent<Text>().text = "#" + (GameManager.Instance.levelsCompleted_8x8 + 1);
        levelNumberLabel9x9.GetComponent<Text>().text = "#" + (GameManager.Instance.levelsCompleted_9x9 + 1);
    
        if (GameManager.Instance.levelsCompleted_5x5 == 200) {
            check5x5.SetActive(true);
        } else {
            check5x5.SetActive(false);
        }

        if (GameManager.Instance.levelsCompleted_6x6 == 200) {
            check6x6.SetActive(true);
        } else {
            check6x6.SetActive(false);
        }

        if (GameManager.Instance.levelsCompleted_7x7 == 200) {
            check7x7.SetActive(true);
        } else {
            check7x7.SetActive(false);
        }

        if (GameManager.Instance.levelsCompleted_8x8 == 200) {
            check8x8.SetActive(true);
        } else {
            check8x8.SetActive(false);
        }

        if (GameManager.Instance.levelsCompleted_9x9 == 200) {
            check9x9.SetActive(true);
        } else {
            check9x9.SetActive(false);
        }

        // Play background music
        SoundEngine.Instance.PlayMusic();

        SetUpSettingsToggles();
    }

    private void SetUpSettingsToggles() {
        if (GameManager.Instance.musicOn) {
            musicToggle.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Toggle On");
        } else {
            musicToggle.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Toggle Off");
        }

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

    private void PopulateCellGrid() {

        cellGrid = new GridCell[6,6];
        
        for(int y = 0; y < 6; y++) {
            for (int x = 0; x < 6; x++) {

                // Find the cell in the grid and get its GridCell component
                GridCell cell = GameObject.Find("GridCell " + x + "," + y).gameObject.GetComponent<GridCell>();

                // Set the appropriate index in cellGrid to this cell
                cellGrid[x,y] = cell;

                // Set the cell's coordinate properties
                cell.SetCoordinates(x,y);
                cell.SetSortingLayer();
            }
        }
    }

    private void SetLevelNumber() {
        levelNumber.GetComponent<Text>().text = "#" + (GameManager.Instance.levelsCompleted_6x6 + 1).ToString();
    }

    public void LevelSetup() {
        Debug.Log(GameManager.Instance.currentLevel);
        ReadLevelData(GameManager.Instance.currentLevel);
    }

    private void ReadLevelData(int level) {
        char[,] data = new char[6,6];

        string levelPath = "Levels/6x6/" + level.ToString();
        TextAsset txtAsset = (TextAsset)Resources.Load(levelPath, typeof(TextAsset));
        string text = txtAsset.text;

        if (txtAsset != null) {
            using (StringReader reader = new StringReader(text)) {

                for (int y = 0; y < 6; y++) {
                    for (int x = 0; x < 6; x++) {
                        data[x,y] = (char)reader.Read();

                        // Set the appropriate cell's status to the specification in data matrix
                        cellGrid[x,y].SetState(data[x,y]);

                        // Update the cell's image
                        cellGrid[x,y].UpdateImage();
                    }
                    char unused__ = (char)reader.Read();
                }
            }
        } else {
            Debug.Log("File not found!");
        }
    }
}
