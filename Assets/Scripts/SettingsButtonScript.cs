using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsButtonScript : MonoBehaviour {

    [SerializeField] GameObject restorePurchases = null;
    [SerializeField] GameObject resetProgress = null;
    [SerializeField] GameObject vibrationsToggle = null;
    [SerializeField] GameObject soundsToggle = null;
    [SerializeField] GameObject settingsButton = null;

    public void CloseSettingsScene() {
        StartCoroutine(CloseSettingsScreenAnimation());
    }

    IEnumerator CloseSettingsScreenAnimation() {
        LeanTween.moveX(restorePurchases, -4f,0.08f);
        yield return new WaitForSeconds(0.05f);

        LeanTween.moveX(resetProgress, -4f,0.08f);    
        yield return new WaitForSeconds(0.05f);

        LeanTween.moveX(vibrationsToggle, -4f,0.08f);    
        yield return new WaitForSeconds(0.05f);

        LeanTween.moveX(soundsToggle, -4f,0.08f);    
        yield return new WaitForSeconds(0.05f);

        LeanTween.rotateZ(settingsButton, 0,0.1f);
        yield return new WaitForSeconds(0.1f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
