using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    private GameObject P1;
    public Vector3 P1SpawnLocation;
    public Vector3 P1SpawnRotation;
    private GameObject P2;
    public Vector3 P2SpawnLocation;
    public Vector3 P2SpawnRotation;

    public GameObject pauseMenu;
    public GameObject endMenu;

    public GameObject mainCamera;
    public GameObject stageViewCamera;

    private GameObject startText;
    private GameObject endText;

    // Use this for initialization
    void Start() {
        P1 = GameObject.Find("Player1");
        P2 = GameObject.Find("Player2");
        startText = GameObject.Find("StartText");
        endText = GameObject.Find("EndText");
        SetUpGameObjects();
        StartCoroutine("StartRoundSequence");
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("XboxStart"))
        {
            pauseGame();
        }
    }

    void SetUpGameObjects()
    {
        P1.SetActive(true);
        P1.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
        P1.GetComponent<Rigidbody>().MovePosition(P1SpawnLocation);
        P1.transform.rotation = Quaternion.Euler(P1SpawnRotation.x, P1SpawnRotation.y, P1SpawnRotation.z);
        P1.GetComponent<Inventory>().ResetItem();

        P2.SetActive(true);
        P2.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        P2.GetComponent<Rigidbody>().MovePosition(P2SpawnLocation);
        P2.transform.rotation = Quaternion.Euler(P2SpawnRotation.x, P2SpawnRotation.y, P2SpawnRotation.z);
        P2.GetComponent<Inventory>().ResetItem();
    }

    void pauseGame()
    {
        Time.timeScale = 0.0f;

        pauseMenu.SetActive(true);

    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }

    void ResetGame()
    {
        StartCoroutine("EndRoundSequence");
    }

    void GameOver()
    {
        StartCoroutine("EndSequence");
    }

    void RestartGame ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1.0f;
    }

    void ClearItems()
    {
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        foreach (GameObject item in items)
        {
            GameObject.Destroy(item);
        }
    }

    void QuitGame()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1.0f;
    }

    IEnumerator StartRoundSequence()
    {
        P1.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().toggleActiveControls(false);
        P2.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().toggleActiveControls(false);
        ClearItems();
        SetUpGameObjects();
        startText.GetComponent<Text>().enabled = true;
        startText.GetComponent<Text>().text = "READY";
        yield return new WaitForSeconds(1.5f);
        startText.GetComponent<AudioSource>().Play();
        startText.GetComponent<Text>().text = "GO!";
        yield return new WaitForSeconds(0.5f);
        startText.GetComponent<Text>().enabled = false;
        P1.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().toggleActiveControls(true);
        P2.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().toggleActiveControls(true);
    }

    IEnumerator EndSequence()
    {
        Time.timeScale = 0.6f;
        mainCamera.SetActive(false);
        stageViewCamera.SetActive(true);
        yield return new WaitForSeconds(1.5f);

        int playerWon = 0;
        if (P1.GetComponent<Inventory>().roundsWon == 2)
        {
            playerWon = 1;
            P2.SetActive(false);
        } else if (P2.GetComponent<Inventory>().roundsWon == 2)
        {
            playerWon = 2;
            P1.SetActive(false);
        }
        mainCamera.SetActive(true);
        stageViewCamera.SetActive(false);
        Time.timeScale = 1.0f;

        endText.GetComponent<Text>().enabled = true;
        endText.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(3.0f);
        endText.GetComponent<Text>().enabled = false;
        Time.timeScale = 0.0f;
        endMenu.SetActive(true);
        
        GameObject winnerText = endMenu.transform.Find("Winner").gameObject;
        winnerText.GetComponent<Text>().text = "Player " + playerWon + " WINS!";
    }

    IEnumerator EndRoundSequence()
    {
        Time.timeScale = 0.6f;
        mainCamera.SetActive(false);
        stageViewCamera.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        mainCamera.SetActive(true);
        stageViewCamera.SetActive(false);
        Time.timeScale = 1.0f;

        StartCoroutine("StartRoundSequence");
    }
}
