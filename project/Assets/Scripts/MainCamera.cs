using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {

    public Vector4 positionLim;
    public Vector2 zoomLim;
    public float defaultZoom;
    public float middleZoom;
    public float focusZoom;

    bool focus = false;

    Transform rotationParent;
    Transform targetRoom;
    World world;

    float targetZoom;
    Vector3 targetPosition;
    public Vector3 TargetPosition
    {
        set
        {
            float x = Mathf.Clamp(value.x, positionLim[0], positionLim[1]);
            float y = Mathf.Clamp(value.y, positionLim[2], positionLim[3]);
            targetPosition = new Vector3(x, y, 0);
            targetRoom = null;
        }
    }

    public float TargetZoom
    {
        set
        {
            targetZoom = Mathf.Clamp(value, zoomLim[0], zoomLim[1]);

            if (targetRoom == null) Align();
            focus = targetZoom < focusZoom + 0.5f;
            float temp = Mathf.InverseLerp(middleZoom, focusZoom, Camera.main.orthographicSize);
            rotationParent.rotation = Quaternion.Euler(Mathf.Lerp(45, 25, temp), 0, 0);
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetRoom.position, temp);
        }
    }

    void Awake()
    {
        world = GameObject.FindGameObjectWithTag("World").GetComponent<World>();
        rotationParent = transform.parent.parent;
    }

    public void MooveTo(Vector3 target)
    {
        if (!focus)
        {
            TargetPosition = transform.localPosition - target;
            transform.localPosition = targetPosition;
        }
    }

    public void ZoomTo(float target)
    {
        TargetZoom = Camera.main.orthographicSize + target;
        Camera.main.orthographicSize = targetZoom;
    }

    public void RoomClick(Transform room)
    {
        MoveToFocus(room);
    }

    public void BackClick()
    {
        if (Camera.main.orthographicSize < middleZoom)
        {
            Align();
            MoveToMiddle(targetRoom);
        }
        else
        {
            MoveToDefault();
        }
    }

    void Align()
    {
        targetRoom = world.FindNearRoom(transform.localPosition);
    }

    void MoveToDefault()
    {
        transform.localPosition = Vector3.zero;
        rotationParent.rotation = Quaternion.Euler(45, 0, 0);
        Camera.main.orthographicSize = defaultZoom;
    }

    void MoveToMiddle(Transform target)
    {
        transform.localPosition = target.position;
        rotationParent.rotation = Quaternion.Euler(45, 0, 0);
        Camera.main.orthographicSize = middleZoom + 0.5f * (defaultZoom - middleZoom);
    }

    void MoveToFocus(Transform target)
    {
        transform.localPosition = target.position;
        rotationParent.rotation = Quaternion.Euler(25, 0, 0);
        Camera.main.orthographicSize = focusZoom;
    }
}
