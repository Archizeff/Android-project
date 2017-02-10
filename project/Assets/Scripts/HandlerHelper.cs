using System;
using UnityEngine;
using UnityEngine.EventSystems;

public static class HandlerHelper  {

    public static float CATCH_TIME = 0.3f;

    public static float lastClick = 0f;
    public static float clickDistance;
    static bool clickPhase = false;
    static Vector2 lastPosition;

    public static void DoubleClick(this PointerEventData eventData, Action oneFunc, Action twoFunc)
    {
        lastClick = Time.time;
        clickDistance = Vector2.Distance(lastPosition, eventData.position);
        lastPosition = eventData.position;

        if (!clickPhase)
        {
            EnumHelper.Processing(oneFunc, twoFunc);
        }
    }

    public static void ToggleClickPhase()
    {
        clickPhase = !clickPhase;
    }
}