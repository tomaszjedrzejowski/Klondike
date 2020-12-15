using System;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int Color { get; set; }
    public int Suit { get; set; }
    public int Rank { get; set; }

    public Sprite FaceUp { get; set; }
    public Sprite FaceDown { get; set; }

    public Pile currentPile { get; set; }
    public bool isFaceUp { get; private set; }

    private Vector3 _Offset;
    private Vector3 startingPos;

    public static Func<Pile, Card, bool> ValidMoveToPileCheck;
    public static Func<Card, Card, bool> ValidMoveToCardCheck;
    public static Action<Card, Card> MoveToCard;
    public static Action<Pile, Card> MoveToPile;

    public Card Topper { get; set; }

    public void TurnCard(Sprite side)
    {
        SpriteRenderer _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = side;
        if (side == FaceUp) isFaceUp = true;
        else isFaceUp = false;
    }

    public void HoldChain(String layer, bool colliderEnable)
    {
        if (Topper != null)
        {    
            Topper.GetComponent<SpriteRenderer>().sortingLayerName = layer;
            Topper.GetComponent<Collider2D>().enabled = colliderEnable;
            Topper.HoldChain(layer, colliderEnable);
        }
        else return;
    }       

    public void PlaceChain()
    {
        if (Topper != null)
        {
            Topper.PlaceOnCard(GetComponent<Card>(), Topper);
            Topper.PlaceChain();
        }
        else return;
    }

    public void SetTopper()
    {
        if (transform.childCount > 0)
        {
            Topper = transform.GetChild(0).GetComponent<Card>();
            Topper.SetTopper();
        }
        else Topper = null;
    }

    private void OnMouseDown()
    {        
        if (isFaceUp && Input.GetMouseButtonDown(0))
        {
            SetTopper();
            startingPos = transform.position;
            GetComponent<SpriteRenderer>().sortingLayerName = "Hold";            
            GetComponent<Collider2D>().enabled = false;
            HoldChain("Hold", false);
            _Offset = gameObject.transform.position - GetMauseWorldPoint();                
        }
    }

    private void OnMouseDrag()
    {
        if (isFaceUp) transform.position = GetMauseWorldPoint() + _Offset;        
    }

    private void OnMouseUp()
    {
        if (isFaceUp && Input.GetMouseButtonUp(0))
        { 
            MoveCard();
        }
        else return;
    }

    private void MoveCard()
    {
        Ray ray = new Ray(GetMauseWorldPoint(), new Vector3(0, 0, 1));
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
        Card placebleCard;
        Pile placeblePile;
        Card thisCard = GetComponent<Card>();

        if (hit && hit.collider.GetComponent<Card>() != null)
        {
            placebleCard = hit.collider.GetComponent<Card>();
            if (ValidMoveToCardCheck(placebleCard, thisCard))
            {
                PlaceOnCard(placebleCard, thisCard);
                PlaceChain();
            }
            else transform.position = startingPos;
        }
        else if(hit && hit.collider.GetComponent<Pile>() != null)
        {
            placeblePile = hit.collider.GetComponent<Pile>();
            if (ValidMoveToPileCheck(placeblePile, thisCard))
            {
                PlaceOnPil(placeblePile, thisCard);
                PlaceChain();
            }
            else transform.position = startingPos;
        }
        else
        {
            transform.position = startingPos;
        }
        GetComponent<SpriteRenderer>().sortingLayerName = "Default";
        GetComponent<Collider2D>().enabled = true;
        HoldChain("Default", true);
    }

    public void PlaceOnPil(Pile placeblePile, Card thisCard)
    {
        MoveToPile?.Invoke(placeblePile, thisCard);
    }

    public void PlaceOnCard(Card placebleCard, Card thisCard)
    {
        MoveToCard?.Invoke(placebleCard, thisCard);
    }

    private Vector3 GetMauseWorldPoint()
    {
        Vector3 mousePos = Input.mousePosition;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
