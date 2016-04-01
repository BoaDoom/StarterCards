using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DraggableSprite : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private Transform cardTransform;
    public AnimationCurve growingCurve;
    public float duration = 0.01f;
    float zoomSize = 80f;
    float zoom = 0f;

    private Vector3 screenPointOld;
    private Vector3 screenPointNew;
    private Vector3 offset;

    void OnMouseDown()
    {
        GrowCard(zoomSize);
    }
    void OnMouseUp()
    {
        GrowCard(zoomSize);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

        Debug.Log("OnBeginDrag");
        screenPointNew = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPointNew.z));
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPointNew.z);
        //GrowCard(zoomSize);
        //curScreenPoint.z -= zoom;
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;

    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");

        
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPointNew.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;
    }

    public void SizeTest(float testZoom)
    {
        zoom = testZoom;
    }


    public void GrowCard(float Tzoom)
    {
        StopCoroutine(selectionZoom(Tzoom));
        StartCoroutine(selectionZoom(Tzoom));
    }

    IEnumerator selectionZoom(float Tzoom)
    {
        Vector3 screenLoc = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        float time = 0f;
        while (time <= 1f)
        {
            float scale = growingCurve.Evaluate(time);
            time = time + (Time.deltaTime / duration);
            screenLoc.z = screenLoc.z - ((scale * Tzoom)/ Tzoom);
            //screenLoc.z = screenLoc.z - Tzoom;
            gameObject.transform.position = Camera.main.ScreenToWorldPoint(screenLoc);
            yield return new WaitForFixedUpdate();
        }
    }


    void Awake()
    {
        cardTransform = GetComponent<Transform>();
    }
}
