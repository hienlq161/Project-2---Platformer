using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftboxDrop : MonoBehaviour {

    [SerializeField] float timeTopDrop = 10f;
    [SerializeField] float gravityScale = 2f;

    private Rigidbody2D rigidbodyComponent;

    private void Start() {
        rigidbodyComponent = GetComponent<Rigidbody2D>();
        rigidbodyComponent.gravityScale = 0f;
    }

    private void Update() {
        if (Time.time >= timeTopDrop) {
            rigidbodyComponent.gravityScale = gravityScale;
        }
    }
}
