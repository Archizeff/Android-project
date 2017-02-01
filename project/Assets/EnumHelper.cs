using UnityEngine;
using System.Collections;

public class EnumHelper : MonoBehaviour {

    static EnumHelper instance;

    void Awake()
    {
        instance = this;
    }

    public static void Processing(HandlerHelper.OneClickFunc oneFunc, HandlerHelper.DoubleClickFunc twoFunc)
    {
        instance.StartCoroutine(instance.Coroutine(oneFunc, twoFunc));
    }

    IEnumerator Coroutine(HandlerHelper.OneClickFunc oneClickFunc, HandlerHelper.DoubleClickFunc doubleClickFunc)
    {
        HandlerHelper.ToggleClickPhase();
        yield return new WaitForSeconds(HandlerHelper.CATCH_TIME);
        if (Time.time - HandlerHelper.lastClick < HandlerHelper.CATCH_TIME)
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

