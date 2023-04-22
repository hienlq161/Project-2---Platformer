using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControl : MonoBehaviour{
    [SerializeField] PlayerType playerType = PlayerType.Player1;

    [SerializeField] public Gun myGun;


    class InputString {
        public string Shoot = "Shoot";
        public InputString(PlayerType player) {
            if (player == PlayerType.Player1) {
                Shoot += "1";
            }else if (player == PlayerType.Player2) {
                Shoot += "2";
            }
        }
    }

    InputString inputString;
    private void Awake() {
        inputString = new InputString(playerType);
    }
    private void Update() {
        Shoot();
    }
    private void Shoot() {
        if (Input.GetButton(inputString.Shoot)) {
            myGun.Fire();
        }
    }
}
