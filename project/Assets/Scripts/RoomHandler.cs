using UnityEngine;
using UnityEngine.EventSystems;

public class RoomHandler : MonoBehaviour, IPointerClickHandler
{
    MainCamera cam;
 
    void Awake()
    {
        cam = Camera.main.GetComponent<MainCamera>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        eventData.DoubleClick(() => OneTouch(), () => DoubleTouch());
    }

    void OneTouch()
    {
    }

    void DoubleTouch()
    {
        cam.ManageRoom(transform);
    }

    public void TurnActive(bool state)
    {
        gameObject.SetActive(state);
    }
}
