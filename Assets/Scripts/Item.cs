using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    public float rotationSpeed = 100.0f;

    public Rigidbody pickupEffect;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.SendMessage("ItemPickup", gameObject.name);
            Rigidbody effect = Instantiate(pickupEffect, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
    }
}
