using UnityEngine;
using System.Collections;

public class CardImage : MonoBehaviour {
    SpriteRenderer spriteRenderer;

    public Sprite[] faces;
    public int cardIndex;

   

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = faces[cardIndex];
    }
}
