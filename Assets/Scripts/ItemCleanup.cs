using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCleanup : MonoBehaviour {

    public float removeTime = 3.0f;

    // Use this for initialization
    void Start () {
        Destroy(gameObject, removeTime);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
