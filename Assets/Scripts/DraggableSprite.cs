using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DraggableSprite : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private Vector3 cardVector3;
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


    IEnumerator GrowCard(int click)
    {
        float Tzoom = 100;
        Vector3 screenLoc = Camera.main.WorldToScreenPoint(transform.position);
        float start = screenLoc.z;
        float time = 0f;
        while (time <= 1f )
        {
            float scale = growingCurve.Evaluate(time);
            time = time + (Time.deltaTime / duration);
            screenLoc.z = start - (click*(Tzoom * scale));
            //if ((screenLoc.z) >= cardVector3.z)
            //{
            //    screenLoc.z = cardVector3.z;
            //}
            //if ((screenLoc.z) <= (cardVector3.z-Tzoom))
            //{
            //    screenLoc.z = cardVector3.z - Tzoom;
            //}
            transform.position = Camera.main.ScreenToWorldPoint(screenLoc);
            yield return new WaitForFixedUpdate();
        }
    }
    IEnumerator MoveCard()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, GetZCord());
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;
        yield return new WaitForFixedUpdate();
    }

    private float GetZCord()
    {
        screenPointNew = Camera.main.WorldToScreenPoint(transform.position);
        float zCord = screenPointNew.z;
        return zCord;
    }


    void Awake()
    {
        Transform cardTransform = GetComponent<Transform>();
        cardVector3 = cardTransform.localPosition;
    }
}
