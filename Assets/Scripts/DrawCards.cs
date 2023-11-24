using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.Serialization;

public class DrawCards : NetworkBehaviour
{
    [SerializeField] private PlayerManager _playerManager;

    public void OnClick()
    {
        var networkId = NetworkClient.connection.identity;
        _playerManager = networkId.GetComponent<PlayerManager>();
        _playerManager.CmdDealCards();
    }
}
