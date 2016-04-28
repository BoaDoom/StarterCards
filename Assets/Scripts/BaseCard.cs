using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class BaseCard : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    /// <summary>
    Vector3 originLocalPosition;

    float Tzoom = 15;
    float startLoc = 100; //Z location of the canvas its located on
    float levelOneZoom;
    float levelTwoZoom;

    float minZoom;
    float maxZoom;

    Transform spriteTransform;
    bool cardReleased;
    bool CardMoved;
    int parentChildren;
    Transform lastOriginParent;
    //bool pointerLock = false;

    //float newZCord;

    float speedInputX;
    float speedInputY;

    float mouseCursorSpeedX;
    float mouseCursorSpeedY;

    float mouseCursorSpeedX1;
    float mouseCursorSpeedY1;
    float mouseCursorSpeedX2;
    float mouseCursorSpeedY2;
    float mouseCursorSpeedX3;
    float mouseCursorSpeedY3;

    float spriteRotationX;
    float spriteRotationY;

    public AnimationCurve growingCurve;
    float zoom = 0f;

    private Vector3 screenPointNew;
    private Vector3 offset;
    //private Vector3 absolutePosition;

    float time;

    /// </summary>
    SpriteRenderer spriteRenderer;


    Sprite face;
    public Sprite backOfCard;
    public CardPositioner CardPositionerPrefab;     //the prefab empty card slot used for positioning
    CardPositioner CardPosition;        //the actual card position object
    Canvas cardCanvas;

    Transform originParent = null;      //the parent of the card origin on begin drag, the playerpanel
    bool cardClicked = false;       //the state of the card being clicked or not
    bool cardHovered = false;       //the state of the card being hovered or not
    public AnimationCurve settlingBackIntoPosition;     //the rate to move back to location after being dragged
    public float duration = 0.5f;   //duration of animation

    bool faceOfCard;
    float damage;

    public void startCard(Sprite assignFace, float assignDamage)
    {
        face = assignFace;
        damage = assignDamage;
        lastOriginParent = this.transform.parent;
        parentChildren = this.transform.parent.childCount;
        UpdatePosition();
    }
    
    void OnMouseDown()
    {
        parentChildren = this.transform.parent.childCount;
        Debug.Log("OnMouseDown");
        cardClicked = true;
        StopCoroutine(ClickGrowCard());
        StartCoroutine(ClickGrowCard());
    }
    void OnMouseUp()
    {
        Debug.Log("OnMouseUp");
        cardClicked = false;
        cardHovered = false;
        cardReleased = true;
        //StopCoroutine(ClickGrowCard());

        StopCoroutine(ClickShrinkCard());
        StartCoroutine(ClickShrinkCard());

    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("OnHoverEnter");
        //StopCoroutine(HoverGrowCard());
        //StopCoroutine(HoverShrinkCard());
        cardHovered = true;
        if (!cardClicked)
        {
            StopCoroutine(HoverGrowCard());
            StartCoroutine(HoverGrowCard());
        }

    }
    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("OnHoverExit");
        //StopCoroutine(HoverShrinkCard());
        //StopCoroutine(HoverGrowCard());
        if (!cardClicked)
        {
            StopCoroutine(HoverShrinkCard());
            StartCoroutine(HoverShrinkCard());
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //StopCoroutine(ClickShrinkCard());
        //StopCoroutine(ClickGrowCard());
        //StopCoroutine(HoverShrinkCard());
        //StopCoroutine(HoverGrowCard());
        //StopCoroutine(GoToLayoutPosition());
        CardMoved = true;
        screenPointNew = Camera.main.WorldToScreenPoint(this.transform.position);
        offset = this.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPointNew.z));


        originParent = lastOriginParent = this.transform.parent;             //saving the origin location of the card, the playerpanel
        CardPosition.transform.SetParent(this.transform.parent.parent, true);     //setting the parent of the CardPositioner clone to the parent of this card
        this.transform.SetParent(this.transform.parent.parent, true);       //setting the parent of the card as the whole card canvas


    }

    public void OnDrag(PointerEventData eventData)
    {
        StopCoroutine(MoveCard());
        StartCoroutine(MoveCard());
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        //StopCoroutine(MoveCard());
        this.transform.SetParent(originParent, true);       //resetting the parent to the player panel
        lastOriginParent = this.transform.parent;     
        CardPosition.transform.SetParent(originParent, true);       //resetting the parent to the player panel


    }

    //int GetCardCount()
    //{
    //    return this.transform.parent.childCount;
    //}


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

    IEnumerator GoToLayoutPosition()
    {
        //StopCoroutine(MoveCard());
        Debug.Log("GoToLayoutPosition activated");
        float time = 0f;
        float TravelX = CardPosition.transform.position.x - this.transform.position.x;
        float TravelY = CardPosition.transform.position.y - this.transform.position.y;
        float TravelZ = CardPosition.transform.position.z - this.transform.position.z;

        float OriginX = this.transform.position.x;
        float OriginY = this.transform.position.y;
        float OriginZ = this.transform.position.z;

        while (time <= 1f)
        {
            float scale = settlingBackIntoPosition.Evaluate(time);
            time = time + (Time.deltaTime / duration);
            this.transform.position = new Vector3((OriginX+(TravelX* scale)), (OriginY + (TravelY * scale)), (OriginZ + (TravelZ * scale)));
            yield return new WaitForEndOfFrame();
        }
        //this.transform.position = CardPosition.transform.position;
    }

    IEnumerator HoverGrowCard()
    {
        float time = 0f;
        float newZCord;
        float startingPosition = this.transform.position.z;
        float distanceToTravel = levelOneZoom - this.transform.position.z;
        while (time <= 1f)
        {
            float scale = growingCurve.Evaluate(time);
            time = time + (Time.deltaTime / duration);
            newZCord = startingPosition + (distanceToTravel * scale);
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(GetCurrentScreenCord().x, GetCurrentScreenCord().y, newZCord));
            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator HoverShrinkCard()
    {
        float time = 0f;
        float newZCord;
        float startingPosition = this.transform.position.z;
        float distanceToTravel = startLoc - this.transform.position.z;
        while (time <= 1f)
        {
            float scale = growingCurve.Evaluate(time);
            time = time + (Time.deltaTime / duration);
            newZCord = startingPosition + (distanceToTravel * scale);
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(GetCurrentScreenCord().x, GetCurrentScreenCord().y, newZCord));
            yield return new WaitForEndOfFrame();
        }
        cardHovered = false;
    }
    IEnumerator ClickGrowCard()
    {
        float time = 0f;
        float newZCord;
        float startingPosition = this.transform.position.z;
        float distanceToTravel = levelTwoZoom - this.transform.position.z;
        while (time <= 1f)
        {
            float scale = growingCurve.Evaluate(time);
            time = time + (Time.deltaTime / duration);
            newZCord = startingPosition + (distanceToTravel * scale);
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(GetCurrentScreenCord().x, GetCurrentScreenCord().y, newZCord));
            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator ClickShrinkCard()
    {
            float time = 0f;
            float newZCord;
            float startingPosition = this.transform.position.z;
            float distanceToTravel = levelOneZoom - this.transform.position.z;
            while (time <= 1f)
            {
                float scale = growingCurve.Evaluate(time);
                time = time + (Time.deltaTime / duration);
                newZCord = startingPosition + (distanceToTravel * scale);
                transform.position = Camera.main.ScreenToWorldPoint(new Vector3(GetCurrentScreenCord().x, GetCurrentScreenCord().y, newZCord));
                yield return new WaitForEndOfFrame();
            }
        }


        IEnumerator MoveCard()
    {
        StopCoroutine(ClickShrinkCard());
        StopCoroutine(ClickGrowCard());
        StopCoroutine(HoverShrinkCard());
        StopCoroutine(HoverGrowCard());

        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, GetCurrentScreenCord().z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        this.transform.position = curPosition;

        yield return new WaitForFixedUpdate();
    }


    IEnumerator Wobble(float startx, float starty)
    {
        mouseCursorSpeedX3 = mouseCursorSpeedX2;
        mouseCursorSpeedY3 = mouseCursorSpeedY2;
        mouseCursorSpeedX2 = mouseCursorSpeedX1;
        mouseCursorSpeedY2 = mouseCursorSpeedY1;
        mouseCursorSpeedX1 = startx;
        mouseCursorSpeedY1 = starty;
        yield return new WaitForEndOfFrame();
    }

    private Vector3 GetCurrentScreenCord()
    {
        screenPointNew = Camera.main.WorldToScreenPoint(this.transform.position);
        return screenPointNew;

    }

    public void UpdatePosition()
    {
        StopCoroutine(GoToLayoutPosition());      //setting the card towards its sorted location
        StartCoroutine(GoToLayoutPosition());
    }


    void Update()
    {
        if (!cardClicked && !cardHovered && CardMoved)
        {
            StopCoroutine(ClickShrinkCard());
            StopCoroutine(ClickGrowCard());
            StopCoroutine(HoverShrinkCard());
            StopCoroutine(HoverGrowCard());
            StopCoroutine(MoveCard());

            StopCoroutine(GoToLayoutPosition());      //setting the card towards its sorted location
            StartCoroutine(GoToLayoutPosition());
            CardMoved = false;
        }
        //if (parentChildren != lastOriginParent.childCount)
        //{
        //    UpdatePosition();
        //    parentChildren = lastOriginParent.childCount;
        //}

        mouseCursorSpeedX = Input.GetAxis("Mouse X") / Time.deltaTime;
        mouseCursorSpeedY = Input.GetAxis("Mouse Y") / Time.deltaTime;

        float spriteRotationX = (mouseCursorSpeedX1 + mouseCursorSpeedX2 + mouseCursorSpeedX3) / 3;
        float spriteRotationY = (mouseCursorSpeedY1 + mouseCursorSpeedY2 + mouseCursorSpeedY3) / 3;

        spriteTransform.eulerAngles = new Vector3(spriteRotationY, spriteRotationX, 0f);
        if (cardClicked)
        {
            speedInputX = mouseCursorSpeedX;
            speedInputY = mouseCursorSpeedY;
        }
        else
        {
            speedInputX = 0;
            speedInputY = 0;
        }
        StopCoroutine(Wobble(speedInputX, speedInputY));
        StartCoroutine(Wobble(speedInputX, speedInputY));

    }
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = backOfCard;
        
        CardPosition = Instantiate(CardPositionerPrefab);       //creates an instance of the positioner object used for animation and sorting positions
        CardPosition.transform.SetParent(this.transform, false);     //sets parent to this instance of the card object, so that it can be found and set as a playerpanel child. Messy

        faceOfCard = false;
        cardReleased = false;
        CardMoved = true;
        

        levelOneZoom = startLoc - Tzoom;
        levelTwoZoom = startLoc - (Tzoom*2);


        //newZCord = GetCurrentScreenCord().z;

        spriteTransform = GetComponent<Transform>();

        mouseCursorSpeedX1 = 0.01f;
        mouseCursorSpeedY1 = 0.01f;
        mouseCursorSpeedX2 = 0.01f;
        mouseCursorSpeedY2 = 0.01f;
        mouseCursorSpeedX3 = 0.01f;
        mouseCursorSpeedY3 = 0.01f;
    }


    
	
}
