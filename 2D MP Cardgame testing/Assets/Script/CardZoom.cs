using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class CardZoom : NetworkBehaviour
{
    public GameObject canvas;
    public GameObject ZoomCard;

    private GameObject zoomCard;
    private Sprite zoomSprite;

    private void Awake()
    {
        canvas = GameObject.Find("Canvas");
        zoomSprite = gameObject.GetComponent<Image>().sprite;
    }

    public void OnHoverEnter()
    {
        if (!hasAuthority && gameObject.tag!= "Neutral") return;

        zoomCard = Instantiate(ZoomCard, new Vector2(Input.mousePosition.x, Input.mousePosition.y + 250f), Quaternion.identity);
        zoomCard.GetComponent<Image>().sprite = zoomSprite;
        zoomCard.transform.SetParent(canvas.transform, true);
        RectTransform rect = zoomCard.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(650f, 912f);
    }

    public void OnHoverExit()
    {
        Destroy(zoomCard);
    }
}
