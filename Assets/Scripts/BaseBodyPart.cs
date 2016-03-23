using UnityEngine;
using System.Collections;

public class BaseBodyPart : MonoBehaviour {
    //http://unitynoobs.blogspot.com/2011/02/xml-loading-data-from-xml-file.html

    public Sprite[] allCardFacesNum1;
    public Sprite[] allCardFacesNum2;
    public Sprite[] allCardFacesNum3;
    Sprite[] allFaces;
    BaseCard[] allCards;
    public BaseCard tempCard;

    public float incriment = 1.1f;

    void Awake()
    {
    }

    public BaseCard[] MakeCards(int piecePicker)
    {
        switch (piecePicker)    //TODO: swap out switch for ability to grab needed info from table of values
        {
            case 1:
                allFaces = allCardFacesNum1;
                break;
            case 2:
                allFaces = allCardFacesNum2;
                break;
            case 3:
                allFaces = allCardFacesNum3;
                break;
        }
        allCards = new BaseCard[allFaces.Length];
        for (int i=0 ; i < allFaces.Length; i++)
        {
            float displacement = incriment * i;

            allCards[i] = Instantiate(tempCard);
            allCards[i].startCard(allFaces[i], i);
            //Vector3 tempLocal = new Vector3(displacement, 0f);
            //allCards[i].transform.position = tempLocal;
            //allCards[i].TurnOverCard();
            //allCards[i].GetComponent<SpriteRenderer>().sortingOrder = i;
        }
        return allCards;
    }
}
