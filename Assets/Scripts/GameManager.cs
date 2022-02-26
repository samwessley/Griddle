using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager: MonoBehaviour {

    private static GameManager _Instance;

    private string file = "/save.txt";

    public int totalLevels;

    public int currentLevel;
    public int levelsUnlocked;
    public int[] stars;
    public int hintsRemaining;
    public bool adsRemoved;

    public string[][] levelTiles = new string[3][];
    public float[] tileScaleFactors = {1.54166666667f, 1f};

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
        totalLevels = levelTiles.Length;
        currentLevel = 1;
        levelsUnlocked = 1;
        stars = new int[] {0,0,0};
        hintsRemaining = 3;
        adsRemoved = false;

        SetLevelButtonColors();
        PopulateTileData();
        Load();
    }

    public void LoadNewScene() {
        // Load new scene
        if (GameManager.Instance.currentLevel == 1)
        UnityEngine.SceneManagement.SceneManager.LoadScene(4);
        else
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }

    private void SetLevelButtonColors() {
        // Give the random generator a manual seed so it generates the same values every time
        Random.InitState(34);

        levelButtonColors = new int[totalLevels];

        // Set each level button color to a random value in the range of possible colors pulling from the same seed
        for (int i = 0; i < totalLevels; i++) {
            levelButtonColors[i] = Random.Range(1,4);
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
            levelsUnlocked = save.levelsUnlocked;
            stars = save.stars;
            hintsRemaining = save.hintsRemaining;
            adsRemoved = save.adsRemoved;

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
        save.levelsUnlocked = levelsUnlocked;
        save.stars = stars;
        save.hintsRemaining = hintsRemaining;
        save.adsRemoved = adsRemoved;

        return save;
    }

    private void OnApplicationPause(bool pause) {
        if (pause)
        SaveAsJSON(); 
    }

    private void PopulateTileData() {
        //levelTiles[0] = new string[] {"2 Tile", "M Tile", "T Tile", "F Tile", "X Tile", "B Tile"};
        levelTiles[0] = new string[] {"2 Tile", "3 Tile", "Tile", "3 L Tile"};
        levelTiles[1] = new string[] {"4 L Tile", "5 Z Tile"};
        levelTiles[2] = new string[] {"4 L Tile", "5 Z Tile"};
    }
}
