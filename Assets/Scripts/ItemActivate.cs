using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemActivate : MonoBehaviour
{
    Inventory playerInventory;

    //public AudioClip throwSound;
    public Rigidbody ballPrefab;
    public Rigidbody bombPrefab;
    public float throwSpeed = 30.0f;

    // Use this for initialization
    void Start()
    {
        playerInventory = transform.parent.gameObject.GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void UseItem()
    {
        string item = playerInventory.itemHolding;
        switch (item)
        {
            case "ball":
                throwBall();
                break;
            case "bomb":
                placeBomb();
                break;
            case "":
                break;
        }
        SendMessageUpwards("ResetItem");

    }

    void throwBall()
    {
        gameObject.GetComponent<AudioSource>().Play();
        Rigidbody newBall = Instantiate(ballPrefab, transform.position, transform.rotation) as Rigidbody;
        newBall.name = "throwBall";
        newBall.velocity = transform.forward * throwSpeed;
        Physics.IgnoreCollision(transform.root.GetComponent<Collider>(), newBall.GetComponent<Collider>(), true);
    }

    void placeBomb()
    {
        Vector3 spawnLocation = new Vector3(transform.position.x, bombPrefab.transform.position.y, transform.position.z);
        Quaternion spawnRotation = new Quaternion(0.0f, 0.0f, 0.0f, 1);
        Rigidbody newBomb = Instantiate(bombPrefab, spawnLocation, spawnRotation) as Rigidbody;
        newBomb.name = "activeBomb";
        if (transform.parent.name.Contains("1"))
        {
            newBomb.gameObject.BroadcastMessage("SetIndicatorText",1.0f);
        } else if (transform.parent.name.Contains("2"))
        {
            newBomb.gameObject.BroadcastMessage("SetIndicatorText",2.0f);
        } else
        {
            newBomb.gameObject.BroadcastMessage("SetIndicatorText",0.0f);
        }
        
        Physics.IgnoreCollision(transform.root.GetComponent<Collider>(), newBomb.GetComponent<Collider>(), true);
        StartCoroutine("StartBombTimer", newBomb);
    }

    IEnumerator StartBombTimer(Rigidbody newBomb)
    {
        newBomb.GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        newBomb.GetComponent<Collider>().enabled = true;
    }

}
