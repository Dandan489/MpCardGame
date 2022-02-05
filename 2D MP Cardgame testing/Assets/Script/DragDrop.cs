using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DragDrop : NetworkBehaviour
{
    private GameObject mainCanvas;
    public PlayerManager PM;

    private bool isDragging = false;
    private bool isDraggable = true;
    private GameObject startParent;
    private Vector2 startPos;
    private GameObject dropZone;
    private bool isOverDropArea;

    private void Start()
    {
        mainCanvas = GameObject.Find("Canvas");
        if (!hasAuthority)
        {
            isDraggable = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isOverDropArea = true;
        dropZone = collision.gameObject;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isOverDropArea = false;
        dropZone = null;
    }
    public void Start_Drag()
    {
        if (!isDraggable) return;
        isDragging = true;
        startParent = transform.parent.gameObject;
        startPos = transform.position;
    }

    public void End_Drag()
    {
        if (!isDraggable) return;
        isDragging = false;
        if (isOverDropArea)
        {
            transform.SetParent(dropZone.transform, false);
            gameObject.tag = "Neutral";
            isDraggable = false;
            NetworkIdentity ID = NetworkClient.connection.identity;
            PM = ID.GetComponent<PlayerManager>();
            PM.PlayCard(gameObject);
        }
        else
        {
            transform.position = startPos;
            transform.SetParent(startParent.transform,false);
        }
    }

    void Update()
    {
        if (isDragging)
        {
            gameObject.transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            transform.SetParent(mainCanvas.transform, true);
        }
    }
}
