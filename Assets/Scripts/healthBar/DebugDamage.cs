using UnityEngine;
using System.Collections;

public class DebugDamage : MonoBehaviour
{
    HealthBar healthBar;
    HealthBarMovement healthBarMovement;

    //PlayerDeck playerDeck;

    public GameObject HPbar;
    public GameObject playerOneDeck;

	void Awake()
    {
        healthBar = HPbar.GetComponent<HealthBar>();
        healthBarMovement = HPbar.GetComponent<HealthBarMovement>();
        //playerDeck = playerOneDeck.GetComponent<PlayerDeck>();
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 28), "20 damage"))
        {
            healthBarMovement.TakeDamage(healthBar.getHealth(), 20);
            healthBar.ReduceHealth(20);
        }

        if (GUI.Button(new Rect(300, 10, 100, 28), "5 damage"))
        {
            healthBarMovement.TakeDamage(healthBar.getHealth(), 5);
            healthBar.ReduceHealth(5);
        }

        if (GUI.Button(new Rect(10, 40, 100, 28), "next card"))
        {
            //playerDeck.cardIndex++;
            //if (playerDeck.cardIndex > playerDeck.faces.Length-1)
            //{
           //     playerDeck.cardIndex = 0;
            //}
            //playerDeck.nextCard();

        }
    }
	
}
