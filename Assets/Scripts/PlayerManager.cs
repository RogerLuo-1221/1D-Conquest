using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class PlayerManager : NetworkBehaviour
{
    public GameObject cardPrefab;
    public GameObject playerArea;
    public GameObject opponentArea;
    public GameObject publicArea;

    private List<GameObject> _deck = new List<GameObject>();
    private int _deckIndex;

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
        
        GenerateDeck();
        ShuttleDeck();
    }

    private void GenerateDeck()
    {
        for (var cardValue = 1; cardValue <= 13; cardValue++)
        {
            for (var cardCount = 0; cardCount < 4; cardCount++)
            {
                var card = Instantiate(cardPrefab, Vector2.zero, Quaternion.identity);
                card.transform.GetChild(1).GetComponent<TMP_Text>().text = cardValue.ToString();
                NetworkServer.Spawn(card, connectionToClient);
                
                _deck.Add(card);
            }
        }
    }

    private void ShuttleDeck()
    {
        for (var i = 0; i < 52; i++)
        {
            var swapIndex = Random.Range(0, 52);
            (_deck[i], _deck[swapIndex]) = (_deck[swapIndex], _deck[i]);
        }
    }
    
    [Command]
    public void CmdDealCards()
    {
        for (var i = 0; i < 5; i++)
        {
            RpcRenderCards(NextCardInDeck(), "Deal");
        }
    }
    
    private GameObject NextCardInDeck()
    {
        var returnCard = _deck[_deckIndex];
        _deckIndex++;

        return returnCard;
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
            case ("Deal"):
                
                if (isOwned)
                {
                    newCard.transform.SetParent(playerArea.transform, false);
                    newCard.GetComponent<FlipCards>().FlipCard();
                }
                else
                {
                    newCard.transform.SetParent(opponentArea.transform, false);
                }
                
                break;
            
            case ("Played"):
                
                newCard.transform.SetParent(publicArea.transform, false);
                
                if (isOwned)
                {
                    newCard.GetComponent<FlipCards>().FlipCard();
                } 
                newCard.GetComponent<FlipCards>().AnimatedFlipCard();
                    
                break;
        }
    }
    
}
