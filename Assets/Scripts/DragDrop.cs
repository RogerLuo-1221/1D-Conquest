using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DragDrop : NetworkBehaviour
{
    public GameObject canvas;
    public PlayerManager playerManager;
    
    private bool _isDragging;
    private GameObject _startParent;
    private Vector2 _startPos;
    private bool _isOverPublicArea;
    private GameObject _dropZone;
    
    public void Start()
    {
        canvas = GameObject.Find("Canvas");

        var networkId = NetworkClient.connection.identity;
        playerManager = networkId.GetComponent<PlayerManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _isOverPublicArea = true;
        _dropZone = collision.gameObject;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _isOverPublicArea = false;
        _dropZone = null;
    }


    public void StartDrag()
    {
        if (!isOwned) return;
        
        _isDragging = true;

        var cardTransform = transform;
        _startParent = cardTransform.parent.gameObject;
        _startPos = cardTransform.position;
    }

    public void EndDrag()
    {
        if (!isOwned) return;
        
        _isDragging = false;

        if (_isOverPublicArea)
        {
            transform.SetParent(_dropZone.transform, false);

            playerManager.CmdPlayCards(gameObject);
        }
        else
        {
            transform.position = _startPos;
            transform.SetParent(_startParent.transform, false);
        }
    }
    
    public void Update()
    {
        if (_isDragging)
        {
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            transform.SetParent(canvas.transform, true);
        }   
            
    }
}
