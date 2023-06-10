using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save {
    public int levelsCompleted_5x5 = 0;
    public int levelsCompleted_6x6 = 0;
    public int levelsCompleted_7x7 = 0;
    public int levelsCompleted_8x8 = 0;
    public int levelsCompleted_9x9 = 0;
    public int currentLevel = 1;
    public int[] stars = new int[] {0,0,0};
    public int hintsRemaining = 3;
    public int skipsRemaining = 3;
    public bool adsRemoved = false;
}
