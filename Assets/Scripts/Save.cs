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
    public int[] five_by_five_stars = new int[200];
    public int[] six_by_six_stars = new int[200];
    public int[] seven_by_seven_stars = new int[200];
    public int[] eight_by_eight_stars = new int[200];   
    public int[] nine_by_nine_stars = new int[200]; 
    public int hintsRemaining = 3;
    public int skipsRemaining = 3;
    public bool adsRemoved = false;
    public bool musicOn = false;
    public bool soundsOn = false;
    public bool hapticsOn = false;
}
