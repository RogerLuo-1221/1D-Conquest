using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    public GameObject canvas;   
    
    private bool _isDragging;
    
    public void Start()
    {
        canvas = GameObject.Find("Canvas");
    }

    public void StartDrag()
    {
        _isDragging = true;
    }

    public void EndDrag()
    {
        _isDragging = false;
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
