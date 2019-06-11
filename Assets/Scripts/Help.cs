using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuNav : MonoBehaviour {

    public GameObject previousMenu;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("XboxB"))
        {
            previousMenu.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
