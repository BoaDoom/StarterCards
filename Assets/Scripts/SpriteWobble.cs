using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class SpriteWobble : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    Transform spriteTransform;

    Vector3 rotationOfCard;

    float mouseCursorSpeedX;
    float mouseCursorSpeedY;

    float spriteRotationX;
    float spriteRotationY;

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
    }
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");

        StopCoroutine(Wobble());
        StartCoroutine(Wobble());
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
    }



    IEnumerator Wobble()
    {
        float maxAngle = 0.2f;
        spriteRotationX = spriteTransform.localRotation.x;
        spriteRotationY = spriteTransform.localRotation.y;
        mouseCursorSpeedX = Input.GetAxis("Mouse X") / Time.deltaTime;
        mouseCursorSpeedY = Input.GetAxis("Mouse Y") / Time.deltaTime;
        if (spriteRotationX > maxAngle)
        {
            mouseCursorSpeedX = 0;
        }
        if (spriteRotationY > maxAngle)
        {
            mouseCursorSpeedY = 0;
        }
        if (spriteRotationX < -maxAngle)
        {
            mouseCursorSpeedX = 0;
        }
        if (spriteRotationY < -maxAngle)
        {
            mouseCursorSpeedY = 0;
        }
        rotationOfCard.x = mouseCursorSpeedY;
        rotationOfCard.y = mouseCursorSpeedX;
        
        spriteTransform.Rotate(rotationOfCard, Space.Self);
        yield return new WaitForEndOfFrame();
    }

    void rotateTheCard(float Xinput, float Yinput)
    {
        spriteTransform.Rotate(rotationOfCard);
    }

    void Update () {
        if (mouseCursorSpeedX < 0)
        {

        }
        else if(mouseCursorSpeedY < 0)
        {

        }
        
        
    }
    void Awake()
    {
        spriteTransform = GetComponent<Transform>();
        rotationOfCard = new Vector3(0f,0f,0f);
    }
}
