using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Deck))]
public class DeckView : MonoBehaviour 
{
    Deck deck;

    public Vector3 start;
    public float cardOffset;
    public GameObject cardPrefab;

    void Start()
    {
        deck = GetComponent<Deck>();
        ShowCards();
    }

    void ShowCards()
    {
        int cardCount = 0;

        foreach(int i in deck.GetCards())
        {
            float co = cardOffset * cardCount;

            GameObject cardCopy = (GameObject)Instantiate(cardPrefab);
            Vector3 temp = start + new Vector3(co, 0f);
            cardCopy.transform.position = temp;

            CardModel cardModel = cardCopy.GetComponent<CardModel>();
            cardModel.cardIndex = i;
            cardModel.ToggleFace(true);

            cardCount++;
        }
    }
}
