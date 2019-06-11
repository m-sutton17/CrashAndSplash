using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class SelectStage : MonoBehaviour {

    public GameObject previousMenu;

    public Texture2D[] stagePreviews;
    public RawImage previewGUI;

    public VideoClip[] videoPreviews;
    public RawImage videoPreviewGUI;
    public VideoPlayer player;

    private int selectedStage = 0;

    private bool moveEnabled;

    // Use this for initialization
    void Start () {
        updateUI();
        moveEnabled = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("XboxA"))
        {
            startGame();
        }

        if (Input.GetButtonDown("XboxB"))
        {
            previousMenu.SetActive(true);
            gameObject.SetActive(false);
        }

        if (moveEnabled)
        {
            if (Input.GetAxis("LeftJoystickX") > 0.1)
            {
                if (selectedStage != stagePreviews.Length -1)
                {
                    selectedStage = selectedStage + 1;
                }
                else
                {
                    selectedStage = 0;
                }
                updateUI();
            }
            else if (Input.GetAxis("LeftJoystickX") < -0.1)
            {

                if (selectedStage != 0)
                {
                    selectedStage = selectedStage - 1;
                }
                else
                {
                    selectedStage = stagePreviews.Length -1;
                }
                updateUI();
            }
            StartCoroutine("PreventRepeatInput");
        }
    }

    void updateUI ()
    {
        //change preview image/video
        //previewGUI.texture = stagePreviews[selectedStage];
        player.clip = videoPreviews[selectedStage];

        GameObject nametext = gameObject.transform.Find("StageName").gameObject;
        GameObject descriptionText = gameObject.transform.Find("StageDescription").gameObject;

        switch (selectedStage)
        {
            case 0:         //water
                nametext.GetComponent<Text>().text = "Beachfront";
                descriptionText.GetComponent<Text>().text = "Fight it out at Crash City's most celebrated event.";
                break;

            case 1:         //lava
                nametext.GetComponent<Text>().text = "Mount Splash";
                descriptionText.GetComponent<Text>().text = "Bring the heat to your opponent in the iconic landmark volcano, Mount Splash!";
                break;
        }
        
    }

    void startGame()
    {
        int stageNumber = selectedStage + 1;
        SceneManager.LoadScene(stageNumber);
    }

    IEnumerator PreventRepeatInput()
    {
        moveEnabled = false;
        yield return new WaitForSeconds(0.175f);
        moveEnabled = true;

    }

    private void OnEnable()
    {
        moveEnabled = true;
        GameObject.Find("MenuMusic").SendMessage("ChangeTrack");
    }

    private void OnDisable()
    {
        GameObject.Find("MenuMusic").SendMessage("ChangeTrack");
    }
}
