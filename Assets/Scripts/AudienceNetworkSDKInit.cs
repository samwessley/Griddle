using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudienceNetwork;
using AudienceNetwork.Utility;

public class AudienceNetworkSDKInit : MonoBehaviour {
    
    private void Awake() {
        AudienceNetworkAds.Initialize();

        AudienceNetwork.AdSettings.SetAdvertiserTrackingEnabled(true);
    }
}
