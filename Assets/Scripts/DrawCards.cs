using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DrawCards : NetworkBehaviour
{
    public PlayerManager playerManager;
    
    
    
    public void OnClick()
    {
        var networkId = NetworkClient.connection.identity;
        playerManager = networkId.GetComponent<PlayerManager>();
        playerManager.CmdDealCards();
    }
}
