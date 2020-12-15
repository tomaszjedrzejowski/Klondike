using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CroupierDealer : MonoBehaviour
{
    [SerializeField] CroupierInspector inspector;
    [SerializeField] List<TableauPile> tableaux;
    [SerializeField] List<FoundationPile> foundations;
    [SerializeField] StockPile stock;
    [SerializeField] TalonPile talon;

    private void Awake()
    {
        inspector.OnRestartGame += HandleRestartGame;
        stock.DrawCard += talon.HandleDrawCard;
        stock.EmptyDeck += HandleEmptyDeck;
    }

    void Start()
    {
        stock.InstantiateDeck();
        stock.ShuffleDeck();
        DealReserve();
    }

    private void HandleRestartGame()
    {
        RedealTable();
        stock.RedealStock();
        stock.ShuffleDeck();
        DealReserve();        
    }

    private void RedealTable()
    {
        foreach (Pile _foundation in foundations)
        {
            stock.AddPileToDeck(_foundation.TakePile());
        }
        foreach (Pile _tableau in tableaux)
        {
            stock.AddPileToDeck(_tableau.TakePile());
        }
    }

    private void HandleEmptyDeck()
    {
        Redeal();
    }

    private void Redeal()
    {
        stock.AddPileToDeck(talon.TakePile());
    }    

    private void DealReserve()
    {
        foreach (TableauPile tableau in tableaux)
        {
            int _cardsToDeal = tableaux.IndexOf(tableau) + 1;

            for (int i = 0; i < _cardsToDeal; i++)
            {
                Card _topCard = stock.PickTopCard();
                stock.RemoveFromPile(_topCard);
                tableau.AddToPile(_topCard);
                _topCard.TurnCard(_topCard.FaceDown);
                if (i == (_cardsToDeal - 1))
                {
                    _topCard.TurnCard(_topCard.FaceUp);
                }
            }
        }
    }
}
