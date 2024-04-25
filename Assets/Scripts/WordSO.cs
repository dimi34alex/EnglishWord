using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Word", menuName = "Word Data", order = 51)]
public class WordSO : ScriptableObject
{
    public string word;
    public string translate;
    public AudioClip audio;
    public List<string> sentences;
    public string explonation;
    public int wordList;

    private void OnEnable()
    {
        word = name;
    }
}
