using UnityEngine;
using UnityEngine.EventSystems;

public static class HandlerHelper  {

    public static float CATCH_TIME = 0.3f;

    public static float lastClick = 0f;
    public static bool clickPhase = false;

    public delegate void OneClickFunc();
    public delegate void DoubleClickFunc();

    public static void DoubleClick(this PointerEventData _, OneClickFunc oneFunc, DoubleClickFunc twoFunc)
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