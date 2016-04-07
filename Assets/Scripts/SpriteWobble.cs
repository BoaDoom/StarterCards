using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class SpriteWobble : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
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


    public void OnBeginDrag(PointerEventData eventData)
    {
        cardWobbleOn = true;
        Debug.Log("OnBeginDrag");
    }
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        cardWobbleOn = false;
        Debug.Log("OnEndDrag");
    }



    //IEnumerator Wobble()
    //{
    //    AverageAngle(mouseCursorSpeedX, mouseCursorSpeedY)

    //    float spriteRotationX = (mouseCursorSpeedX1 + mouseCursorSpeedX2 + mouseCursorSpeedX3) / 3;
    //    float spriteRotationY = (mouseCursorSpeedY1 + mouseCursorSpeedY2 + mouseCursorSpeedY3) / 3;

    //     = mouseCursorSpeedYP;
    //     = mouseCursorSpeedXP;

    //    spriteTransform.eulerAngles = new Vector3(spriteRotationX, spriteRotationY, 0f);

    //    yield return new WaitForEndOfFrame();
    //}
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


    void Update () {
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
