using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class World : MonoBehaviour {

    public Transform[] rooms;

	void Start () {
        rooms = GetComponentsInChildren<Transform>();
    }

    public void TurnOffWithout(Transform target)
    {
        foreach (Transform room in rooms)
        {
            if (room != target && room != transform)
            {
                room.GetComponent<RoomHandler>().TurnActive(false);
            }
        }
    }

    public void TurnOnAll()
    {
        foreach (Transform room in rooms)
        {
            if (room != transform)
            {
                room.GetComponent<RoomHandler>().TurnActive(true);
            }
        }
    }
}
