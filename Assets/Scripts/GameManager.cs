using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager GM;
    public int currentLevel;
    public int[,,] levelBoardData = new int[2,12,12];

    void Awake() {

        // Set a singleton instance of the GameManager object, and if one already exists, destroy it.
        if (GM == null) {
            GM = this;
            DontDestroyOnLoad(this);
        }
        // If GM is not 'this', there is more than one instance. Destroy it.
        else if (GM != this) {
            Destroy(gameObject);
        }

        PopulateLevelsData();
    }

    private void PopulateLevelsData() {
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
