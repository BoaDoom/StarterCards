using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DraggableSprite : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    Transform spriteTransform;
    bool cardWobbleOn;

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

    //private Vector3 cardVector3;
    public AnimationCurve growingCurve;
    public float duration = 0.5f;
    //float zoomSize = 5f;
    float zoom = 0f;

    private Vector3 screenPointOld;
    private Vector3 screenPointNew;
    private Vector3 offset;

    float time;

    void OnMouseDown()
    {
        cardWobbleOn = true;
    }
    void OnMouseUp()
    {
        cardWobbleOn = false;
    }


    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        //startZlocation();
        
        StopCoroutine(GrowCard(1));
        StartCoroutine(GrowCard(1));
    }
    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        
        StopCoroutine(GrowCard(-1));
        StartCoroutine(GrowCard(-1));
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


    
    IEnumerator GrowCard(int click)
    {
        float Tzoom = 30;
        Vector3 screenLoc = GetCurrentScreenCord();
        Vector3 startLoc = Camera.main.ScreenToWorldPoint(transform.parent.position);
        float start = screenLoc.z;
        float time = 0f;
        while (time <= 1f)
        {
            float scale = growingCurve.Evaluate(time);
            time = time + (Time.deltaTime / duration);
            screenLoc.z = start - (click * (Tzoom * scale));
            if ((screenLoc.z) > startLoc.z)
            {
                screenLoc.z = startLoc.z;
            }
            if ((screenLoc.z) < (startLoc.z - Tzoom))
            {
                screenLoc.z = startLoc.z - (Tzoom);
            }
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(GetCurrentScreenCord().x, GetCurrentScreenCord().y, screenLoc.z));
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator MoveCard()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, GetCurrentScreenCord().z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;
        yield return new WaitForFixedUpdate();
    }

    private Vector3 GetCurrentScreenCord()
    {
        screenPointNew = Camera.main.WorldToScreenPoint(transform.position);
        return screenPointNew;
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
        spriteTransform = GetComponent<Transform>();
        cardWobbleOn = false;
        mouseCursorSpeedX1 = 0.01f;
        mouseCursorSpeedY1 = 0.01f;
        mouseCursorSpeedX2 = 0.01f;
        mouseCursorSpeedY2 = 0.01f;
        mouseCursorSpeedX3 = 0.01f;
        mouseCursorSpeedY3 = 0.01f;

    }

}
