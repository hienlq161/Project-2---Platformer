using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcquireGun : MonoBehaviour {

    [SerializeField] GunControl gunControlScript;

    private int randomNumber;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Giftbox")) {
            randomNumber = Random.Range(1, 4);
            if (randomNumber == 1) {
                gunControlScript.myGun.SetGun(WeaponType.assaultRifle);
            }else if (randomNumber == 2) {
                gunControlScript.myGun.SetGun(WeaponType.snipingRifle);
            }else if (randomNumber == 3) {
                gunControlScript.myGun.SetGun(WeaponType.shotgun);
            }
        }
    }
}
