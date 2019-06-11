using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour {

    public string location;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider theObject)
    {
        Debug.Log(theObject.gameObject.name + " out of bounds,");

        if (theObject.gameObject.tag == "Player")
        {
            //Destroy(theObject.gameObject);
            if (location != "lava")
            {
                theObject.gameObject.SetActive(false);
            }
            
            int winnerNumber;
            if (theObject.gameObject.name.Contains("1"))
            {
                winnerNumber = 2;
            }
            else
            {
                winnerNumber = 1;
            }

            switch (location) {
                case "water":
                    //splash animation
                    GameObject splashEffect = GameObject.Find("BigSplash");
                    splashEffect.transform.position = theObject.gameObject.transform.position;
                    splashEffect.GetComponent<ParticleSystem>().Play();
                    splashEffect.GetComponent<AudioSource>().Play(0);
                    break;

                case "lava":
                    theObject.gameObject.GetComponent<Rigidbody>().velocity = theObject.transform.TransformDirection(new Vector3(0, 65, 0));
                    GameObject launchEffect = GameObject.Find("LaunchTrail");
                    launchEffect.transform.position = theObject.gameObject.transform.position;
                    launchEffect.GetComponent<ParticleSystem>().Play();
                    launchEffect.GetComponent<AudioSource>().Play(0);
                    break;
            }
            GameObject otherPlayer = GameObject.Find("Player" + winnerNumber);
            Inventory playerInventory = otherPlayer.GetComponent<Inventory>();
            
            if (playerInventory.WinRound())
            {
                GameObject.Find("GameManagement").SendMessage("GameOver");
            }
            else {
                GameObject.Find("GameManagement").SendMessage("ResetGame");
            }
            
            
        }
    }
}
