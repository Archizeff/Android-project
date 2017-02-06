using UnityEngine;
using System.Collections;

public class MainCameraT : MonoBehaviour {

    Camera thisCamera;
    Transform focusRoom;

    string[] states = new string[] { "default", "zoom", "zoom-align", "doubleZoom" };
    public string state;

    Vector3 targetPosition;
    Vector3 deltaVector;
    Vector3 defaultPosition = Vector3.zero;
    public Vector2 XlimitDefault;
    public Vector2 YlimitDefault;
    public Vector2 XlimitZoom;
    public Vector2 YlimitZoom;

    int targetSize;
    public int minSize = 8;
    public int maxSize = 24;
    public int defaultSize, focusSize, doubleFocusSize;

    public int TargetSize
    {
        set
        {
            value = Mathf.Clamp(value, minSize, maxSize);
            targetSize = value;
        }
    }

    public Vector3 TargetPosition
    {
        set
        {
            if (state == states[0])
            {
                targetPosition = new Vector3(Mathf.Clamp(value.x, XlimitDefault[0], XlimitDefault[1]), Mathf.Clamp(value.y, YlimitDefault[0], YlimitDefault[1]), value.z);
            }
            else
            {
                targetPosition = new Vector3(Mathf.Clamp(value.x, XlimitZoom[0], XlimitZoom[1]), Mathf.Clamp(value.y, YlimitZoom[0], YlimitZoom[1]), value.z);
            }
            targetPosition += deltaVector;
        }
    }

    void Start()
    {
        state = states[0];
        thisCamera = GetComponent<Camera>();
        deltaVector = transform.position;
        TargetPosition = defaultPosition;
        TargetSize = defaultSize;
    }

    void Update()
    {
    }


    void MoveToTarget()
    {
        if (targetPosition != transform.position)
        {
            transform.position = targetPosition;
        }
    }

    public void FocusRoom(Transform room)
    {
        if (state == states[2] && focusRoom == room)
        {
            DoubleFocusRoom(room);
            return;
        }
        if (state == states[3])
        {
            ExitDoubleFocusRoom();
            return;
        }
        state = states[2];
        TargetPosition = room.position;
        TargetSize = focusSize;
        focusRoom = room;
    }

    public void DoubleFocusRoom(Transform room)
    {
        state = states[3];
        TargetPosition = room.position;
        TargetSize = doubleFocusSize;
        GameObject.FindGameObjectWithTag("World").GetComponent<World>().TurnOffWithout(room);
    }

    public void ExitDoubleFocusRoom()
    {
        state = states[2];
        TargetSize = focusSize;
        GameObject.FindGameObjectWithTag("World").GetComponent<World>().TurnOnAll();
    }

    public void MoveTo(Vector3 position)
    {
        state = states[1];
        TargetPosition = transform.position + position;
        print(transform.position);
    }

    public void ZoomUp()
    {
        if (state == states[3])
        {
            state = states[2];
            ExitDoubleFocusRoom();
        }
        else if (state == states[1] || state == states[2])
        {
            state = states[0];
            TargetPosition = defaultPosition;
            TargetSize = defaultSize;
        } 
    }

    public void ZoomDown()
    {
        if (state == states[0])
        {
            state = states[1];
            TargetSize = focusSize;
        }
        else if (state == states[1] || state == states[2])
        {
            state = states[3];
            TargetSize = doubleFocusSize;
            //TODO: Move to near room
        }
    }
}
