using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftboxCollision : MonoBehaviour {
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            Destroy(gameObject, 8f);
            return;
        }
        Destroy(gameObject);
    }
}
