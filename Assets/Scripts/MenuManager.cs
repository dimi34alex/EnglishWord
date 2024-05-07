using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum Screen
{
    Start,
    Settings,
    Shop,
    ChooseTheme,
    ChooseWordList,
    Lesson
}
public enum Theme
{
    None,
    BigData,
    DataSecurity,
    WebDesign
}
public class MenuManager : MonoBehaviour
{
    public TextMeshProUGUI themeText;
    public GameObject canvas;
    public Screen screen;
    public Theme theme;
    public GameObject backButton;
    public List<GameObject> StartScreen;
    public List<GameObject> Settings;
    public List<GameObject> Shop;
    public List<GameObject> themesButtons;
    public List<GameObject> wordListWebDesign;
    public List<GameObject> wordListBigData;
    public List<GameObject> wordListDataSecurity;

    void Start()
    {
        ShowStartScreen();
    }

    public void BackButton()
    {
        switch (screen)
        {
            case Screen.ChooseTheme:
                ShowStartScreen();
                break;
            case Screen.ChooseWordList:
                ShowChooseThemeScreen();
                break;
            case Screen.Settings:
                ShowStartScreen();
                break;
            case Screen.Shop:
                ShowStartScreen();
                break;
        }

    }
    public void ChooseBigDataTheme()
    {
        theme = Theme.BigData;
        themeText.text = "Big Data";

    }
    public void ChooseDataSecurityTheme()
    {
        theme = Theme.DataSecurity;
        themeText.text = "Data Security";
    }
    public void ChooseWebDesigneTheme()
    {
        theme = Theme.WebDesign;
        themeText.text = "Web Design";
    }
    public void ShowStartScreen()
    {
        screen = Screen.Start;
        theme = Theme.None;
        HideCanvas();
        ShowButtons(StartScreen);
    }
    public void ShowSettingsScreen(){
        screen = Screen.Settings;
        theme = Theme.None;
        HideCanvas();
        backButton.SetActive(true);
        ShowButtons(Settings);
    }
    public void ShowShopScreen()
    {
        screen = Screen.Shop;
        theme = Theme.None;
        HideCanvas();
        backButton.SetActive(true);
        ShowButtons(Shop);
    }
    public void ShowChooseThemeScreen()
    {
        themeText.gameObject.SetActive(true);
        themeText.text = "Choose theme";
        screen = Screen.ChooseTheme;
        HideCanvas();
        backButton.SetActive(true);
        ShowButtons(themesButtons, true);
    }
    public void ShowChooseWordListScreen()
    {
        screen = Screen.ChooseWordList;
        HideCanvas();
        backButton.SetActive(true);
        themeText.gameObject.SetActive(true);
        switch (theme)
        {
            case Theme.WebDesign:
                ShowButtons(wordListWebDesign, true);
                break;
            case Theme.BigData:
                ShowButtons(wordListBigData, true);
                break;
            case Theme.DataSecurity:
                ShowButtons(wordListDataSecurity, true);
                break;
        }
    }
    public void Exit()
    {
        Application.Quit(); 
    }
    private void ShowButtons(List<GameObject> buttons, bool show = true)
    {
        if (show)
        {
            foreach (GameObject button in buttons)
            {
                button.SetActive(true);
            }
        }
        else if (!show)
        {
            foreach (GameObject button in buttons)
            {
                button.SetActive(false);
            }
        }
    }
    public void HideCanvas()
    {
        foreach (Transform child in canvas.transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}
