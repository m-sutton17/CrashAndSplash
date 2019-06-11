using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {

    private Rigidbody rb;

    public Rigidbody ballImpactPrefab;

    // Use this for initialization
    void Start () {
		 rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision theObject)
    {
        Vector3 externalForce = new Vector3();

        switch (theObject.gameObject.name)
        {
            case "throwBall":
                //burst animation
                Rigidbody effect = Instantiate(ballImpactPrefab, gameObject.transform.position, gameObject.transform.rotation);

                externalForce = new Vector3(theObject.rigidbody.velocity.x * 50.0f, theObject.rigidbody.velocity.y + 25.0f, theObject.rigidbody.velocity.z * 50.0f);
                rb.gameObject.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().playerHit(externalForce,0.25f);
                Destroy(theObject.gameObject);
                break;

            case "":
                break;
                
        }
    }
}
