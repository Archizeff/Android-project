using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WorldHandler : MonoBehaviour {

    MainCamera cam;
    UserTouch[] touches = new UserTouch [2];

    void Awake()
    {
        cam = Camera.main.GetComponent<MainCamera>();
        touches[0].CurrentTouch = Vector2.up;
        touches[1].CurrentTouch = Vector2.up;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < 2; i++)
            {
                Touch touch = Input.GetTouch(i);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        touches[i].CurrentTouch = touch.position;
                        break;
                    case TouchPhase.Moved:
                        touches[i].CurrentTouch = touch.position;
                        if (Input.touchCount == 1 && touches[0].DeltaPosition.magnitude > 0.01f)
                        {
                            cam.MooveTo(touches[0].DeltaPosition);
                        }
                        if (Input.touchCount == 2)
                        {
                            float deltaOne = Vector2.Distance(touches[0].GetPrevious(), touches[1].GetPrevious());
                            float deltaTwo = Vector2.Distance(touches[0].GetCurrent(), touches[1].GetCurrent());
                            cam.ZoomTo((deltaOne - deltaTwo) * 0.7f);
                        }
                        break;
                    case TouchPhase.Ended:
                        touches[i].CurrentTouch = Vector2.zero;
                        break;
                }
            }
        }
    }

    struct UserTouch
    {
        Vector2 currentTouch;
        Vector2 previousTouch;

        public Vector2 CurrentTouch
        {
            set
            {
                previousTouch = currentTouch;
                currentTouch = value;
            }
        }

        public Vector3 DeltaPosition
        {
            get { return previousTouch == Vector2.zero ? Vector3.zero : GetCurrent() - GetPrevious(); }
        }

        public Vector3 GetCurrent()
        {
            return currentTouch == Vector2.zero ? GetPrevious() : Camera.main.ScreenToWorldPoint(currentTouch);
        }

        public Vector3 GetPrevious()
        {
           return previousTouch == Vector2.zero ? GetCurrent() : Camera.main.ScreenToWorldPoint(previousTouch);
        }
    }
}




