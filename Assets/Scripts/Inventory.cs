using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    [HideInInspector]
    public string itemHolding = "";
    [HideInInspector]
    public int itemIndex = 0;

    [HideInInspector]
    public int roundsWon = 0;

    //HUD
    public Texture2D[] hudItem;
    public RawImage itemHudGUI;

    public Texture2D[] hudRounds;
    public RawImage roundsHudGUI;


    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
    }

    void ItemPickup(string itemName)
    {
        itemHolding = itemName;
        switch (itemHolding)
        {
            case "ball":
                itemIndex = 1;
                break;
            case "bomb":
                itemIndex = 2;
                break;
        }
        
        UpdateUI();
    }

    public void ResetItem()
    {
        itemHolding = "";
        itemIndex = 0;
        UpdateUI();
    }

    void UpdateUI()
    {
        itemHudGUI.texture = hudItem[itemIndex];
        roundsHudGUI.texture = hudRounds[roundsWon];
    }

    public bool WinRound()
    {
        roundsWon = roundsWon + 1;
        UpdateUI();

        if (roundsWon == 2)
        {
            return true;
        } else
        {
            return false;
        }
    }
}
