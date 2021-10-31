using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager: MonoBehaviour {

    private static GameManager _Instance;

    public int currentLevel;
    public int[,,] levelBoardData;

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
        PopulateLevelsData();
    }

    public void PopulateLevelsData() {
        levelBoardData = new int[2,12,12] { {   {0,0,0,0,0,0,0,0,0,0,0,0},
                                            {0,0,0,0,0,1,0,0,0,0,0,0},
                                            {0,0,0,1,0,1,1,0,1,1,0,0},
                                            {0,0,1,1,0,1,0,0,1,1,0,0},
                                            {0,0,0,1,1,0,0,0,0,1,0,0},
                                            {2,0,0,0,0,1,0,0,1,0,0,0},
                                            {2,2,0,0,1,1,1,0,0,1,1,0},
                                            {0,2,2,0,0,1,0,0,0,1,1,0},
                                            {0,1,1,1,1,0,0,1,1,0,0,0},
                                            {0,0,1,0,0,1,1,0,0,1,0,0},
                                            {0,0,0,0,0,0,1,0,0,1,2,0},
                                            {0,0,0,0,0,0,0,0,0,1,2,0}
                                        },
                                        {   {1,1,1,1,1,1,0,0,0,0,0,0},
                                            {0,0,0,0,0,1,0,0,0,0,0,0},
                                            {0,0,0,1,0,1,1,0,1,1,0,0},
                                            {0,0,1,1,0,1,0,0,1,1,0,0},
                                            {0,0,0,1,1,2,2,0,0,1,0,0},
                                            {2,0,0,0,0,1,2,0,1,0,0,0},
                                            {2,2,0,0,1,1,1,0,0,1,1,0},
                                            {0,2,2,0,0,1,0,0,0,1,1,0},
                                            {0,1,1,1,1,0,0,1,1,0,0,0},
                                            {0,0,1,0,0,1,1,0,0,1,0,0},
                                            {0,0,0,0,0,0,1,0,0,1,2,0},
                                            {0,0,0,0,0,0,0,0,0,1,2,0}
                                        }
                                        };
    }
}
