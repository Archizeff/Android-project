using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class World : MonoBehaviour {

    public GameObject[] rooms;

	void Start () {
        rooms = GameObject.FindGameObjectsWithTag("Room");
    }

    public void TurnOffWithout(GameObject target)
    {
        foreach (GameObject room in rooms)
        {
            if (room != target)
            {
                room.GetComponent<RoomHandler>().TurnActive(false);
            }
        }
    }

    public void TurnOnAll()
    {
        foreach (GameObject room in rooms)
        {
            room.GetComponent<RoomHandler>().TurnActive(true);
        }
    }

    public Transform FindNearRoom(Vector3 currentPos)
    {
        Transform temp = rooms[0].transform; 
        foreach (GameObject room in rooms)
        {
            if (Vector3.Distance(temp.position, currentPos) > Vector3.Distance(room.transform.position, currentPos))
            {
                temp = room.transform;
            }
        }
        return temp;
    }
}
