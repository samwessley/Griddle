using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonSpawnerScript : MonoBehaviour {

    [SerializeField] GameObject balloonSpawner = null;
    
    void Start() {
        GenerateBalloons();
    }

    void GenerateBalloons() {
        bool direction = false;
        for (int i = 0; i < 36; i++) {
            int balloonNumber = Random.Range(1,4);
            GameObject balloon = Instantiate(Resources.Load<GameObject>("Prefabs/Balloon " + balloonNumber));
            balloon.transform.SetParent(balloonSpawner.transform);

            float randomScale = Random.Range(0.3f, 1f);
            balloon.transform.localScale = new Vector3(randomScale,randomScale,randomScale);

            float randomYPos = Random.Range(-24750,24750);
            float randomXPos = 0;
            if (direction) {
                randomXPos = Random.Range(-200, 100);
            } else {
                randomXPos = Random.Range(1200, 1900);
            }

            direction = !direction;
            balloon.transform.localPosition = new Vector3(randomXPos,randomYPos, 1 - randomScale);

            StartCoroutine(AnimateBalloon(balloon, direction));
        }
    }

    IEnumerator AnimateBalloon(GameObject balloon, bool direction) {
        int time = Random.Range(50,70);

        if (!direction) {
            while (true) {
                LeanTween.moveX(balloon, 3.6f, time);
                yield return new WaitForSeconds(time);
                LeanTween.moveX(balloon, -3.6f, 0);
            }
        } else {
            while (true) {
                LeanTween.moveX(balloon, -3.6f, time);
                yield return new WaitForSeconds(time);
                LeanTween.moveX(balloon, 3.6f, 0);
            }
        }
    }

}
