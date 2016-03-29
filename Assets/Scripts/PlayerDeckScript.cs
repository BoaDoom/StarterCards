using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerDeckScript : MonoBehaviour {

    List<BaseCard> deck;
    List<BaseCard> cardsInPlay;
    List<BaseCard> discardPile;

    float interations = 0;

    private static System.Random rand = new System.Random();
    int handLimit = 5;

    public void takeCard(BaseCard card)
    {
        deck.Add(card);
    }
    public void takeSetOfCards(List<BaseCard> setOfCards)
    {
        int i = 0;
        foreach (BaseCard card in setOfCards)
        {
            card.transform.Translate((0.3f * i)+ interations, 0.0f, 0.0f);
            card.GetComponent<SpriteRenderer>().sortingOrder = deck.Count;
            deck.Add(card);
            i++;
        }
        interations += 2;
    }

    public void dealHand()
    {
        int i = 0;
        while (i < handLimit)
        {
            i++;
            int randomInt = rand.Next(0, deck.Count);
            cardsInPlay.Add(deck[randomInt]);
            deck.RemoveAt(randomInt);
        }
    }

    public void showHand()
    {
        foreach (BaseCard card in deck)
        {
            card.TurnOverCard();
        }
    }




    void Awake()
    {
        deck = new List<BaseCard>();
    }
}
