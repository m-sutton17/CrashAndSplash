using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualCameraMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("CamUp"))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
        }

        if (Input.GetButton("CamDown"))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.05f, transform.position.z);
        }

        if (Input.GetButton("CamLeft"))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.05f);
        }

        if (Input.GetButton("CamRight"))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.05f);
        }
    }
}
