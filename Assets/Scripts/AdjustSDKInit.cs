using UnityEngine;
using com.adjust.sdk;

public class AdjustExample: MonoBehaviour {

    // Mandatory - set your Android app token here
    [SerializeField] private string appTokenAndroid = "Token Value Here";

    // Mandatory - set your iOS app token here
    [SerializeField] private string appTokenIOS = "zuph8xwbu874";
    
    // Set it to AdjustEnvironment.Sandbox to test in dashboard
    [SerializeField] private AdjustEnvironment environment = AdjustEnvironment.Production;

    // Set it to AdjustLogLevel.Suppress to disable logs from Adjust in device
    [SerializeField] private AdjustLogLevel logLevel = AdjustLogLevel.Info;

    private void Start() {
        #if DEVELOPMENT_BUILD
                environment = AdjustEnvironment.Sandbox;
        #endif

		// This ensures that Adjust is initialized after MAX SDK.
		// So ATT has been displayed and idfa is available.
		if (MaxSdk.IsInitialized())
        InitAdjust();
        else
        MaxSdkCallbacks.OnSdkInitializedEvent += config => InitAdjust();
    }

	private void InitAdjust() {
        #if UNITY_IOS
                InitAdjust(appTokenIOS);
        #elif UNITY_ANDROID
                InitAdjust(appTokenAndroid);
        #endif
	}

    private void InitAdjust(string adjustAppToken) {
        var adjustConfig = new AdjustConfig(adjustAppToken, environment, true);
        adjustConfig.setLogLevel(logLevel); 
        adjustConfig.setSendInBackground(true);
        Adjust.start(adjustConfig);
    }
}
              