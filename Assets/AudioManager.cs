using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;


    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(WordSO word)
    {
        audioSource.PlayOneShot(word.audio);
    }
}
