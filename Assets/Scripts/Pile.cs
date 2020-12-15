using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pile : MonoBehaviour
{
    public List<Card> pile;

    public virtual void AddToPile(Card _card)
    {
        pile.Add(_card);
        _card.currentPile = gameObject.GetComponent<Pile>();
    }

    public virtual void RemoveFromPile(Card _card)
    {
        pile.Remove(_card);
    }

    public virtual Card PickTopCard()
    {
        Card _topCard = pile[0];
        return _topCard;
    }

    public virtual void ArrangePile()
    {
        foreach (Card _card  in pile)
        {
            _card.transform.parent = transform;
            _card.transform.position = transform.position;
        }
    }

    public virtual List<Card> TakePile()
    {
        List<Card> cardPack = new List<Card>(pile);
        pile.Clear();
        return cardPack;
    }
}
