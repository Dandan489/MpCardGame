using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DrawCards : NetworkBehaviour
{
    public PlayerManager PM;

    public void OnClick()
    {
        NetworkIdentity ID = NetworkClient.connection.identity;
        PM = ID.GetComponent<PlayerManager>();
        PM.CmdDealCards();
    }
}
