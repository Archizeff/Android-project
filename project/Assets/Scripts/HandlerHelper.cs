using System;
using UnityEngine;
using UnityEngine.EventSystems;

public static class HandlerHelper  {

    public static float CATCH_TIME = 0.3f;

    public static float lastClick = 0f;
    public static bool clickPhase = false;

    public static void DoubleClick(this PointerEventData _, Action oneFunc, Action twoFunc)
    {
        lastClick = Time.time;
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