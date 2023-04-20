using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour{

    private float knockBackPower;
    private Vector2 originPosition;
    private Vector2 currentPosition;
    private Vector2 movingDirection;
    private Rigidbody2D playerRigidbody;
    private void OnEnable() {
        originPosition = transform.position;
    }
    private void Update() {
        currentPosition = transform.position;
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            Debug.Log("Touching Map border\n");
            return;
        }
        if (collision.gameObject.CompareTag("Player")) {
            Debug.Log("Touching Player\n");
            KnockBackPlayer(collision);
        }
        Destroy(gameObject);
    }

    private void KnockBackPlayer(Collider2D collision) {
        movingDirection = (currentPosition - originPosition).normalized;
        playerRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
        playerRigidbody.constraints = RigidbodyConstraints2D.FreezePositionY;
        playerRigidbody.AddForce(movingDirection * knockBackPower);
        playerRigidbody.constraints &= ~RigidbodyConstraints2D.FreezePositionY; // unfreeze position Y
    }

    public void SetKnockBackPower(float value) {
        knockBackPower = value;
    }
}
