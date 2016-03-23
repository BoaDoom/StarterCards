using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MechConstruction : MonoBehaviour {

    public BaseBodyPart bodyParts;
    public Vector3 deckLocation = new Vector3(0f, 0f, 0f);

    List<BaseCard> playersHand;
    BaseCard[] tempCards;
    
    

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 28), "choose deck 1"))
        {
            addPartsCards(1);
        }
        if (GUI.Button(new Rect(120, 10, 100, 28), "choose deck 2"))
        {
            addPartsCards(2);
        }
        if (GUI.Button(new Rect(240, 10, 100, 28), "choose deck 3"))
        {
            addPartsCards(3);
        }

    }

    void addPartsCards(int choice)
    {
        tempCards = bodyParts.MakeCards(choice);
        foreach (BaseCard baseCard in tempCards)
        {
            float displacement = 0.05f * playersHand.Count;
            Vector3 tempLocal = new Vector3(displacement, 0f) + deckLocation;
            baseCard.transform.position = tempLocal;
            baseCard.GetComponent<SpriteRenderer>().sortingOrder = playersHand.Count;
            playersHand.Add(baseCard);
        }
    }
    void Awake()
    {
        playersHand = new List<BaseCard>();
    }
}