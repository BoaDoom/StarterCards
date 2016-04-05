using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerDeckScript : MonoBehaviour {

    List<BaseCard> deck;
    List<BaseCard> cardsInPlay;
    List<BaseCard> discardPile;

    float interations = 0;
    public Vector3 playerStack = new Vector3(0.3f, 0.0f, 0.0f);

    private static System.Random rand = new System.Random();
    int handLimit = 5;

    public void takeCard(BaseCard card)
    {
        deck.Add(card);
    }
    public void takeSetOfCards(List<BaseCard> setOfCards)
    {
        //int i = 0;
        foreach (BaseCard card in setOfCards)
        {
            //
            //card.GetComponent<SpriteRenderer>().sortingOrder = deck.Count;  //setting sprite overlap/soring order
            deck.Add(card);
            //card.transform.Translate(playerStack);  //location moving
            //i++;
        }
    }

    public void dealHand()
    {
        int i = 0;
        while (i < handLimit)
        {
            int randomInt = rand.Next(0, deck.Count);
            cardsInPlay.Add(deck[randomInt]);

            //cardsInPlay[i].transform.position = new Vector3(200+(15.0f * i), 50.0f, 0.0f);  //location moving
            //cardsInPlay[i].GetComponent<SpriteRenderer>().sortingOrder = cardsInPlay.Count;  //setting sprite overlap/soring order
            cardsInPlay[i].TurnOverCard();

            deck.RemoveAt(randomInt);
            i++;
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
        cardsInPlay = new List<BaseCard>();
    }
}
