using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSwitch : MonoBehaviour {

    public AudioClip[] musicTracks;
    private int currentTrack;

    // Use this for initialization
    void Start() {
        currentTrack = 0;
    }

    // Update is called once per frame
    void Update() {

    }

    public void ChangeTrack()
    {
        if (currentTrack == 0)
        {
            currentTrack = 1;
            GetComponent<AudioSource>().clip = musicTracks[currentTrack];
            GetComponent<AudioSource>().Play();

        } else
        {
            currentTrack = 0;
            GetComponent<AudioSource>().clip = musicTracks[currentTrack];
            GetComponent<AudioSource>().Play();
        }
    }
}
