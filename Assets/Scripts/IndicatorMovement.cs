using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorMovement : MonoBehaviour {

    private float yPos;

	// Use this for initialization
	void Start () {
        yPos = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.parent.transform.position.x,yPos, transform.parent.transform.position.z);
	}
} 
