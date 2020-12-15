using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockPile : Pile
{
    [SerializeField] private Card cardPrefab;
    [SerializeField] private List<Sprite> cardSprites;
    [SerializeField] private Sprite cardBack;
    [SerializeField] private Sprite emptyStock;
    private string[] _suitsNames = { "Spades", "Hearts", "Clubs", "Dimonds" };
    private string[] _ranksNames = { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King" };

    public Action<Card> DrawCard;
    public Action EmptyDeck;

    private void OnMouseDown()
    {
        Ray ray = new Ray(GetMauseWorldPoint(), Vector3.forward);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
        if (pile.Count > 0)
        {
            Card _topCard = PickTopCard();
            DrawCard?.Invoke(_topCard);
            RemoveFromPile(_topCard);
            if (pile.Count == 0) UpdateSprite(emptyStock);
        }
        else
        {
            RedealStock();
        }
    }

    public void RedealStock()
    {
        EmptyDeck?.Invoke();
        ArrangePile();
        if (pile.Count > 0) UpdateSprite(cardBack);
    }    

    public void AddPileToDeck(List<Card> cardPack)
    {
        List<Card> _additionalPile = cardPack;
        pile.AddRange(new List<Card>(cardPack));
        ArrangePile();
    }

    private Vector3 GetMauseWorldPoint()
    {
        Vector3 mousePos = Input.mousePosition;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    public override void ArrangePile()
    {
        base.ArrangePile();
        foreach (Card _card in pile)
        {
            _card.transform.position = transform.position + Vector3.forward;
        }
    }

    private void UpdateSprite(Sprite sprite)
    {
        SpriteRenderer _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = sprite;
    }

    public void ShuffleDeck()
    {
        for (int i = 0; i < pile.Count; i++)
        {
            //UnityEngine.Random.seed = 142;
            UnityEngine.Random.InitState(142);
            int random = UnityEngine.Random.Range(i, pile.Count);
            Card temp = pile[i];
            pile[i] = pile[random];
            pile[random] = temp;
        }
    }

    public void InstantiateDeck()
    {
        UpdateSprite(cardBack);
        foreach (var item in cardSprites)
        {
            var _newCard = Instantiate(cardPrefab, transform.position + Vector3.forward, Quaternion.identity, transform);

            if (item != null)
            {
                _newCard.FaceUp = item;
                if (cardBack != null) _newCard.FaceDown = cardBack;
            }
            if (cardSprites.IndexOf(item) < 13)
            {
                _newCard.Suit = 0;
                _newCard.Rank = cardSprites.IndexOf(item);
                _newCard.name = _ranksNames[cardSprites.IndexOf(item)] + " of " + _suitsNames[0];
            }
            else if (cardSprites.IndexOf(item) >= 13 && cardSprites.IndexOf(item) < 26)
            {
                _newCard.Suit = 1;
                _newCard.Rank = cardSprites.IndexOf(item) - 13;
                _newCard.name = _ranksNames[cardSprites.IndexOf(item) - 13] + " of " + _suitsNames[1];
            }
            else if (cardSprites.IndexOf(item) >= 26 && cardSprites.IndexOf(item) < 39)
            {
                _newCard.Suit = 2;
                _newCard.Rank = cardSprites.IndexOf(item) - 26;
                _newCard.name = _ranksNames[cardSprites.IndexOf(item) - 26] + " of " + _suitsNames[2];
            }
            else if (cardSprites.IndexOf(item) >= 39 && cardSprites.IndexOf(item) < 52)
            {
                _newCard.Suit = 3;
                _newCard.Rank = cardSprites.IndexOf(item) - 39;
                _newCard.name = _ranksNames[cardSprites.IndexOf(item) - 39] + " of " + _suitsNames[3];
            }
            _newCard.Color = _newCard.Suit % 2;
            AddToPile(_newCard);
        }
    }
}
