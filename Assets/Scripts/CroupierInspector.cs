using System;
using UnityEngine;

public class CroupierInspector : MonoBehaviour
{
    [SerializeField] private FoundationPile[] foundations;
    [SerializeField] private Timer timer;
    private bool[] foundationFlags;
     
    public Pile targetPile;
    public Pile currentPile;

    private int moveCounter = 0;

    public Action<bool> OnGameOver;
    public Action OnRestartGame;
    public Action<float> OnPointsValueChange;
    public Action<string> OnMoveCounterChange;

    private int FoundationBaseCard = 0;
    private int TableauBaseCard = 12;
        
    void Start()
    {
        Card.ValidMoveToCardCheck += ValidCardToCardMove;
        Card.ValidMoveToPileCheck += ValidCardToPileMove;
        Card.MoveToCard += HandleMoveCardToCard;
        Card.MoveToPile += HandleMoveCardToPile;

        foundationFlags = new bool[foundations.Length];
        foreach (FoundationPile _foundation in foundations)
        {           
            _foundation.FoundationCompleted += CheckWinCondition;
        }        
    }

    public void SetUpNewGame()
    {
        timer.ResetTiemr();
        moveCounter = 0;
        UpdateMoveCounter();
        // reset score;
        OnRestartGame?.Invoke();
    }

    private void UpdateMoveCounter()
    {       
        OnMoveCounterChange?.Invoke(moveCounter.ToString());
    }

    private void CheckWinCondition(bool _isCompleted, int suit)
    {   
        foundationFlags[suit] = _isCompleted;
        for (int i = 0; i < foundationFlags.Length; i++)
        {
            bool item = foundationFlags[i];
            if (item == false) return;           
        }  
        OnGameOver?.Invoke(true);
    }

    private void HandleMoveCardToPile(Pile _pile, Card _card)
    {
        moveCounter++;
        UpdateMoveCounter();

        targetPile = _pile;
        currentPile = _card.currentPile;

        targetPile.AddToPile(_card);
        currentPile.RemoveFromPile(_card);

        (targetPile, currentPile) = (null, null);
    }

    private void HandleMoveCardToCard(Card _bottomCard, Card _topCard)
    {        
        targetPile = _bottomCard.currentPile;
        currentPile = _topCard.currentPile;
               
        targetPile.AddToPile(_topCard);
        currentPile.RemoveFromPile(_topCard);
           
        (targetPile, currentPile) = (null, null);
    }

    public bool ValidCardToCardMove(Card _bottomCard, Card _topCard)
    {
        bool _isValid;
        if (PlacedOnFoundation(_bottomCard) && !HasCardAttached(_topCard))
        {
            if (SuitsMatched(_topCard, _bottomCard)
                    && NextInRank(_topCard, _bottomCard)
                    && IsAvailable(_topCard)
                    && IsAvailable(_bottomCard)
                    && _bottomCard.Topper != _topCard)
            {
                _isValid = true;
                moveCounter++;
                UpdateMoveCounter();
            }
            else _isValid = false;
        }
        else if (!ColorMatched(_topCard, _bottomCard)
                    && NextInRank(_bottomCard, _topCard)
                    && IsAvailable(_bottomCard)
                    && IsAvailable(_topCard)
                    && _bottomCard.Topper != _topCard 
                    && !HasCardAttached(_bottomCard))
        {
            _isValid = true;
            moveCounter++;
            UpdateMoveCounter();
        }
        else _isValid = false;
        return _isValid; 
    }

    public bool ValidCardToPileMove(Pile _pile, Card _card)
    {        
        bool _isValid;
        if (_pile == _pile.GetComponent<TableauPile>())
        {
            if (RankMatch(_card, TableauBaseCard) && IsAvailable(_card) && PileIsEmpty(_pile)) _isValid = true;
            else _isValid = false;
        }
        else if(_pile == _pile.GetComponent<FoundationPile>() && !_card.Topper)
        {
            if (RankMatch(_card, FoundationBaseCard) && IsAvailable(_card) && PileIsEmpty(_pile)) _isValid = true;
            else _isValid = false;
        }
        else _isValid = false;
        return _isValid;
    }

    private bool RankMatch(Card _card, int expectedValue)
    {
        if (_card.Rank == expectedValue) return true;
        else return false;
    }

    private bool PileIsEmpty(Pile _pile)
    {
        if (_pile.pile.Count == 0) return true;
        else return false;
    }

    private bool HasCardAttached(Card _card)
    {
        if (_card.Topper) return true;
        else return false;
    }

    private bool PlacedOnFoundation(Card _card)
    {
        if (_card.currentPile.GetComponent<FoundationPile>()) return true;
        else return false;
    }

    private bool SuitsMatched(Card _topCard, Card _bottomCard)
    {
        if (_topCard.Suit == _bottomCard.Suit) return true;
        else return false;
    }

    private bool ColorMatched(Card _baseCard, Card _compareCard)
    {
        if (_baseCard.Color == _compareCard.Color) return true;
        else return false;
    }

    private bool IsAvailable(Card _card)
    {
        if (_card.isFaceUp) return true;
        else return false;
    }

    private bool NextInRank(Card _baseCard, Card _compareCard)
    {
        if (_baseCard.Rank == _compareCard.Rank + 1) return true;
        else return false;
    }
}
