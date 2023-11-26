using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;


public class Card : NetworkBehaviour
{
    public int id;
    public int value;
    public TMP_Text number;

    public void GetValueById()
    {
        var networkId = NetworkClient.connection.identity;
        
        var playerManager = networkId.GetComponent<PlayerManager>();
        playerManager.CmdGetCardValue(id, gameObject);
    }
    
}
