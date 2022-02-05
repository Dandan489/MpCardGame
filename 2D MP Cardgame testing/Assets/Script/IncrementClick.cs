using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class IncrementClick : NetworkBehaviour
{
    public PlayerManager PM;

    public void IncrementClicks()
    {
        NetworkIdentity ID = NetworkClient.connection.identity;
        PM = ID.GetComponent<PlayerManager>();
        PM.CmdIncrementClicks(gameObject);
    }

    [SyncVar]
    public int numberOfClick = 0;
}

