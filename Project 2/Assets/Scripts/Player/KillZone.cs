using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : Respawn
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
            RespawnPlayer();
            //heart--;
        }
    }
}
