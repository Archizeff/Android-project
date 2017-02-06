using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {

    public Vector4 positionLim;
    public Vector2 zoomLim;

    float targetZoom;
    Vector3 targetPosition;
    public Vector3 TargetPosition
    {
        set
        {
            float x = Mathf.Clamp(value.x, positionLim[0], positionLim[1]);
            float y = Mathf.Clamp(value.y, positionLim[2], positionLim[3]);
            float z = Z(y);
            targetPosition = new Vector3(x, y, z);
        }
    }

    public float TargetZoom
    {
        set
        {
            targetZoom = Mathf.Clamp(value, zoomLim[0], zoomLim[1]);
        }
    }

    public void MooveTo(Vector3 target)
    {
        TargetPosition = transform.position - target;
        transform.position = targetPosition;
    }

    public void ZoomTo(float target)
    {
        TargetZoom = Camera.main.orthographicSize + target;
        Camera.main.orthographicSize = targetZoom;
    }

    float Z(float x)
    {
        return x - 60;
    }
}
