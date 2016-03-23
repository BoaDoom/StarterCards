using UnityEngine;
using System.Collections;

public class PlayerDeck : MonoBehaviour {

    SpriteRenderer spriteRenderer;

    public Sprite[] faces;
    public int cardIndex;

    public void nextCard()
    {
        spriteRenderer.sprite = faces[cardIndex];
    }

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
