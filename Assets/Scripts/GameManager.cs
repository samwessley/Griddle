using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager: MonoBehaviour {

    private static GameManager _Instance;

    public int totalLevels;
    public int currentLevel;
    public int[,,] levelBoardData;
    public string[][] levelTiles = new string[3][];

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
        PopulateLevelBoardData();
        PopulateTileData();
        totalLevels = levelTiles.Length;

        if (currentLevel == 0)
        currentLevel = 1;
    }

    private void PopulateTileData() {
        levelTiles[0] = new string[] {"2 Tile", "M Tile", "T Tile", "F Tile", "X Tile", "B Tile"};
        levelTiles[1] = new string[] {"4 L Tile", "5 Z Tile"};
        levelTiles[2] = new string[] {"4 L Tile", "5 Z Tile"};
    }

    private void PopulateLevelBoardData() {
        levelBoardData = new int[3,12,12] {{{0,0,0,0,0,0,0,0,0,0,0,0},
                                            {0,0,0,0,0,1,0,0,0,0,0,0},
                                            {0,0,0,1,0,1,1,0,1,1,0,0},
                                            {1,0,1,1,0,1,3,0,1,1,0,0},
                                            {1,1,0,1,1,0,0,0,0,1,0,0},
                                            {0,1,0,0,0,1,0,0,1,0,0,0},
                                            {3,0,0,0,1,1,1,0,0,1,1,0},
                                            {0,0,0,0,0,1,0,0,0,1,1,0},
                                            {0,1,1,1,1,0,0,1,1,0,0,0},
                                            {0,0,1,0,0,1,1,0,0,1,0,0},
                                            {0,0,0,0,0,0,1,0,0,1,3,0},
                                            {0,0,0,0,0,0,0,0,0,1,0,0}
                                        },
                                        {   {0,1,1,1,0,0,0,0,1,1,1,1},
                                            {0,0,1,0,0,0,0,1,0,1,0,0},
                                            {0,1,0,0,0,0,0,1,1,0,1,1},
                                            {1,1,0,0,0,0,0,0,0,0,0,1},
                                            {1,0,1,0,0,3,0,0,0,0,0,1},
                                            {1,0,1,1,1,0,0,0,0,1,0,1},
                                            {0,0,1,0,0,0,0,0,0,1,1,0},
                                            {0,1,3,0,0,1,0,0,1,0,0,1},
                                            {0,1,1,0,0,0,1,1,1,0,0,1},
                                            {0,0,1,0,0,1,0,0,0,0,0,1},
                                            {0,0,0,0,0,1,1,0,0,0,0,1},
                                            {0,0,1,1,1,0,1,1,0,0,0,0}
                                        },
                                        {   {0,0,0,0,0,0,0,0,0,0,0,0},
                                            {0,0,0,0,0,0,0,0,0,0,0,0},
                                            {0,0,0,0,0,0,0,0,0,0,0,0},
                                            {0,0,0,0,0,0,0,0,0,0,0,0},
                                            {0,0,0,0,0,0,0,0,0,0,0,0},
                                            {0,0,0,0,0,0,0,0,0,0,0,0},
                                            {0,0,0,0,0,0,0,0,0,0,0,0},
                                            {0,0,0,0,0,0,0,0,0,0,0,0},
                                            {0,0,0,0,0,0,0,0,0,0,0,0},
                                            {0,0,0,0,0,0,0,0,0,0,0,0},
                                            {0,0,0,0,0,0,0,0,0,0,0,0},
                                            {0,0,0,0,0,0,0,0,0,0,0,0}
                                        }
                                        };
    }
}
