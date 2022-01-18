using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save {
    public int levelsUnlocked = 1;
    public int currentLevel = 1;
    public int[] stars = new int[] {0,0,0};
    public int hintsRemaining = 3;
    public bool adsRemoved = false;
}
