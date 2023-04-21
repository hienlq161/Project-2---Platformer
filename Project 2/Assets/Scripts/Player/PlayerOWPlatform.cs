using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOWPlatform : MonoBehaviour{
    [SerializeField] PlayerType playerType;
    
    [SerializeField] private BoxCollider2D playerCollider;
    
    private GameObject currentOWPlatform;
    private InputString inputString;

    private class InputString {
        public string Down = "Down";
        public InputString(PlayerType playerType) {
            if (playerType == PlayerType.Player1) {
                Down += "1";
            }else if(playerType == PlayerType.Player2) {
                Down += "2";
            }
        }
    }

    private void Awake() {
        inputString = new InputString(playerType);
    }

    void Update(){
        if (Input.GetButtonDown(inputString.Down))
        {
            if (currentOWPlatform != null)
            {
                StartCoroutine(DisableCollision());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            currentOWPlatform = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            currentOWPlatform = null;
        }
    }

    private IEnumerator DisableCollision()
    {
        BoxCollider2D platformCollider = currentOWPlatform.GetComponent<BoxCollider2D>();

        Physics2D.IgnoreCollision(playerCollider, platformCollider);
        yield return new WaitForSeconds(0.25f);
        Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
    }
}
