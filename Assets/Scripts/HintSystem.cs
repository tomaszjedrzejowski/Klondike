using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintSystem : MonoBehaviour
{
    [SerializeField] List<TableauPile> tableaux;
    [SerializeField] List<FoundationPile> foundations;
    [SerializeField] TalonPile talon;
    [SerializeField] private CroupierInspector inspector;

    private List<Hint> availableHints;

    public void CheckPossibleMove()
    {
        availableHints = new List<Hint>();
        Debug.Log(availableHints.Count);

        foreach (var _tableau in tableaux)
        {
            foreach (var _compareTableau in tableaux)
            {
                if (_tableau != _compareTableau)
                {
                    CheckCardToCard(_tableau, _compareTableau);
                    CheckCardToPile(_tableau, _compareTableau);
                }
            }
            foreach (var _foundation in foundations)
            {
                CheckCardToCard(_tableau, _foundation);
                CheckCardToPile(_tableau, _foundation);
            }
        }

        Card _topTalonCard = talon.PickTopCard();
        if (_topTalonCard != null) CheckPossibleTalonMove(_topTalonCard);

        ShowHint();

    }

    private void CheckPossibleTalonMove(Card _topTalonCard)
    {
        foreach (var _compareTableau in tableaux)
        {
            foreach (var _compareCard in _compareTableau.pile)
            {
                if (inspector.ValidCardToCardMove(_compareCard, _topTalonCard))
                {
                    Debug.Log(_topTalonCard.name + " Can be placed on: " + _compareCard);
                    Hint hint = new Hint();
                    hint.ChoosenCard = _topTalonCard;
                    hint.CardSpot = _compareCard;
                    availableHints.Add(hint);
                }
            }
            if (inspector.ValidCardToPileMove(_compareTableau, _topTalonCard))
            {
                Debug.Log(_topTalonCard.name + " Can be placed on: " + _compareTableau);
                Hint hint = new Hint();
                hint.ChoosenCard = _topTalonCard;
                hint.PileSpot = _compareTableau;
                availableHints.Add(hint);
            }
        }
    }

    private void CheckCardToPile(Pile _tableau, Pile _compareTableau)
    {
        foreach (var _card in _tableau.pile)
        {
            if (inspector.ValidCardToPileMove(_compareTableau, _card))
            {
                Debug.Log(_card.name + " Can be placed on: " + _compareTableau);
                Hint hint = new Hint();
                hint.ChoosenCard = _card;
                hint.PileSpot = _compareTableau;
                availableHints.Add(hint);
            }
        }
    }

    private void CheckCardToCard(Pile _tableau, Pile _compareTableau)
    {
        foreach (var _card in _tableau.pile)
        {
            foreach (var _compareCard in _compareTableau.pile)
            {
                if(inspector.ValidCardToCardMove(_compareCard, _card))
                {
                    Debug.Log(_card.name + " Can be placed on: " + _compareCard);
                    Hint hint = new Hint();
                    hint.ChoosenCard = _card;
                    hint.CardSpot = _compareCard;
                    availableHints.Add(hint);
                }
            }
        }
    }


    public void ShowHint()
    {
        Debug.Log(availableHints.Count);
        if(availableHints.Count > 0)
        {
            StartCoroutine(HintVisualisation(availableHints[UnityEngine.Random.Range(0, availableHints.Count)]));
        }
        availableHints.Clear();
    }

    private IEnumerator HintVisualisation(Hint hint)
    {
        hint.ChoosenCard.GetComponent<SpriteRenderer>().color = Color.yellow;
        if(hint.CardSpot) hint.CardSpot.GetComponent<SpriteRenderer>().color = Color.yellow;
        else if (hint.PileSpot) hint.PileSpot.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.5f);
        hint.ChoosenCard.GetComponent<SpriteRenderer>().color = Color.white;
        if (hint.CardSpot) hint.CardSpot.GetComponent<SpriteRenderer>().color = Color.white;
        else if (hint.PileSpot) hint.PileSpot.GetComponent<SpriteRenderer>().color = Color.white;
    }
}

public class Hint
{
    public Card ChoosenCard { get; set; }
    public Card CardSpot { get; set; }
    public Pile PileSpot { get; set; }

}
