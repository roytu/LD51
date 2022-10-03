using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRandomizer : MonoBehaviour
{
    public AudioClip[] audioClips;
    void Start()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        int r = Random.Range(0, audioClips.Length);
        AudioClip clip = audioClips[r];
        audioSource.clip = clip;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
