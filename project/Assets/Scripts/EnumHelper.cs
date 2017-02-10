using System;
using UnityEngine;
using System.Collections;

public class EnumHelper : MonoBehaviour {

    static EnumHelper instance;

    void Awake()
    {
        instance = this;
    }

    public static void Processing(Action oneFunc, Action twoFunc)
    {
        instance.StartCoroutine(instance.Coroutine(oneFunc, twoFunc));
    }

    IEnumerator Coroutine(Action oneClickFunc, Action doubleClickFunc)
    {
        HandlerHelper.ToggleClickPhase();
        yield return new WaitForSeconds(HandlerHelper.CATCH_TIME);
        if (Time.time - HandlerHelper.lastClick < HandlerHelper.CATCH_TIME && HandlerHelper.clickDistance < 60f)
        {
            doubleClickFunc();
        }
        else
        {
            oneClickFunc();
        }
        HandlerHelper.ToggleClickPhase();
    }
}

