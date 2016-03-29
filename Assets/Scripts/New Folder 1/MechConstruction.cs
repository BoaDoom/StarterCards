using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MechConstruction : MonoBehaviour {

    public BaseBodyPart bodyParts;
    List<BaseCard> playersHand = new List<BaseCard>();

    //void OnGUI()
    //{
    //    if (GUI.Button(new Rect(10, 10, 100, 28), "choose deck 1"))
    //    {
    //        addPartsCards(1);
    //    }
    //    if (GUI.Button(new Rect(120, 10, 100, 28), "choose deck 2"))
    //    {
    //        addPartsCards(2);
    //    }
    //    if (GUI.Button(new Rect(240, 10, 100, 28), "choose deck 3"))
    //    {
    //        addPartsCards(3);
    //    }

    //}

    //void addPartsCards(int choice)
    //{
    //    BaseCard[] tempCards = bodyParts.MakeCards(choice);
    //    foreach (BaseCard baseCard in tempCards)
    //    {
    //        playersHand.Add(baseCard);
    //    }
    //}

    //public List<BaseCard> 
    public void MakePlayersHand()
    {
        List<BaseCard> tempCards;

        tempCards = bodyParts.MakeCards(1);
        foreach (BaseCard baseCard in tempCards)
        {
            playersHand.Add(baseCard);
        }
        tempCards = bodyParts.MakeCards(2);
        foreach (BaseCard baseCard in tempCards)
        {
            playersHand.Add(baseCard);
        }
        tempCards = bodyParts.MakeCards(3);
        foreach (BaseCard baseCard in tempCards)
        {
            playersHand.Add(baseCard);
        }
        //return playersHand;
    }
    void Awake()
    {
        ;
    }
}