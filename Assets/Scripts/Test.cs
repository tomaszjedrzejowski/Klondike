using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Test : MonoBehaviour
{
    public static Action<Card> DealToTalon; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            DealTopCardToTalon();
        }
    }

    private void OnMouseDown()
    {
        DealTopCardToTalon();
    }   

    private void DealTopCardToTalon()
    {
        Ray ray = new Ray(GetMauseWorldPoint(), new Vector3(0, 0, 1));
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
        Debug.Log(hit.collider.gameObject.name);

        Pile m_pile = GetComponent<StockPile>();
        Card _topCard = m_pile.PickTopCard();
        DealToTalon(_topCard);
        m_pile.RemoveFromPile(_topCard);
    }

    private Vector3 GetMauseWorldPoint()
    {
        Vector3 mousePos = Input.mousePosition;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
