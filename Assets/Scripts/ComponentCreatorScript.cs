﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ComponentCreatorScript : MonoBehaviour {

    public Sprite[] allCardFaces;
    public PlayerDeckScript Player1Deck;
    public BaseCard prefabCard;

    //public GameObject positioner;
    public PlayerHandPositioner PlayerHandPositioner;
    public Vector3 deckLocation;

    public Canvas cardCanvas;

    public void takeComponentCode(int code)
    {
        List<BaseCard> tempSetOfCards = new List<BaseCard>();
        switch (code)
        {
            case 1:
                tempSetOfCards = AddCardLoop(0, 5);
                break;
            case 2:
                tempSetOfCards = AddCardLoop(5, 5);
                break;
            case 3:
                tempSetOfCards = AddCardLoop(10, 5);
                break;
        }
        Player1Deck.takeSetOfCards(tempSetOfCards);
    }

    public List<BaseCard> AddCardLoop(int start, int count)
    {
        
        List<BaseCard> tempSetOfCards = new List<BaseCard>();
        for (int i=0; i < count; i++)
        {
            tempSetOfCards.Add(Instantiate(prefabCard));        //making the cards
            tempSetOfCards[i].transform.SetParent(PlayerHandPositioner.transform, false);      //placing the card onto the GUI object panel PlayerHandPanel
            tempSetOfCards[i].startCard(allCardFaces[start + i], i);

            BaseCard tempSingleCard = tempSetOfCards[i];
            PlayerHandPositioner.TakeCard(tempSingleCard);
            
        }
        return tempSetOfCards;
    }

    void Awake()
    {
        //deckLocation = positioner.transform.position;
    }
}
