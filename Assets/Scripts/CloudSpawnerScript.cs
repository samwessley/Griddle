using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawnerScript : MonoBehaviour {

    [SerializeField] GameObject cloudSpawner = null;
    
    void Start() {
        StartCoroutine(AnimateClouds());
    }

    void GenerateClouds(int round) {
        for (int i = 0; i < 60; i++) {
            int cloudNumber = Random.Range(1,5);
            GameObject cloud = Instantiate(Resources.Load<GameObject>("Prefabs/Cloud " + cloudNumber));
            //cloud.transform.parent = cloudSpawner.transform;
            cloud.transform.SetParent(cloudSpawner.transform);
            cloud.transform.localScale = new Vector3(1,1,1);

            float randomYPos = Random.Range(-24750,24750);
            float randomXPos = 0;
            if (round == 1) {
                randomXPos = Random.Range(-1600, 800); 
            } else {
                randomXPos = Random.Range(-1700, -950);            
            }

            cloud.transform.localPosition = new Vector3(randomXPos,randomYPos,0);

            StartCoroutine(AnimateCloud(cloud));
        }
    }

    IEnumerator AnimateClouds() {
        GenerateClouds(1);
        yield return new WaitForSeconds(7);
        GenerateClouds(2);
        yield return new WaitForSeconds(7);
        GenerateClouds(3);
    }

    IEnumerator AnimateCloud(GameObject cloud) {
        int time = Random.Range(70,100);
        while (true) {
            LeanTween.moveX(cloud, 8f, time);
            yield return new WaitForSeconds(time);

            float randomXPos = Random.Range(-16f, -19f);
            LeanTween.moveX(cloud, randomXPos, 0);
        }
        //Destroy(cloud);
    }

}
