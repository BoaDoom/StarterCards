using UnityEngine;
using System.Collections;

public class DebugDealCards : MonoBehaviour {

    public GameObject cardInPlay;
    
    CardImage cardImage;
    int cardIndex = 0;
    Vector3 start = new Vector3(0f,0f,0f);

    

    void Start()
    {

        cardImage = cardInPlay.GetComponent<CardImage>();
        ShowCards();
    }

    public void ShowCards()
    {
        for (int i = 0; i < cardImage.faces.Length; i++)
        {

            cardImage.cardIndex = cardIndex;
            cardIndex++;
            float displacement = 0.5f * i;
            GameObject cardCopy = (GameObject)Instantiate(cardInPlay);
            Vector3 tempLocal = start + new Vector3(displacement, 0f);
            cardCopy.transform.position = tempLocal;


        }
    }
	
}
