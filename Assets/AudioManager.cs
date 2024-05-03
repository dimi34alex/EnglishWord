using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioSource SoundAudioSorce;
    [SerializeField] AudioClip rightAnswer;
    [SerializeField] AudioClip wrongAnswer;
    [SerializeField] AudioClip endLesson;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(WordSO word)
    {
        audioSource.PlayOneShot(word.audio);
    }
    public void PlayRightAnswerSound()
    {
        SoundAudioSorce.PlayOneShot(rightAnswer);
    }
    public void PlayWrongAnswerSound()
    {
        SoundAudioSorce.PlayOneShot(wrongAnswer);
    }
    public void PlayEndLessonSound()
    {
        SoundAudioSorce.PlayOneShot(endLesson);
    }
}
