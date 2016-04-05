using UnityEngine;
using System.Collections;

public class BaseCard : MonoBehaviour {
    SpriteRenderer spriteRenderer;

    Sprite face;
    public Sprite backOfCard;
    Canvas cardCanvas;

    bool faceOfCard;

    float damage;

    public void startCard(Sprite assignFace, float assignDamage)
    {
        face = assignFace;
        damage = assignDamage;
        faceOfCard = false;
        spriteRenderer.sprite = backOfCard;
    }

    public void TurnOverCard()
    {
        if (!faceOfCard)
        {
            spriteRenderer.sprite = face;
            faceOfCard = true;
        }
        else
        {
            spriteRenderer.sprite = backOfCard;
            faceOfCard = false;
        }
    }


    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        faceOfCard = false;
        spriteRenderer.sprite = backOfCard;
        
    }
    
	
}
