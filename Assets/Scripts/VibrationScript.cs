using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationScript : MonoBehaviour {

    public void VibrateLight() {
        if (GameManager.Instance.hapticsOn) {
            Vibration.Init();
            #if UNITY_IOS
            Vibration.VibrateIOS(ImpactFeedbackStyle.Light);
            #endif
            #if UNITY_ANDROID
            Vibration.Vibrate(50);
            #endif
        }
    }
}
