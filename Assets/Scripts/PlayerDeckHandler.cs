using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PlayerDeckHandler : MonoBehaviour {

    public MechConstruction construction;
    List<BaseCard> playersHand = new List<BaseCard>();
    List<BaseCard> tempHand = new List<BaseCard>();

    public Vector3 deckLocation = new Vector3(0f, 0f, 0f);

    public void Shuffle()
    {
        int i = 0;
        while (playersHand.Count > 0)
        {
            int randomPick = Random.Range(0, playersHand.Count-1);
            BaseCard temp = playersHand[randomPick];
            playersHand.RemoveAt(randomPick);
            tempHand.Add(temp);

            float displacement = 0.15f * tempHand.Count;
            Vector3 tempLocal = new Vector3(displacement, 0f) + deckLocation;
            tempHand[i].transform.position = tempLocal;
            tempHand[i].GetComponent<SpriteRenderer>().sortingOrder = tempHand.Count;
            i++;
            for (int e = 0; e < playersHand.Count; e++)
            {
                playersHand[i].TurnOverCard();
            }
        }
        playersHand = tempHand;
        tempHand.Clear();
    }
    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 28), "shuffle"))
        {
            //playersHand = construction.MakePlayersHand();
            Shuffle();
        }
    }


    void Awake()
    {
        //playersHand = construction.playersHand;
        construction.MakePlayersHand();

    }

}
