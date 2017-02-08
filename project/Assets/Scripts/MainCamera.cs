using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {

    public Vector3 defaultPosition;
    public Vector4 positionLim;
    public Vector2 zoomLim;
    public float defaultZoom;
    public float focusZoom;
    public float doubleZoom;

    string state = "default";
    string[] states = new string[] {"default", "focus", "doubleFocus"};

    bool mooveBlock = false;

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
        }
    }

    public float TargetZoom
    {
        set
        {
            targetZoom = Mathf.Clamp(value, zoomLim[0], zoomLim[1]);
            rotationParent.rotation = Quaternion.Euler(Mathf.Lerp(25, 45, Mathf.InverseLerp(doubleZoom, focusZoom, targetZoom)), 0, 0);
        }
    }

    void Awake()
    {
        world = GameObject.FindGameObjectWithTag("World").GetComponent<World>();
        rotationParent = transform.parent.parent;
    }

    public void MooveTo(Vector3 target)
    {
        if (!mooveBlock)
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

    public void Align()
    {
        targetRoom = world.FindNearRoom(transform.localPosition);
    }

    void ToFocus(Transform target)
    {
        transform.localPosition = target.position;
        Camera.main.orthographicSize = focusZoom;
    }

    void ToDoubleFocus(Transform target)
    {
        transform.localPosition = target.position;
        Camera.main.orthographicSize = doubleZoom;
    }

    void ToDefault()
    {
        transform.localPosition = Vector3.zero;
        Camera.main.orthographicSize = defaultZoom;
    }

    public void ManageRoom(Transform room)
    {
        Align();
    }

    public void UpZoom()
    {
    }
}
