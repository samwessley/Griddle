using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager: MonoBehaviour {

    private static GameManager _Instance;

    private string file = "/save.txt";

    public int totalLevels;
    public int currentLevelPack;
    public int currentLevel;

    public int levelsCompleted_5x5;
    public int levelsCompleted_6x6;
    public int levelsCompleted_7x7;
    public int levelsCompleted_8x8;
    public int levelsCompleted_9x9;

    public int hintsRemaining;
    public int skipsRemaining;

    public bool musicOn;
    public bool soundsOn;
    public bool hapticsOn;
    public bool adsRemoved;
    public bool tutorialShown;

    //public string[][] levelTiles = new string[3][];
    public Dictionary<char, string> tileDictionary = new Dictionary<char, string>();
    public Dictionary<char, int> tileColorDictionary = new Dictionary<char, int>();
    public float[] tileScaleFactors = {1.733333f, 1.66666666667f, 1.62222222222f, 1.43333333333f, 1.26666666667f, 1f};

    public int[] levelButtonColors;

    public static GameManager Instance {
        get {
            if (_Instance == null) {
                _Instance = new GameObject().AddComponent<GameManager>();
                DontDestroyOnLoad(_Instance.gameObject);
            }
            return _Instance;
        }
    }

    void Awake() {
        //SetUpInstance();

        totalLevels = 1000;
        currentLevel = 1;
        hintsRemaining = 3;
        skipsRemaining = 3;

        adsRemoved = false;
        musicOn = true;
        soundsOn = true;
        hapticsOn = true;
        tutorialShown = false;

        levelsCompleted_5x5 = 0;
        levelsCompleted_6x6 = 0;
        levelsCompleted_7x7 = 0;
        levelsCompleted_8x8 = 0;

        Load();
        CreateTileDictionary();
        CreateTileColorDictionary();
    }

    /*private void SetUpInstance() {
        if (Instance != null && Instance != this) { 
            Destroy(this); 
        } else { 
            Instance = this; 
        }
    }*/

    public void LoadNewScene() {

        if (GameManager.Instance.currentLevelPack == 0) {
            // Load 5x5 scenes
            UnityEngine.SceneManagement.SceneManager.LoadScene(5);
        } else if (GameManager.Instance.currentLevelPack == 1) {
            // Load 6x6 scenes
            UnityEngine.SceneManagement.SceneManager.LoadScene(4);
        } else if (GameManager.Instance.currentLevelPack == 2) {
            // Load 7x7 scenes
            UnityEngine.SceneManagement.SceneManager.LoadScene(3);
        } else if (GameManager.Instance.currentLevelPack == 3) {
            // Load 8x8 scenes
            UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        } else if (GameManager.Instance.currentLevelPack == 4) {
            // Load 9x9 scenes
            UnityEngine.SceneManagement.SceneManager.LoadScene(6);
        } else {
            Debug.Log("Tried to load a level scene that doesn't exist");
        }
    }

    public void Load() {
        
        if (File.Exists(Application.persistentDataPath + file)) {
            // Get the data from the JSON file and put it into a new Save object
            Save save = new Save();
            string json = ReadFromFile(file);
            JsonUtility.FromJsonOverwrite(json, save);

            // Set the GameManager's properties by pulling from this new Save object
            currentLevel = save.currentLevel;
            levelsCompleted_5x5 = save.levelsCompleted_5x5;
            levelsCompleted_6x6 = save.levelsCompleted_6x6;
            levelsCompleted_7x7 = save.levelsCompleted_7x7;
            levelsCompleted_8x8 = save.levelsCompleted_8x8;
            levelsCompleted_9x9 = save.levelsCompleted_9x9;
            hintsRemaining = save.hintsRemaining;
            skipsRemaining = save.skipsRemaining;
            adsRemoved = save.adsRemoved;
            musicOn = save.musicOn;
            soundsOn = save.soundsOn;
            hapticsOn = save.hapticsOn;
            tutorialShown = save.tutorialShown;

            Debug.Log("Game Loaded.");
        } else {
            Debug.Log("No save file found!");
        }
    }

    private string ReadFromFile(string fileName) {
        string path = GetFilePath(fileName);

        if (File.Exists(path)) {
            using (StreamReader reader = new StreamReader(path)) {
                string json = reader.ReadToEnd();
                return json;
            }
        } else {
            Debug.Log("File not found!");
        }

        return "";
    }

    private string GetFilePath(string fileName) {

        return Application.persistentDataPath + "/" + fileName;
    }

    public void SaveAsJSON() {

        // Create a save object from the Game Manager's current data and convert it to JSON
        Save save = CreateSaveGameObject();
        string json = JsonUtility.ToJson(save);

        // Write the JSON string to file
        WriteToFile(file, json);
    }

    private void WriteToFile(string fileName, string json) {

        string path = GetFilePath(fileName);

        // Create a new FileStream, then write the JSON string to it
        FileStream fileStream = new FileStream(path, FileMode.Create);

        using (StreamWriter writer = new StreamWriter(fileStream)) {
            writer.Write(json);
        }
    }

    private Save CreateSaveGameObject() {

        Save save = new Save();

        save.currentLevel = currentLevel;

        save.levelsCompleted_5x5 = levelsCompleted_5x5;
        save.levelsCompleted_6x6 = levelsCompleted_6x6;
        save.levelsCompleted_7x7 = levelsCompleted_7x7;
        save.levelsCompleted_8x8 = levelsCompleted_8x8;
        save.levelsCompleted_9x9 = levelsCompleted_9x9;

        save.hintsRemaining = hintsRemaining;
        save.skipsRemaining = skipsRemaining;
        save.adsRemoved = adsRemoved;

        save.musicOn = musicOn;
        save.soundsOn = soundsOn;
        save.hapticsOn = hapticsOn;
        save.tutorialShown = tutorialShown;

        return save;
    }

    private void OnApplicationPause(bool pause) {
        if (pause)
        SaveAsJSON(); 
    }

    private void CreateTileDictionary() {
        tileDictionary.Add('a', "Tile");
        tileDictionary.Add('A', "Tile");
        tileDictionary.Add('v', "Tile");
        tileDictionary.Add('b', "2 Tile");
        tileDictionary.Add('B', "2 Tile");
        tileDictionary.Add('V', "2 Tile");
        tileDictionary.Add('c', "3 Tile");
        tileDictionary.Add('C', "3 Tile");
        tileDictionary.Add('w', "3 Tile");
        tileDictionary.Add('d', "3 L Tile");
        tileDictionary.Add('D', "3 L Tile");
        tileDictionary.Add('W', "3 L Tile");
        tileDictionary.Add('e', "4 Stick Tile");
        tileDictionary.Add('E', "4 Stick Tile");
        tileDictionary.Add('x', "4 Stick Tile");
        tileDictionary.Add('f', "4 L Tile");
        tileDictionary.Add('F', "4 L Tile");
        tileDictionary.Add('X', "4 L Tile");
        tileDictionary.Add('g', "4 T Tile");
        tileDictionary.Add('G', "4 T Tile");
        tileDictionary.Add('y', "4 T Tile");
        tileDictionary.Add('h', "Square Tile");
        tileDictionary.Add('H', "Square Tile");
        tileDictionary.Add('Y', "Square Tile");
        tileDictionary.Add('i', "Z Tile");
        tileDictionary.Add('I', "Z Tile");
        tileDictionary.Add('z', "Z Tile");
        tileDictionary.Add('j', "5 Stick Tile");
        tileDictionary.Add('J', "5 Stick Tile");
        tileDictionary.Add('Z', "5 Stick Tile");
        tileDictionary.Add('k', "Long L Tile");
        tileDictionary.Add('K', "Long L Tile");
        tileDictionary.Add('=', "Long L Tile");
        tileDictionary.Add('l', "Long Z Tile");
        tileDictionary.Add('L', "Long Z Tile");
        tileDictionary.Add('*', "Long Z Tile");
        tileDictionary.Add('m', "B Tile");
        tileDictionary.Add('M', "B Tile");
        tileDictionary.Add('4', "B Tile");
        tileDictionary.Add('n', "C Tile");
        tileDictionary.Add('N', "C Tile");
        tileDictionary.Add('5', "C Tile");
        tileDictionary.Add('o', "R Tile");
        tileDictionary.Add('O', "R Tile");
        tileDictionary.Add('6', "R Tile");
        tileDictionary.Add('p', "T Tile");
        tileDictionary.Add('P', "T Tile");
        tileDictionary.Add('7', "T Tile");
        tileDictionary.Add('q', "L Tile");
        tileDictionary.Add('Q', "L Tile");
        tileDictionary.Add('8', "L Tile");
        tileDictionary.Add('r', "M Tile");
        tileDictionary.Add('R', "M Tile");
        tileDictionary.Add('9', "M Tile");
        tileDictionary.Add('s', "5 Z Tile");
        tileDictionary.Add('S', "5 Z Tile");
        tileDictionary.Add('<', "5 Z Tile");
        tileDictionary.Add('t', "F Tile");
        tileDictionary.Add('T', "F Tile");
        tileDictionary.Add('>', "F Tile");
        tileDictionary.Add('u', "X Tile");
        tileDictionary.Add('U', "X Tile");
        tileDictionary.Add('+', "X Tile");
    }

    private void CreateTileColorDictionary() {
        tileColorDictionary.Add('a', 1);
        tileColorDictionary.Add('A', 2);
        tileColorDictionary.Add('v', 3);
        tileColorDictionary.Add('b', 1);
        tileColorDictionary.Add('B', 2);
        tileColorDictionary.Add('V', 3);
        tileColorDictionary.Add('c', 1);
        tileColorDictionary.Add('C', 2);
        tileColorDictionary.Add('w', 3);
        tileColorDictionary.Add('d', 1);
        tileColorDictionary.Add('D', 2);
        tileColorDictionary.Add('W', 3);
        tileColorDictionary.Add('e', 1);
        tileColorDictionary.Add('E', 2);
        tileColorDictionary.Add('x', 3);
        tileColorDictionary.Add('f', 1);
        tileColorDictionary.Add('F', 2);
        tileColorDictionary.Add('X', 3);
        tileColorDictionary.Add('g', 1);
        tileColorDictionary.Add('G', 2);
        tileColorDictionary.Add('y', 3);
        tileColorDictionary.Add('h', 1);
        tileColorDictionary.Add('H', 2);
        tileColorDictionary.Add('Y', 3);
        tileColorDictionary.Add('i', 1);
        tileColorDictionary.Add('I', 2);
        tileColorDictionary.Add('z', 3);
        tileColorDictionary.Add('j', 1);
        tileColorDictionary.Add('J', 2);
        tileColorDictionary.Add('Z', 3);
        tileColorDictionary.Add('k', 1);
        tileColorDictionary.Add('K', 2);
        tileColorDictionary.Add('=', 3);
        tileColorDictionary.Add('l', 1);
        tileColorDictionary.Add('L', 2);
        tileColorDictionary.Add('*', 3);
        tileColorDictionary.Add('m', 1);
        tileColorDictionary.Add('M', 2);
        tileColorDictionary.Add('4', 3);
        tileColorDictionary.Add('n', 1);
        tileColorDictionary.Add('N', 2);
        tileColorDictionary.Add('5', 3);
        tileColorDictionary.Add('o', 1);
        tileColorDictionary.Add('O', 2);
        tileColorDictionary.Add('6', 3);
        tileColorDictionary.Add('p', 1);
        tileColorDictionary.Add('P', 2);
        tileColorDictionary.Add('7', 3);
        tileColorDictionary.Add('q', 1);
        tileColorDictionary.Add('Q', 2);
        tileColorDictionary.Add('8', 3);
        tileColorDictionary.Add('r', 1);
        tileColorDictionary.Add('R', 2);
        tileColorDictionary.Add('9', 3);
        tileColorDictionary.Add('s', 1);
        tileColorDictionary.Add('S', 2);
        tileColorDictionary.Add('<', 3);
        tileColorDictionary.Add('t', 1);
        tileColorDictionary.Add('T', 2);
        tileColorDictionary.Add('>', 3);
        tileColorDictionary.Add('u', 1);
        tileColorDictionary.Add('U', 2);
        tileColorDictionary.Add('+', 3);
    }
}
