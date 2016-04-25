using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class BaseCard : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    SpriteRenderer spriteRenderer;


    Sprite face;
    public Sprite backOfCard;
    public CardPositioner CardPositionerPrefab;     //the prefab empty card slot used for positioning
    CardPositioner CardPosition;        //the actual card position object
    Canvas cardCanvas;

    Transform originParent = null;      //the parent of the card origin on begin drag, the playerpanel
    bool cardClicked = false;       //the state of the card being dragged or not
    public AnimationCurve settlingBackIntoPosition;     //the rate to move back to location after being dragged
    public float duration = 0.5f;   //duration of animation

    bool faceOfCard;
    float damage;

    public void OnBeginDrag(PointerEventData eventData)
    {
        cardClicked = true;     //yes, the card is being dragged
        originParent = this.transform.parent;             //saving the origin location of the card, the playerpanel
        CardPosition.transform.SetParent(this.transform.parent.parent, true);     //setting the parent of the CardPositioner clone to the parent of this card
        this.transform.SetParent(this.transform.parent.parent, true);       //setting the parent of the card as the whole card canvas

    }

    public void OnDrag(PointerEventData eventData)
    {

    }
    public void OnEndDrag(PointerEventData eventData)
    {
        cardClicked = false;    //no, the card is not being dragged
        this.transform.SetParent(originParent, true);       //resetting the parent to the player panel
        CardPosition.transform.SetParent(originParent, true);       //resetting the parent to the player panel

        StopCoroutine(goToPosition());      //setting the card towards its sorted location
        StartCoroutine(goToPosition());
    }


    public void startCard(Sprite assignFace, float assignDamage)
    {
        face = assignFace;
        damage = assignDamage;
        faceOfCard = false;
        spriteRenderer.sprite = backOfCard;
        CardPosition = Instantiate(CardPositionerPrefab);       //creates an instance of the positioner object used for animation and sorting positions
        CardPosition.transform.SetParent(this.transform, true);     //sets parent to this instance of the card object, so that it can be found and set as a playerpanel child. Messy
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

    IEnumerator goToPosition()
    {
        float time = 0f;
        float TravelX = CardPosition.transform.position.x - this.transform.position.x;
        float TravelY = CardPosition.transform.position.y - this.transform.position.y;
        float OriginX = this.transform.position.x;
        float OriginY = this.transform.position.y;
        while (time <= 1f)
        {
            float scale = settlingBackIntoPosition.Evaluate(time);
            time = time + (Time.deltaTime / duration);
            this.transform.position = new Vector3((OriginX+(TravelX* scale)), (OriginY + (TravelY * scale)), this.transform.position.z);
            yield return new WaitForEndOfFrame();
        }
    }

    void Update()
    {
        if (!cardClicked)
        {
            StopCoroutine(goToPosition());      //setting the card towards its sorted location
            StartCoroutine(goToPosition());
        }
    }
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        faceOfCard = false;
        spriteRenderer.sprite = backOfCard;
        
    }


    
	
}
