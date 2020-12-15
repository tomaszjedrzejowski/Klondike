using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalonPile : Pile
{
    public override Card PickTopCard()
    {
        if (pile.Count > 0)
        {
            Card _topCard = pile[pile.Count - 1];
            return _topCard;
        }
        else return null;
    }

    public void HandleDrawCard(Card _card)
    {
        AddToPile(_card);
        _card.TurnCard(_card.FaceUp);
        ArrangePile();
    }

    public override void AddToPile(Card _card)
    {
        base.AddToPile(_card);
        _card.transform.parent = transform;
        _card.transform.position = transform.position;
    }

    public override void ArrangePile()
    {
        Vector3 previousPos = this.transform.position;
        Vector3 curentPos;
        float _OffsetZ = 0.1f;

        foreach (Card card in pile)
        {
            previousPos.z -= _OffsetZ;
            card.transform.position = previousPos;
            curentPos = card.transform.position;
            previousPos = curentPos;
        }
    }
}
