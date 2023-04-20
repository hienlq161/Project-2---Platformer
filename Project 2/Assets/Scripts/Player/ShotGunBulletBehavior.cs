using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunBulletBehavior : MonoBehaviour{

    [SerializeField] float delayTime = 0f;

    private void OnEnable() {
        Destroy(gameObject, delayTime);
    }
}
