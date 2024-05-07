using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeScript : MonoBehaviour
{
    public AudioSource voiceAS;
    public AudioSource soundsAS;
    public Slider volumeVoice;
    public Slider volumeSounds;
    void Start()
    {
        if (!PlayerPrefs.HasKey("voice")) // Проверяем, установлено ли значение для ключа "myKey"
        {
            PlayerPrefs.SetFloat("voice", 1); // Устанавливаем начальное значение в 10
            PlayerPrefs.Save(); // Сохраняем изменения
        }
        if (!PlayerPrefs.HasKey("sounds")) // Проверяем, установлено ли значение для ключа "myKey"
        {
            PlayerPrefs.SetFloat("sounds", 1); // Устанавливаем начальное значение в 10
            PlayerPrefs.Save(); // Сохраняем изменения
        }
        Load();
        volumeSounds.value = soundsAS.volume;
        volumeVoice.value = voiceAS.volume;
    }
    public void SoundsScript()
    {
        soundsAS.volume = volumeSounds.value;
        Save();
    }
    public void VoiceScript()
    {
        voiceAS.volume = volumeVoice.value;
        Save();
    }
    private void Save()
    {
        PlayerPrefs.SetFloat("voice", voiceAS.volume);
        PlayerPrefs.SetFloat("sounds", soundsAS.volume);
    }
    private void Load()
    {
        voiceAS.volume = PlayerPrefs.GetFloat("voice");
        soundsAS.volume = PlayerPrefs.GetFloat("sounds");
    }
    public void DebugVolume()
    {
        PlayerPrefs.DeleteKey("voice");
        PlayerPrefs.DeleteKey("sounds");
        if (!PlayerPrefs.HasKey("voice")) // Проверяем, установлено ли значение для ключа "myKey"
        {
            PlayerPrefs.SetFloat("voice", 1); // Устанавливаем начальное значение в 10
            PlayerPrefs.Save(); // Сохраняем изменения
        }
        if (!PlayerPrefs.HasKey("sounds")) // Проверяем, установлено ли значение для ключа "myKey"
        {
            PlayerPrefs.SetFloat("sounds", 1); // Устанавливаем начальное значение в 10
            PlayerPrefs.Save(); // Сохраняем изменения
        }
    }
}
