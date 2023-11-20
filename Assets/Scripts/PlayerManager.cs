using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerManager : NetworkBehaviour
{
    public GameObject card;
    public GameObject playerArea;
    public GameObject opponentArea;
    public GameObject publicArea;

    private List<GameObject> deck = new List<GameObject>();

    public override void OnStartClient()
    {
        base.OnStartClient();

        playerArea = GameObject.Find("Player Area");
        opponentArea = GameObject.Find("Opponent Area");
        publicArea = GameObject.Find("Public Area");
    }
    
    [Server]
    public override void OnStartServer()
    {
        base.OnStartServer();
        
        deck.Add(card);
    }
    
    [Command]
    public void CmdDealCards()
    {
        for (var i = 0; i < 5; i++)
        {
            var newCard = Instantiate(deck[0], Vector2.zero, Quaternion.identity);
            NetworkServer.Spawn(newCard, connectionToClient);
            
            RpcRenderCards(newCard, "Dealt");
        }
    }

    [Command]
    public void CmdPlayCards(GameObject playedCard)
    {
        RpcRenderCards(playedCard, "Played");
    }

    [ClientRpc]
    private void RpcRenderCards(GameObject newCard, string type)
    {
        switch (type)
        {
            case ("Dealt"):
                newCard.transform.SetParent(isOwned ? playerArea.transform : opponentArea.transform, false);
                break;
            case ("Played"):
                newCard.transform.SetParent(publicArea.transform, false);
                break;
        }
    }
}
