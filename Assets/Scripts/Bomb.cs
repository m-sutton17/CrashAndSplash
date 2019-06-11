using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    GameObject playerIndicator;
    public Rigidbody bombSplashPrefab;

    // Use this for initialization
    void Start () {
        playerIndicator = gameObject.transform.Find("PlayerOwned").gameObject;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            //StartCoroutine("deSpawn");

            //splash animation
            Rigidbody effect = Instantiate(bombSplashPrefab, gameObject.transform.position, gameObject.transform.rotation);

            //set force of explosion on player
            Vector3 externalForce = new Vector3();
            Rigidbody playerRB = col.gameObject.GetComponent<Rigidbody>();
            Debug.Log(col.gameObject.name + " bomb triggered");

            if (Vector3.Distance(playerRB.position, gameObject.transform.position) < 2)
            {
                externalForce.x = (playerRB.position.x - gameObject.transform.position.x) * 500.0f;
                externalForce.y = (playerRB.position.y - gameObject.transform.position.y) + 15.0f;
                externalForce.z = (playerRB.position.z - gameObject.transform.position.z) * 500.0f;
            } else
            {
                externalForce.x = (playerRB.position.x - gameObject.transform.position.x) * 200.0f;
                externalForce.y = (playerRB.position.y - gameObject.transform.position.y) + 15.0f;
                externalForce.z = (playerRB.position.z - gameObject.transform.position.z) * 200.0f;
            }
            
            //exert force
            col.gameObject.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().playerHit(externalForce,0.5f);

            Destroy(gameObject);
        }
    }

    public void SetIndicatorText(float playerNumber)
    {
        playerIndicator = gameObject.transform.Find("PlayerOwned").gameObject;
        TextMesh indText = playerIndicator.GetComponent<TextMesh>();
        indText.text = "P" + playerNumber;
        if (playerNumber == 1)
        {
            indText.color = new Color(186, 0, 0);
        }
        else if (playerNumber == 2)
        {
            indText.color = new Color(0, 74, 150);
        } else
        {
            indText.color = new Color(0, 0, 0);
        }

    }
}
