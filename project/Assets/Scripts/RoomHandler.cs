﻿using UnityEngine;
using UnityEngine.EventSystems;

public class RoomHandler : MonoBehaviour, IPointerClickHandler
{
    MainCamera sceneCamera;

    bool touching = false;
    float timeDoubleTouch = 0.3f;
    float lastTouch;
 
    void Start()
    {
        sceneCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MainCamera>();
    }
	
	void Update ()
    {
        if (touching && (Time.time - lastTouch > timeDoubleTouch))
        {
            touching = false;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (touching)
        {
            touching = false;
            sceneCamera.FocusRoom(transform);
        }
        else
        {
            touching = true;
            lastTouch = Time.time;
        }
    }

    public void TurnActive(bool state)
    {
        gameObject.SetActive(state);
    }
}
