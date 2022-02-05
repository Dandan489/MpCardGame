using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerManager : NetworkBehaviour
{
    public GameObject card1;
    public GameObject card2;
    public GameObject playerArea;
    public GameObject enemyArea;
    public GameObject dropZone;

    List<GameObject> cards = new List<GameObject>();
    
    public override void OnStartClient()
    {
        base.OnStartClient();

        playerArea = GameObject.Find("PlayerArea");
        enemyArea = GameObject.Find("EnemyArea");
        dropZone = GameObject.Find("DropArea");
    }

    [Server]
    public override void OnStartServer()
    {
        base.OnStartServer();

        cards.Add(card1);
        cards.Add(card2);
        Debug.Log(cards);
    }

    [Command]
    public void CmdDealCards()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject card = Instantiate(cards[Random.Range(0, cards.Count)], Vector2.zero, Quaternion.identity);
            //give Client Authority over the object
            NetworkServer.Spawn(card, connectionToClient);
            RpcShowCard(card, "dealt");
        }
    }

    public void PlayCard(GameObject card)
    {
        CmdPlayCard(card);
    }

    private void CmdPlayCard(GameObject card)
    {
        RpcShowCard(card, "played");

        if (isServer)
        {
            UpdateTurnsPlayed();
        }
    }

    [Server]
    private void UpdateTurnsPlayed()
    {
        GameManager GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        GM.UpdateTurnsPlayed();
        RpcLogToClients("Turns Played: " + GM.TurnsPlayed);
    }

    [ClientRpc]
    void RpcLogToClients(string message)
    {
        Debug.Log(message);
    }

    [ClientRpc]
    private void RpcShowCard(GameObject card, string type)
    {
        if(type == "dealt")
        {
            if (hasAuthority)
            {
                card.transform.SetParent(playerArea.transform, false);
            }
            else
            {
                card.transform.SetParent(enemyArea.transform, false);
                card.GetComponent<CardFlip>().Flip();
            }
        }
        else if(type == "played")
        {
            card.tag = "Neutral";
            card.transform.SetParent(dropZone.transform, false);
            if (!hasAuthority)
            {
                card.GetComponent<CardFlip>().Flip();
            }
        }
    }

    [Command]
    public void CmdTargetSelfCard()
    {
        TargetSelfCard();
    }

    [Command]
    public void CmdTargetOtherCard(GameObject target)
    {
        NetworkIdentity EnemyID = target.GetComponent<NetworkIdentity>();
        TargetOtherCard(EnemyID.connectionToClient);
    }

    [TargetRpc]
    private void TargetSelfCard()
    {
        Debug.Log("targeted by self");
    }

    [TargetRpc]
    private void TargetOtherCard(NetworkConnection target)
    {
        Debug.Log("targeted by other");
    }

    [Command]
    public void CmdIncrementClicks(GameObject card)
    {
        RpcIncrementClicks(card);
    }

    [ClientRpc]
    private void RpcIncrementClicks(GameObject card)
    {
        card.GetComponent<IncrementClick>().numberOfClick++;
        Debug.Log("This card has been clicked " + card.GetComponent<IncrementClick>().numberOfClick + " times.");
    }
}
