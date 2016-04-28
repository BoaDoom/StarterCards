using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerHandPositioner : MonoBehaviour {
    //public int numberOfCards;
    //List<BaseCard> children = new List<BaseCard>();

    //void updateCards()
    //{
    //    //children = null;
    //    foreach (BaseCard child in transform)
    //    {
    //        BaseCard tempChild = child;
    //        tempChild.UpdatePosition();
    //    }
    //}

    public void TakeCard(BaseCard tempCard)
    {
        //BaseCard tempCard = baseCard;
        //listOfCards.Add(tempCard);
        tempCard.transform.GetChild(0).SetParent(this.transform, false);      //placing the card's only child, its positioning object, back into the PlayerHandPanel
        tempCard.GetComponent<SpriteRenderer>().sortingOrder = 1;
        //numberOfCards = this.transform.childCount;
        //updateCards();
    }

    //void Update()
    //{
    //    if (numberOfCards != this.transform.childCount)
    //    {
    //        updateCards();
    //        numberOfCards = this.transform.childCount;
    //    }
    //}
    void Start()
    {
        //listOfCards = new List<BaseCard>();
    }
}

