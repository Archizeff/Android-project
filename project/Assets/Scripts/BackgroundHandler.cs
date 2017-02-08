using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class BackgroundHandler : MonoBehaviour, IPointerClickHandler {

    MainCamera cam;

    void Awake()
    {
        cam = Camera.main.GetComponent<MainCamera>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        eventData.DoubleClick(() => OneTouch(), () => DoubleTouch());
    }

    void OneTouch()
    {
    }

    void DoubleTouch()
    {
        cam.UpZoom();
    }
}
