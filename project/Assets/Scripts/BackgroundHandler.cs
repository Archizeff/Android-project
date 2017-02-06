using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class BackgroundHandler : MonoBehaviour, IPointerClickHandler {

    public void OnPointerClick(PointerEventData eventData)
    {
        eventData.DoubleClick(() => OneTouch(), () => DoubleTouch());
    }

    void OneTouch()
    {
        Debug.Log("Touch");
    }

    void DoubleTouch()
    {
        Debug.Log("DoubleTouch");
    }
}
