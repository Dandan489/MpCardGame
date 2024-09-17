using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TargetClick : NetworkBehaviour
{
    public PlayerManager PM;

    public void OnTargetClick()
    {
        NetworkIdentity ID = NetworkClient.connection.identity;
        PM = ID.GetComponent<PlayerManager>();

        if (hasAuthority)
        {
            PM.CmdTargetSelfCard();
        }
        else
        {
            PM.CmdTargetOtherCard(gameObject);
        }
    }
}
