using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MatchControler : MonoBehaviour{
    [SerializeField] PlayerLife player1Live;
    [SerializeField] PlayerLife player2Live;
    [SerializeField] TMP_Text announceText;

    private string announceMessage = "Player";

    public void AnnounceVictory() {
        if (player1Live.live == 0) {
            announceMessage += " 2 Victory";
        } else if (player2Live.live == 0) {
            announceMessage += " 1 Victory";
        }
        announceText.text = announceMessage;
    }
}
