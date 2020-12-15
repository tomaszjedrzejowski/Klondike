using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FoundationPile : Pile
{
    public Action<bool, int> FoundationCompleted;
    public bool isComplete;

    public override void AddToPile(Card _card)
    {
        base.AddToPile(_card);
        ArrangePile();
    }

    public override void RemoveFromPile(Card _card)
    {
        base.RemoveFromPile(_card);
        ArrangePile();
    }

    public override void ArrangePile()
    {
        Vector3 curentPos;
        Vector3 basePos = transform.position;
        float _OffsetZ = 0.1f;
        base.ArrangePile();
        foreach (Card _card in pile)
        {            
            basePos.z -= _OffsetZ;
            _card.transform.position = basePos;
            curentPos = _card.transform.position;
            basePos = curentPos;
        }
        UpdateFoundationStatus();
    }

    public void UpdateFoundationStatus()
    {
        if (pile.Count == 13) isComplete = true;
        else if (pile.Count < 13) isComplete = false;
        int _foundationSuit = CheckFoundationSuit();
        FoundationCompleted?.Invoke(isComplete, _foundationSuit);
    }

    private int CheckFoundationSuit()
    {
        return PickTopCard().Suit;
    }
}
