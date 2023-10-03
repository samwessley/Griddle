using Firebase;
using Firebase.Extensions;
using Firebase.Analytics;
using UnityEngine;

public class FirebaseInit : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
        });
    }
}
