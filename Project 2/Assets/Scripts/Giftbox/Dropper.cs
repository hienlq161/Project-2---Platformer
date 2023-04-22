using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour {

    [SerializeField] float delayTime = 10f;
    
    [SerializeField] GameObject giftBox;

    private float timeCounter = 0;
    private void Awake() {
        timeCounter = 0;
    }
    private void Update() {
        timeCounter += Time.deltaTime;
        if (timeCounter >= delayTime) {
            timeCounter = 0;
            Instantiate(giftBox, transform.position, transform.rotation);
        }
    }
}
