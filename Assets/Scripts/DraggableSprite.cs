using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DraggableSprite : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
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
        //startZlocation();
        StopCoroutine(GrowCard(1));
        StartCoroutine(GrowCard(1));
    }
    void OnMouseUp()
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


    //IEnumerator GrowCard(int click)
    //{
    //    float Tzoom = 30;
    //    Vector3 startLoc = Camera.main.WorldToScreenPoint(cardVector3);
    //    float startZ = startLoc.z;
    //    Vector3 creenLoc = startLoc;
    //    float time = 0f;
    //    while (time <= 1f)
    //    {
    //        float scale = growingCurve.Evaluate(time);
    //        time = time + (Time.deltaTime / duration);
    //        creenLoc.z = startZ - (click * (Tzoom * scale));
    //        if ((creenLoc.z) > startLoc.z)
    //        {
    //            creenLoc.z = startLoc.z;
    //        }
    //        if ((creenLoc.z) < startLoc.z - Tzoom)
    //        {
    //            creenLoc.z = startLoc.z - Tzoom;
    //        }
    //        transform.position = Camera.main.ScreenToWorldPoint(creenLoc);
    //        yield return new WaitForFixedUpdate();
    //    }
    //}
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
    //IEnumerator GrowCard(int click)
    //{
    //    float Tzoom = 30;
    //    Vector3 startLoc = cardVector3;
    //    Vector3 creenLoc = startLoc;
    //    float startZ = startLoc.z;

    //        creenLoc.z = startZ - (click * (Tzoom));
    //        transform.localPosition = creenLoc;
    //        yield return new WaitForFixedUpdate();
    //}

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
    //void startZlocation()
    //{
    //    cardVector3 = GetCurrentScreenCord();
    //}

    void Awake()
    {

    }
}
