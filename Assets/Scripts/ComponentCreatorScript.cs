using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ComponentCreatorScript : MonoBehaviour {

    public PlayerDeckScript Player1Deck;
    public Sprite[] allCardFaces;
    public BaseCard prefabCard = new BaseCard();

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
            tempSetOfCards.Add(Instantiate(prefabCard));
            tempSetOfCards[i].startCard(allCardFaces[start+i], i);
            tempSetOfCards[i].transform.position = new Vector3(-4.11f, 0.0f, 0.0f);
        }
        return tempSetOfCards;
    }
}
