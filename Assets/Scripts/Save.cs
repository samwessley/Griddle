using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save {
    public int[] levelsCompleted_ClassicPack = new int[175];
    public int[] levelsCompleted_BonusPack = new int[175];
    public int[] levelsCompleted_6x6 = new int[175];
    public int[] levelsCompleted_7x7 = new int[175];
    public int[] levelsCompleted_8x8 = new int[175];
    public int[] levelsCompleted_9x9 = new int[175];
    public int currentLevel = 1;
    public int[] stars = new int[] {0,0,0};
    public int hintsRemaining = 3;
    public bool adsRemoved = false;
}
