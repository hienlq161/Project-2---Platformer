using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerLife : MonoBehaviour{
    public int live = 5;

    [SerializeField] GunControl gunControl;
    [SerializeField] TMP_Text liveText;
    [SerializeField] MatchControler mathControler;

    private void Awake() {
        SetLiveText();    
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("KillZone")) {
            if (live == 0) {
                return;
            }
            live--;
            gunControl.myGun.SetGun(WeaponType.handGun);
            SetLiveText();
            if (live == 0) {
                mathControler.AnnounceVictory();
                Destroy(gameObject);
            }
        }
    }

    void SetLiveText() {
        liveText.text = live.ToString();
    }
}
