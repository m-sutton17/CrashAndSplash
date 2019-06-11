using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    private GameObject P1;
    private GameObject P2;

    public Vector3 offset;
    public float height;

    private Vector3 newPos = new Vector3();

    // Use this for initialization
    void Start () {
		P1 = GameObject.Find("Player1");
        P2 = GameObject.Find("Player2");

        offset.x = transform.position.x - (P1.transform.position.x + ((P2.transform.position.x - P1.transform.position.x) / 2));
        offset.y = transform.position.y - height;
        offset.z = transform.position.z - (newPos.z = P2.transform.position.z + ((P1.transform.position.z - P2.transform.position.z) / 2));
    }
	

	void LateUpdate () {

        if (P1.activeSelf == false)
        {
            newPos.x = P2.transform.position.x;
            newPos.z = P2.transform.position.z;
        } else if (P2.activeSelf == false)
        {
            newPos.x = P1.transform.position.x;
            newPos.z = P1.transform.position.z;
        } else
        {
            if (P1.transform.position.x > P2.transform.position.x)
            {
                newPos.x = P2.transform.position.x + ((P1.transform.position.x - P2.transform.position.x) / 2);
            }
            else
            {
                newPos.x = P1.transform.position.x + ((P2.transform.position.x - P1.transform.position.x) / 2);
            }

            if (P1.transform.position.z > P2.transform.position.z)
            {
                newPos.z = P2.transform.position.z + ((P1.transform.position.z - P2.transform.position.z) / 2);
            }
            else
            {
                newPos.z = P1.transform.position.z + ((P2.transform.position.z - P1.transform.position.z) / 2);
            }
        }

        newPos.y = height;

        transform.position = newPos + offset;
    }
}
