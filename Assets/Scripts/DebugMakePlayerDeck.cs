using UnityEngine;
using System.Collections;

public class DebugMakePlayerDeck : MonoBehaviour {

    public PlayerDeckScript playerDeckScript;
    public ComponentCreatorScript componentCreatorScript;

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 40, 100, 28), "show hand"))
        {
            playerDeckScript.dealHand();
        }
        if (GUI.Button(new Rect(110, 40, 100, 28), "add set 1"))
        {
            componentCreatorScript.takeComponentCode(1);
        }
        if (GUI.Button(new Rect(210, 40, 100, 28), "add set 2"))
        {
            componentCreatorScript.takeComponentCode(2);
        }
        if (GUI.Button(new Rect(310, 40, 100, 28), "add set 3"))
        {
            componentCreatorScript.takeComponentCode(3);
        }

    }
}
