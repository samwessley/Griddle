using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxSDKInit : MonoBehaviour {
    
    void Awake() {
        MaxSdkCallbacks.OnSdkInitializedEvent += (MaxSdkBase.SdkConfiguration sdkConfiguration) => {
            // AppLovin SDK is initialized, start loading ads
        };

        MaxSdk.SetSdkKey("ZMlAiSNPabibq4lz8eVg_ZUJPyv2uZo21LD7b3TtkmPGw0vGhTO49fnxQTrpfLpSd8JQD49mohLUqfzO3df4Nv");
        //MaxSdk.SetUserId("USER_ID");
        MaxSdk.InitializeSdk();
    }
}
