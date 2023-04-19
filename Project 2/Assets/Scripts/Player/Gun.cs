using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType {
    handGun,
    assaultRifle,
    shotgun,
    snipingRifle
}

public class Gun : MonoBehaviour{

    [Header("HandGun attributes")]
    [SerializeField] float h_bulletSpeed = 40f;
    [SerializeField] float h_timePerFire = 0.5f;
    [SerializeField] float h_knockBackPower = 50f;
    [SerializeField] int h_magazineCapacity = 99999999;


    [Header("AssaultRifle attributes")]
    [SerializeField] float a_bulletSpeed = 40f;
    [SerializeField] float a_timePerFire = 0.1f;
    [SerializeField] float a_knockBackPower = 70f;
    [SerializeField] int a_magazineCapacity = 30;

    [Header("Sniping attributes")]
    [SerializeField] float snp_bulletSpeed;
    [SerializeField] float snp_timePerFire;
    [SerializeField] float snp_knockBackPower;
    [SerializeField] int snp_magazineCapacity;

    [Header("Caching attributes")]
    [SerializeField] Transform barrelEnd;
    [SerializeField] GameObject handGunBullet;
    [SerializeField] GameObject assaultRifleBullet;
    [SerializeField] GameObject snipingBullet;
    [SerializeField] PlayerMovement movementScript;

    private float bulletSpeed;
    private float timePerFire;
    private float knockBackPower;
    private int magazineCapacity;
    private int currentAmmo;
    private GameObject bullet;
    private bool allowFire = true;

    public void SetGun(WeaponType weaponType) {
        if (weaponType == WeaponType.handGun) {
            bulletSpeed = h_bulletSpeed;
            timePerFire = h_timePerFire;
            knockBackPower = h_knockBackPower;
            magazineCapacity = h_magazineCapacity;
            bullet = handGunBullet;
        } else if (weaponType == WeaponType.assaultRifle) {
            bulletSpeed = a_bulletSpeed;
            timePerFire = a_timePerFire;
            knockBackPower = a_knockBackPower;
            magazineCapacity = a_magazineCapacity;
            bullet = assaultRifleBullet;
        } else if (weaponType == WeaponType.shotgun) {
            //not impelemted yet
        } else if (weaponType == WeaponType.snipingRifle) {
            bulletSpeed = snp_bulletSpeed;
            timePerFire = snp_timePerFire;
            knockBackPower = snp_knockBackPower;
            magazineCapacity = snp_magazineCapacity;
            bullet = snipingBullet;
        }
        currentAmmo = magazineCapacity;
    }

    private void Awake() {
        SetGun(WeaponType.handGun);
    }

    public void Fire() {
        if (!allowFire) {
            return;
        }
        if (currentAmmo == 0) {
            return;
        }
        StartCoroutine(FireProcedure());
    }
    private IEnumerator FireProcedure() {
        allowFire = false;
        currentAmmo--;
        GameObject bulletClone;
        Rigidbody2D cloneRigid;
        bulletClone = Instantiate(bullet, barrelEnd.position, barrelEnd.rotation);
        cloneRigid = bulletClone.GetComponent<Rigidbody2D>();
        bulletClone.GetComponent<BulletBehavior>().SetKnockBackPower(knockBackPower);
        if (movementScript.isFacingRight) {
            Vector3 bulletScale = bulletClone.transform.localScale;
            bulletScale.x *= -1;
            bulletClone.transform.localScale = bulletScale;
            cloneRigid.velocity = new Vector2(bulletSpeed, cloneRigid.velocity.y);
        } else {
            cloneRigid.velocity = new Vector2(-bulletSpeed, cloneRigid.velocity.y);
        }
        yield return new WaitForSeconds(timePerFire);
        allowFire = true;
    }
}
