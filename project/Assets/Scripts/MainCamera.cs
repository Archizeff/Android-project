using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {

    public Vector4 positionLim;
    public Vector2 zoomLim;
    public float defaultZoom;
    public float middleZoom;
    public float focusZoom;
    public float animationSpeed;

    bool focus = false;
    bool anim = false;
    float tempZoom;
    float targetZoom;
    World world;
    Transform rotationParent;
    Transform targetRoom;
    Vector3 targetRoomPos;
    Vector3 tempPosition = Vector3.zero;
    Vector3 targetPosition;
     
    public Vector3 TargetPosition
    {
        set
        {
            float x = Mathf.Clamp(value.x, positionLim[0], positionLim[1]);
            float y = Mathf.Clamp(value.y, positionLim[2], positionLim[3]);
            targetPosition = new Vector3(x, y, 0);
        }
        get
        {
            return targetPosition;
        }
    }

    public float TargetZoom
    {
        set
        {
            bool zoomIn = TargetZoom > value;
            Focus = TargetZoom == focusZoom;
            targetZoom = Mathf.Clamp(value, zoomLim[0], zoomLim[1]);
            float temp = Mathf.InverseLerp(middleZoom, focusZoom, TargetZoom);
            rotationParent.rotation = Quaternion.Euler(Mathf.Lerp(45, 25, temp), 0, 0);
            if (!anim)Focussing(zoomIn);
        }
        get
        {
            return targetZoom;
        }
    }

    public bool Focus
    {
        set
        {
            if (value && !Focus)
            {
                world.TurnOffWithout(targetRoom);
            }
            else if (!value && Focus)
            {
                world.TurnOnAll();
            }
            focus = value;
        }
        get
        {
            return focus;
        }
    }

    void Awake()
    {
        world = GameObject.FindGameObjectWithTag("World").GetComponent<World>();
        rotationParent = transform.parent.parent;
    }

    public void MooveTo(Vector3 target)
    {
        if (!focus && !anim)
        {
            targetRoom = null;
            tempPosition = Vector3.zero;
            TargetPosition = transform.localPosition - target;
            transform.localPosition = targetPosition;
        }
    }

    public void ZoomTo(float target)
    {
        if (!anim)
        {
            TargetZoom = Camera.main.orthographicSize + target;
            Camera.main.orthographicSize = targetZoom;
        }
    }

    public void RoomClick(Transform room)
    {
        targetRoom = room;
        targetRoomPos = new Vector3(targetRoom.position.x, targetRoom.position.y * 1.225f, targetRoom.position.z);
        StartCoroutine(MoveToFocus(targetRoomPos));
    }

    public void BackClick()
    {
        StartCoroutine(MoveToDefault());
    }

    void Align()
    {
        targetRoom = world.FindNearRoom(transform.localPosition);
        targetRoomPos = new Vector3(targetRoom.position.x, targetRoom.position.y * 1.225f, targetRoom.position.z);
    }

    void Focussing(bool zoomIn)
    {
        if (targetRoom == null) Align();
        if (zoomIn)
        {
            if (tempPosition == Vector3.zero)
            {
                tempPosition = transform.localPosition;
                tempZoom = Camera.main.orthographicSize;
            }
            else
            {
                float temp = Mathf.InverseLerp(tempZoom, focusZoom, Camera.main.orthographicSize);
                transform.localPosition = Vector3.Lerp(tempPosition, targetRoomPos, temp);
            }
        }
        else
        {
            tempPosition = Vector3.zero;
        }
    }

    IEnumerator MoveToDefault()
    {
        anim = true;
        Vector3 tempPos = transform.localPosition;
        TargetZoom = Camera.main.orthographicSize;
        float tempZoom = TargetZoom;
        while (TargetZoom < defaultZoom)
        {
            TargetZoom += animationSpeed;
            Camera.main.orthographicSize = TargetZoom;
            TargetPosition = Vector3.Lerp(tempPos, Vector3.zero, Mathf.InverseLerp(tempZoom, defaultZoom, TargetZoom));
            transform.localPosition = TargetPosition;
            yield return null;
        }
        targetRoom = null;
        anim = false; 
    }

    IEnumerator MoveToFocus(Vector3 target)
    {
        anim = true;
        Vector3 tempPos = transform.localPosition;
        TargetZoom = Camera.main.orthographicSize;
        float tempZoom = TargetZoom;
        while (TargetZoom > focusZoom)
        {
            TargetZoom -= animationSpeed;
            Camera.main.orthographicSize = TargetZoom;
            TargetPosition = Vector3.Lerp(tempPos, target, Mathf.InverseLerp(tempZoom, focusZoom, TargetZoom));
            transform.localPosition = TargetPosition;
            yield return null;
        }
        Focus = true;
        anim = false;
    }
}
