using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DraggableSprite : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    float Tzoom = 15;
    float startLoc = 100; //Z location of the canvas its located on

    float minZoom;
    float maxZoom;

    Transform spriteTransform;
    bool cardWobbleOn;
    bool pointerLock;

    float newZCord;

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
    public float duration = 0.5f;
    float zoom = 0f;

    private Vector3 screenPointNew;
    private Vector3 offset;


    float time;

    void OnMouseDown()
    {
        pointerLock = true;
        cardWobbleOn = true;
        StopCoroutine(ClickGrowCard(1));
        StartCoroutine(ClickGrowCard(1));
    }
    void OnMouseUp()
    {
        pointerLock = false;
        cardWobbleOn = false;
        StopCoroutine(ClickGrowCard(-1));
        StartCoroutine(ClickGrowCard(-1));
        StopCoroutine(HoverGrowCard(-1));
        StartCoroutine(HoverGrowCard(-1));
    }


    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        if (!pointerLock)
        {
            StopCoroutine(HoverGrowCard(1));
            StartCoroutine(HoverGrowCard(1));
        }

    }
    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        if (!pointerLock)
        {
            StopCoroutine(HoverGrowCard(-1));
            StartCoroutine(HoverGrowCard(-1));
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {

        screenPointNew = Camera.main.WorldToScreenPoint(transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPointNew.z));
    }

    public void OnDrag(PointerEventData eventData)
    {
        StopCoroutine(MoveCard());
        StartCoroutine(MoveCard());
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        StopCoroutine(MoveCard());
        StartCoroutine(MoveCard());
    }
    
    IEnumerator HoverGrowCard(int click)
    {

        float start = newZCord;
        float time = 0f;
        while (time <= 1f)
        {
            float scale = growingCurve.Evaluate(time);
            time = time + (Time.deltaTime / duration);
            newZCord = start - (click * (Tzoom * scale));

            if (newZCord > minZoom)
            {
                newZCord = minZoom;
            }
            if (newZCord < maxZoom)
            {
                newZCord = maxZoom;
            }

            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(GetCurrentScreenCord().x, GetCurrentScreenCord().y, newZCord));
            yield return new WaitForEndOfFrame();
        }

    }
    IEnumerator ClickGrowCard(int click)
    {
        if (click > 0)
        {
            minZoom -= (Tzoom * click);
            maxZoom -= (Tzoom * click);
        }
        float start = newZCord;
        float time = 0f;
        while (time <= 1f)
        {
            float scale = growingCurve.Evaluate(time);
            time = time + (Time.deltaTime / duration);
            newZCord = start - (click * (Tzoom * scale));

            if (newZCord > minZoom)
            {
                newZCord = minZoom;
            }
            if (newZCord < maxZoom)
            {
                newZCord = maxZoom;
            }
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(GetCurrentScreenCord().x, GetCurrentScreenCord().y, newZCord));
            yield return new WaitForEndOfFrame();
        }
        if (click < 0)
        {
            minZoom -= (Tzoom * click);
            maxZoom -= (Tzoom * click);
        }
    }

    IEnumerator MoveCard()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, GetCurrentScreenCord().z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;
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
        screenPointNew = Camera.main.WorldToScreenPoint(transform.position);
        return screenPointNew;
    }

    void Update()
    {
        mouseCursorSpeedX = Input.GetAxis("Mouse X") / Time.deltaTime;
        mouseCursorSpeedY = Input.GetAxis("Mouse Y") / Time.deltaTime;

        float spriteRotationX = (mouseCursorSpeedX1 + mouseCursorSpeedX2 + mouseCursorSpeedX3) / 3;
        float spriteRotationY = (mouseCursorSpeedY1 + mouseCursorSpeedY2 + mouseCursorSpeedY3) / 3;

        spriteTransform.eulerAngles = new Vector3(spriteRotationY, spriteRotationX, 0f);
        if (cardWobbleOn)
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
        minZoom = startLoc;
        maxZoom = startLoc - Tzoom;

        newZCord = GetCurrentScreenCord().z;

        spriteTransform = GetComponent<Transform>();
        cardWobbleOn = false;
        pointerLock = false;

        mouseCursorSpeedX1 = 0.01f;
        mouseCursorSpeedY1 = 0.01f;
        mouseCursorSpeedX2 = 0.01f;
        mouseCursorSpeedY2 = 0.01f;
        mouseCursorSpeedX3 = 0.01f;
        mouseCursorSpeedY3 = 0.01f;

    }

}
