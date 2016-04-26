using UnityEngine;
using System.Collections;

public class CardPositioner : MonoBehaviour {

    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = !spriteRenderer.enabled;

    }

}
