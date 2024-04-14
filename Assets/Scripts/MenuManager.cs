using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum Screen
{
    Start,
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
    //создать массив кнопок, чтобы в цикле их включать и выключать
    public TextMeshProUGUI themeText;
    public GameObject canvas;
    public Screen screen;
    public Theme theme;
    public GameObject backButton;
    public GameObject startButton;
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
        startButton.SetActive(true);
    }
    public void ShowChooseThemeScreen()
    {
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
