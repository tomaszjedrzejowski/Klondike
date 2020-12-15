using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableauPile : Pile
{
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
        Transform previousParent = transform;
        Transform curentParent;
        Vector3 previousPos = transform.position;
        Vector3 curentPos;
        float _OffsetY = 0f;
        float _OffsetZ = 0.1f;

        foreach (Card card in pile)
        {
            previousPos.y -= _OffsetY;
            previousPos.z -= _OffsetZ;
            card.transform.position = previousPos;
            curentPos = card.transform.position;
            previousPos = curentPos;
            if (pile.Count <= 7) _OffsetY = 0.75f;
            else if (pile.Count >= 10) _OffsetY = 0.55f;
            else _OffsetY = 0.65f;

            card.transform.parent = previousParent;
            curentParent = card.transform;
            previousParent = curentParent;

            if(pile.IndexOf(card) == pile.Count - 1 && !card.isFaceUp)
            {
                card.TurnCard(card.FaceUp);
            }
        }
    }
}
