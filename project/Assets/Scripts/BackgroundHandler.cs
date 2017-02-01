using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class BackgroundHandler : MonoBehaviour, IPointerClickHandler {

    HandlerHelper.OneClickFunc OneTouch = new HandlerHelper.OneClickFunc(Touch);
    HandlerHelper.DoubleClickFunc TwoTouch = new HandlerHelper.DoubleClickFunc(Touch2);


    public void OnPointerClick(PointerEventData eventData)
    {
        eventData.DoubleClick(OneTouch, TwoTouch);
    }

    static void Touch() {
        Debug.Log("Touch");
    }

    static void Touch2()
    {
        Debug.Log("DoubleTouch");
    }
}
