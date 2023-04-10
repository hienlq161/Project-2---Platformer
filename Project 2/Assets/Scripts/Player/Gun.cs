using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType {
    handGun,
    assaultRifle,
    shotgun,
    snipingRifle
}

public class Gun{
    private float bulletSpeed;
    private float timePerFire;
    private float knockBackPower;
    private int magazineCapacity;
    WeaponType weaponType;

    public Gun(WeaponType weaponType) {
        if (weaponType == WeaponType.handGun) {
            bulletSpeed = 50f;
            timePerFire = 0.5f;
            knockBackPower = 50f;
            magazineCapacity = 999999999;
        } else if (weaponType == WeaponType.assaultRifle) {
            bulletSpeed = 50f;
            timePerFire = 0.1f;
            knockBackPower = 70f;
            magazineCapacity = 35;
        } else if (weaponType == WeaponType.shotgun) {

        } else if (weaponType == WeaponType.snipingRifle) {

        }
    }

    public WeaponType GetWeaponType() {
        return this.weaponType;
    }
}
