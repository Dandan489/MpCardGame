using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    public Card card;

    public TMP_Text cardName;
    public TMP_Text attack;
    public TMP_Text health;
    public TMP_Text cost;
    public TMP_Text description;

    void Start()
    {
        cardName.text = card.cardName;
        attack.text = card.attack.ToString();
        health.text = card.health.ToString();
        cost.text = card.playCost.ToString();
        description.text = card.description;
    }
}
