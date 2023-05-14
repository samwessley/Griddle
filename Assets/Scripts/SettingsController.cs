using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsController : MonoBehaviour {

    [SerializeField] GameObject restorePurchases = null;
    [SerializeField] GameObject resetProgress = null;
    [SerializeField] GameObject vibrationsToggle = null;
    [SerializeField] GameObject soundsToggle = null;
    [SerializeField] GameObject backButton = null;

    // Start is called before the first frame update
    void Start() {
        restorePurchases.transform.position = new Vector3(-4f,restorePurchases.transform.position.y,restorePurchases.transform.position.z);
        resetProgress.transform.position = new Vector3(-4f,resetProgress.transform.position.y,resetProgress.transform.position.z);
        vibrationsToggle.transform.position = new Vector3(-4f,vibrationsToggle.transform.position.y,vibrationsToggle.transform.position.z);
        soundsToggle.transform.position = new Vector3(-4f,soundsToggle.transform.position.y,soundsToggle.transform.position.z);

        StartCoroutine(SettingsScreenAnimation());
    }

    IEnumerator SettingsScreenAnimation() {
        LeanTween.rotateZ(backButton, -60f,0.1f);
        yield return new WaitForSeconds(0.1f);

        LeanTween.moveX(soundsToggle, -1.836493f,0.08f);
        yield return new WaitForSeconds(0.05f);
        
        LeanTween.moveX(vibrationsToggle, -1.836493f,0.08f);
        yield return new WaitForSeconds(0.05f);

        LeanTween.moveX(resetProgress, -1.836493f,0.08f);
        yield return new WaitForSeconds(0.05f);

        LeanTween.moveX(restorePurchases, -1.836493f,0.08f);
    }
}
