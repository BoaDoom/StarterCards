using UnityEngine;
using System.Collections;

public class ZoomOnClick : MonoBehaviour {

    private Transform cardTransform;
    public AnimationCurve growingCurve;
    public float duration = 0.5f;

    void OnMouseDown()
    {
        //StopCoroutine(GrowCard(200));
        StartCoroutine(GrowCard(200));
    }

    IEnumerator GrowCard(float Tzoom)
    {
        Vector3 screenLoc = Camera.main.WorldToScreenPoint(transform.position);
        float start = screenLoc.z;
        float time = 0f;
        while (time <= 1f)
        {
            float scale = growingCurve.Evaluate(time);
            time = time + (Time.deltaTime / duration);
            screenLoc.z = start - (Tzoom * scale);
            transform.position = Camera.main.ScreenToWorldPoint(screenLoc);
            yield return new WaitForFixedUpdate();
        }
    }

    void Awake()
    {
        cardTransform = GetComponent<Transform>();
    }
}
