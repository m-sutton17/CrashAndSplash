using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour {

    public Rigidbody ballPrefab;
    public Rigidbody bombPrefab;

    // Use this for initialization
    void Start () {
        InvokeRepeating("spawnItem", 2.0f, 2.0f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void spawnItem()
    {
        float xPosition = transform.position.x + Random.Range(-5, 5);
        float yPosition = transform.position.y;
        float zPosition = transform.position.z + Random.Range(-5, 5);
        //Vector3 spawnPosition = new Vector3(xPosition, yPosition, zPosition);

        float itemRandomiser = Random.Range(0.0f, 10.0f);
        if (itemRandomiser <= 2.5)
        {
            yPosition = bombPrefab.transform.position.y;
            Vector3 spawnPosition = new Vector3(xPosition, yPosition, zPosition);
            Rigidbody newItem = Instantiate(bombPrefab, spawnPosition, transform.rotation) as Rigidbody;
            newItem.name = "bomb";
        } else if (itemRandomiser > 2.5)
        {
            yPosition = ballPrefab.transform.position.y;
            Vector3 spawnPosition = new Vector3(xPosition, yPosition, zPosition);
            Rigidbody newItem = Instantiate(ballPrefab, spawnPosition, transform.rotation) as Rigidbody;
            newItem.name = "ball";
        }
        
    }
}
