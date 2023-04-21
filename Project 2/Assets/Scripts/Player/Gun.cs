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

    [Header("Shotgun attributes")]
    [SerializeField] float s_bulletSpeed;
    [SerializeField] float s_timePerFire;
    [SerializeField] float s_knockBackPower;
    [SerializeField] int s_magazineCapacity;
    [SerializeField] int spreadAngle;


    [Header("Caching attributes")]
    [SerializeField] Transform barrelEnd;
    [SerializeField] GameObject handGunBullet;
    [SerializeField] GameObject assaultRifleBullet;
    [SerializeField] GameObject snipingBullet;
    [SerializeField] GameObject shotGunBullet;
    [SerializeField] PlayerMovement movementScript;
    [SerializeField] Sprite handGun;
    [SerializeField] Sprite assaultRifle;
    [SerializeField] Sprite snipingRifle;
    [SerializeField] Sprite shotgun;

    private WeaponType weaponType;
    private float bulletSpeed;
    private float timePerFire;
    private float knockBackPower;
    private int magazineCapacity;
    private int currentAmmo;
    private GameObject bullet;
    private bool allowFire = true;
    private SpriteRenderer spriteRenderer;

    public void SetGun(WeaponType weaponType) {
        if (weaponType == WeaponType.handGun) {
            bulletSpeed = h_bulletSpeed;
            timePerFire = h_timePerFire;
            knockBackPower = h_knockBackPower;
            magazineCapacity = h_magazineCapacity;
            bullet = handGunBullet;
            spriteRenderer.sprite = handGun;
        } else if (weaponType == WeaponType.assaultRifle) {
            bulletSpeed = a_bulletSpeed;
            timePerFire = a_timePerFire;
            knockBackPower = a_knockBackPower;
            magazineCapacity = a_magazineCapacity;
            bullet = assaultRifleBullet;
            spriteRenderer.sprite = assaultRifle;
        } else if (weaponType == WeaponType.shotgun) {
            bulletSpeed = s_bulletSpeed;
            timePerFire = s_timePerFire;
            knockBackPower = s_knockBackPower;
            magazineCapacity = s_magazineCapacity;
            bullet = shotGunBullet;
            spriteRenderer.sprite = shotgun;
        } else if (weaponType == WeaponType.snipingRifle) {
            bulletSpeed = snp_bulletSpeed;
            timePerFire = snp_timePerFire;
            knockBackPower = snp_knockBackPower;
            magazineCapacity = snp_magazineCapacity;
            bullet = snipingBullet;
            spriteRenderer.sprite = snipingRifle;
        }
        this.weaponType = weaponType;
        currentAmmo = magazineCapacity;
    }

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        if (weaponType == WeaponType.shotgun) {
            ShootShotgunBullet();
        } else {
            ShootNormalBullet();
        }
        yield return new WaitForSeconds(timePerFire);
        allowFire = true;
    }
    private void ShootNormalBullet() {
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
    }
    private void ShootShotgunBullet() {
        for (int i = 0; i < 7; i++) {
            GameObject bulletClone;
            Rigidbody2D cloneRigidbody;
            Vector2 forceDirection;
            int randomNumber = Random.Range(-spreadAngle, spreadAngle);
            Quaternion bulletAngle = Quaternion.Euler(0, 0, randomNumber);

            bulletClone = Instantiate(bullet, barrelEnd.position, bulletAngle);

            cloneRigidbody = bulletClone.GetComponent<Rigidbody2D>();
            bulletClone.GetComponent<BulletBehavior>().SetKnockBackPower(knockBackPower);
            forceDirection = new Vector2(Mathf.Cos(bulletAngle.z), Mathf.Sin(bulletAngle.z));
            if (movementScript.isFacingRight) {
                Vector3 bulletScale = bulletClone.transform.localScale;
                bulletScale.x *= -1;
                bulletClone.transform.localScale = bulletScale;
                cloneRigidbody.velocity = bulletSpeed * forceDirection;
            } else {
                forceDirection *= -1;
                cloneRigidbody.velocity = bulletSpeed * forceDirection;
            }
        }
    }
}
