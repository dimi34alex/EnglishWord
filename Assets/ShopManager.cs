using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public enum Skin
{
    Orange,
    Pink,
    Blue
}
[System.Serializable]
public class SkinColor
{
    public Skin skin;
    public Color color;
    public bool hasSkin;
}
public class ShopManager : MonoBehaviour
{
    public SliderRecolor sliderRecolor;
    public List<SkinColor> skinColor = new List<SkinColor>(2);
    public List<Skin> SkinSelected = new List<Skin>()
    {
        Skin.Orange,
        Skin.Pink,
        Skin.Blue
    };
    List<Button> allButtons = new List<Button>();
    //public int orangeCost;
    public TextMeshProUGUI money;
    public TextMeshProUGUI pinkCostText;
    public TextMeshProUGUI blueCostText;
    public AudioManager audioSource;
    public Button orange;
    public Button pink;
    public Button blue;
    public int pinkCost;
    public int blueCost;

    void Start()
    {
        Load();
        SelectSkin(skinColor[PlayerPrefs.GetInt("skin")]);
        
        orange.onClick.AddListener(() => Buy(Skin.Orange));
        orange.onClick.AddListener(() => SelectSkin(skinColor[0]));

        pink.onClick.AddListener(() => Buy(Skin.Pink));
        pink.onClick.AddListener(() => SelectSkin(skinColor[1]));

        blue.onClick.AddListener(() => Buy(Skin.Blue));
        blue.onClick.AddListener(() => SelectSkin(skinColor[2]));
    }
    public void Buy(Skin skin)
    {
        switch (skin)
        {
            case Skin.Pink:
                if (PlayerPrefs.GetInt("hasPinkButton") == 0 && PlayerPrefs.GetInt("money") >= pinkCost)
                {
                    PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money") - pinkCost);
                    PlayerPrefs.SetInt("hasPinkButton", 1);
                    audioSource.PlayEndLessonSound();
                    Load();
                }
                else if (PlayerPrefs.GetInt("hasPinkButton") == 0 && PlayerPrefs.GetInt("money") < pinkCost)
                {
                    audioSource.PlayWrongAnswerSound();
                }
                break;
            case Skin.Blue:
                if (PlayerPrefs.GetInt("hasBlueButton") == 0 && PlayerPrefs.GetInt("money") >= blueCost)
                {
                    PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money") - blueCost);
                    PlayerPrefs.SetInt("hasBlueButton", 1);
                    audioSource.PlayEndLessonSound();
                    Load();
                }
                else if (PlayerPrefs.GetInt("hasBlueButton") == 0 && PlayerPrefs.GetInt("money") < blueCost)
                {
                    audioSource.PlayWrongAnswerSound();
                }
                break;
            default:
                break;
        }
    }
    public void SelectSkin(SkinColor skin)
    {
        if (skin.hasSkin)
        {
            sliderRecolor.RecolorAllSliders(skin.skin);
            RecolorAllButtons(skin.color);
            PlayerPrefs.SetInt("skin", SkinSelected.IndexOf(skin.skin));
        }
    }
    public void Load()
    {
        money.text = "You have " + PlayerPrefs.GetInt("money").ToString() + "$";
        if (PlayerPrefs.GetInt("hasPinkButton") == 1)
        {
            skinColor[1].hasSkin = true;
            pinkCostText.text = "Sales";
            pink.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = "Select";
        }
        else
        {
            pinkCostText.text = pinkCost.ToString() + "$";
            pink.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = "Buy";
        }
        if (PlayerPrefs.GetInt("hasBlueButton") == 1)
        {
            skinColor[2].hasSkin = true;
            blueCostText.text = "Sales";
            blue.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = "Select";
        }
        else
        {
            blueCostText.text = blueCost.ToString() + "$";
            blue.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = "Buy";
        }
    }
    void RecolorAllButtons(Color color)
    {
        Canvas[] allCanvases = FindObjectsOfType<Canvas>(true);
        foreach (Canvas canvas in allCanvases)
        {
            Button[] buttons = canvas.GetComponentsInChildren<Button>(true);
            allButtons.AddRange(buttons);
        }
        foreach (Button button in allButtons)
        {
            // Проверяем, содержит ли имя кнопки одно из указанных слов: OrangeButton, PinkButton, BlueButton
            if (!button.name.Contains("OrangeButton") &&
                !button.name.Contains("PinkButton") &&
                !button.name.Contains("BlueButton"))
            {
                button.GetComponent<Image>().color = color;
            }
        }
    }
    public void ResetAll()
    {
        PlayerPrefs.DeleteAll();
    }
}
