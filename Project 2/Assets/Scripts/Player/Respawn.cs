using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : PlayerMovement
{
    public GameObject respawnPoint;

    public void RespawnPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = lastGroundPosition;
    }
}
