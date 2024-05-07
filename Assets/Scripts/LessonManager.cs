using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class LessonManager : MonoBehaviour
{
    public Color colorRight;
    public Color colorWrong;
    public float delayAnswer = 2.5f;
    public AudioManager audioManager;
    public GameObject lessenOver;
    public Slider progress;
    public GameObject goStartScreen;
    public int currentScore;
    public int plusScore = 10;
    public MenuManager menuManager;
    public List<Canvas> Templates;
    public int stages;
    public List<WordSO> WordList1;
    public List<WordSO> WordList2;
    public List<WordSO> WordList3;
    public List<WordSO> WordList4;
    public List<WordSO> WordList5;
    public List<WordSO> WordList6;
    public List<WordSO> WordList7;
    public List<WordSO> WordList8;
    [SerializeField] private List<Canvas> Lesson;
    public List<Button> correctAnswerButton;
    public int currentStage;
    public void GenerateLesson(int numberwordList)
    {
        correctAnswerButton.Clear();
        currentStage = 0;
        currentScore = 0;
        progress.maxValue = stages * plusScore;
        progress.value = 0;
        menuManager.HideCanvas();
        progress.gameObject.SetActive(true);
        goStartScreen.SetActive(true);
        for (int i = 0; i < stages; i++)
        {
            switch (numberwordList)
            {
                case 1:
                    {
                        Lesson.Add(GenerateTemplate(WordList1[i]));
                        break;
                    }
                case 2:
                    {
                        Lesson.Add(GenerateTemplate(WordList2[i]));
                        break;
                    }
                case 3:
                    {
                        Lesson.Add(GenerateTemplate(WordList3[i]));
                        break;
                    }
                case 4:
                    {
                        Lesson.Add(GenerateTemplate(WordList4[i]));
                        break;
                    }
                case 5:
                    {
                        Lesson.Add(GenerateTemplate(WordList5[i]));
                        break;
                    }
                case 6:
                    {
                        Lesson.Add(GenerateTemplate(WordList6[i]));
                        break;
                    }
                case 7:
                    {
                        Lesson.Add(GenerateTemplate(WordList7[i]));
                        break;
                    }
                case 8:
                    {
                        Lesson.Add(GenerateTemplate(WordList8[i]));
                        break;
                    }
            }

        }
        HideCanvas(Lesson);
    }

    public void NextStage(string text, WordSO word, Button button, List<Button> buttons)
    {
        progress.value += plusScore;
        StartCoroutine(Wait());
        if (text == word.translate || text == word.word)
        {
        currentScore += plusScore;
            audioManager.PlayRightAnswerSound();
            button.GetComponent<Image>().color = colorRight;
            Debug.Log("Правильно!");
        }
        else
        {
            correctAnswerButton[currentStage].GetComponent<Image>().color = colorRight;
            audioManager.PlayWrongAnswerSound();
            button.GetComponent<Image>().color = colorWrong;
            Debug.Log("Неправильно!");
        }
        if (currentStage + 1 == stages)
        {
            audioManager.PlayEndLessonSound();
            LessenOver();
            return;
        }
        foreach (Button button1 in buttons)
        {
            button1.interactable = false;
        }
        currentStage++;
    }
    public void LessenOver()
    {
        PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money") + currentScore);
        HideCanvas(Lesson);
        ShowLessenOver();
        progress.gameObject.SetActive(false);
        goStartScreen.SetActive(false);
        Debug.Log("Урок окончен!");
        DelLesson();
    }
    private Canvas GenerateTemplate(WordSO word)
    {
        int templateNumber = Random.Range(0, Templates.Count);
        //templateNumber = 4;
        switch (templateNumber)
        {
            case 0:
                {
                    return GenerateTranslateOnRussian(word);
                }
            case 1:
                {
                    return GenerateTranslateOnEnglish(word);
                }
            case 2:
                {
                    return GenerateAudio(word);
                }
            case 3:
                {
                    return GenerateSentence(word);
                }
            case 4:
                {
                    return GenerateExplonation(word);
                }
            default:
                {
                    return null;
                }
        }

    }

    private Canvas GenerateExplonation(WordSO word)
    {
        List<Button> buttons = new List<Button>();
        Canvas template = Instantiate(Templates[4]);
        List<string> uniqueTranslates = new List<string>();

        foreach (Transform child in template.transform)
        {
            if (child.name == "Explonation")
            {
                child.GetComponent<TextMeshProUGUI>().text = word.explonation;
            }
            if (child.GetComponent<Button>())
            {
                Button button = child.GetComponent<Button>();
                buttons.Add(button);
                TextMeshProUGUI text = child.GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
                RandomWords(word.wordList, word, text, uniqueTranslates);
                button.onClick.AddListener(() => NextStage(text.text, word, button, buttons));
            }
        }
        GenerateCorrectWord(buttons, word);
        return template;
    }

    private Canvas GenerateSentence(WordSO word)
    {
        List<Button> buttons = new List<Button>();

        Canvas template = Instantiate(Templates[3]);
        List<string> uniqueTranslates = new List<string>();
        foreach (Transform child in template.transform)
        {
            if (child.name == "Sentences")
            {
                child.GetComponent<TextMeshProUGUI>().text = word.sentences[Random.Range(0, word.sentences.Count)];
            }
            if (child.GetComponent<Button>())
            {
                Button button = child.GetComponent<Button>();
                buttons.Add(button);
                TextMeshProUGUI text = child.GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
                RandomWords(word.wordList, word, text, uniqueTranslates);
                button.onClick.AddListener(() => NextStage(text.text, word, button, buttons));
            }
        }
        GenerateCorrectWord(buttons, word);
        return template;
    }

    private Canvas GenerateAudio(WordSO word)
    {
        List<Button> buttons = new List<Button>();

        List<string> uniqueTranslates = new List<string>();
        Canvas template = Instantiate(Templates[2]);
        foreach (Transform child in template.transform)
        {
            if (child.name == "Audio")
            {
                child.GetComponent<Button>().onClick.AddListener(() => audioManager.PlaySound(word));
                continue;
            }
            if (child.GetComponent<Button>())
            {
                Button button = child.GetComponent<Button>();
                buttons.Add(button);
                TextMeshProUGUI text = child.GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
                RandomWords(word.wordList, word, text, uniqueTranslates);
                button.onClick.AddListener(() => NextStage(text.text, word, button, buttons));
            }
        }
        GenerateCorrectWord(buttons, word);
        return template;
    }
    private Canvas GenerateTranslateOnRussian(WordSO word)
    {
        List<Button> buttons = new List<Button>();

        List<string> uniqueTranslates = new List<string>();
        Canvas template = Instantiate(Templates[0]);
        foreach (Transform child in template.transform)
        {
            if (child.name == "Translate")
            {
                child.GetComponent<TextMeshProUGUI>().text = "Переведи на русский: " + word.word;
            }
            if (child.GetComponent<Button>())
            {
                Button button = child.GetComponent<Button>();
                buttons.Add(button);
                TextMeshProUGUI text = child.GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
                RandomWords(word.wordList, word, text, uniqueTranslates, true);
                button.onClick.AddListener(() => NextStage(text.text, word, button, buttons));
            }
        }
        GenerateCorrectWord(buttons, word, true);
        return template;
    }
    private Canvas GenerateTranslateOnEnglish(WordSO word)
    {
        List<Button> buttons = new List<Button>();

        List<string> uniqueTranslates = new List<string>();
        Canvas template = Instantiate(Templates[1]);
        foreach (Transform child in template.transform)
        {
            if (child.name == "Translate")
            {
                child.GetComponent<TextMeshProUGUI>().text = "Переведи на английский: " + word.translate;
            }
            if (child.GetComponent<Button>())
            {
                Button button = child.GetComponent<Button>();
                buttons.Add(button);
                TextMeshProUGUI text = child.GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
                string translate = WordList7[Random.Range(0, WordList7.Count)].word;
                RandomWords(word.wordList, word, text, uniqueTranslates);
                button.onClick.AddListener(() => NextStage(text.text, word, button, buttons));
            }
        }
        GenerateCorrectWord(buttons, word);
        return template;
    }

    private void HideCanvas(List<Canvas> Lesson)
    {
        for (int i = 0; i < Lesson.Count; i++)
        {
            if (i == currentStage)
            {
                Lesson[i].gameObject.SetActive(true);
            }
            else
            {
                Lesson[i].gameObject.SetActive(false);
            }
        }
    }
    private void DelLesson()
    {
        foreach (Canvas canvas in Lesson)
        {
            Destroy(canvas.gameObject);
        }
        Lesson.Clear();
    }
    public void ShowLessenOver(bool hide = false)
    {

        foreach (Transform child in lessenOver.transform)
        {
            if (hide)
            {
                child.gameObject.SetActive(false);
            }
            else if (!hide)
            {
                child.gameObject.SetActive(true);
                if (child.name == "Prize")
                {
                    child.GetComponent<TextMeshProUGUI>().text = "Ты получил " + currentScore + " монет!";
                }
            }
        }
    }
    private void GenerateCorrectWord(List<Button> buttons, WordSO correctWord, bool russian = false)
    {

        Button rightAnswer = buttons[Random.Range(0, buttons.Count)];
        if (russian)
        {
            rightAnswer.GetComponentInChildren<TextMeshProUGUI>().text = correctWord.translate;
        }
        else
        {
            rightAnswer.GetComponentInChildren<TextMeshProUGUI>().text = correctWord.word;
        }
        rightAnswer.onClick.RemoveAllListeners();
        correctAnswerButton.Add(rightAnswer);
        rightAnswer.onClick.AddListener(() => NextStage(correctWord.word, correctWord, rightAnswer, buttons));
    }
    private void RandomWords(int wordlist, WordSO word, TextMeshProUGUI text, List<string> uniqueTranslates, bool russian = false)
    {
        string randomWord;
        if (russian)
        {
            randomWord = GenerateRandomWord(wordlist).translate;
            while (randomWord == word.translate || uniqueTranslates.Contains(randomWord))
            {
                randomWord = GenerateRandomWord(wordlist).translate;
            }
        }
        else
        {
            randomWord = GenerateRandomWord(wordlist).word;
            while (randomWord == word.word || uniqueTranslates.Contains(randomWord))
            {
                randomWord = GenerateRandomWord(wordlist).word;
            }
        }

        if (!uniqueTranslates.Contains(randomWord))
        {
            text.text = randomWord;
            uniqueTranslates.Add(text.text);
        }
    }
    private WordSO GenerateRandomWord(int wordlist)
    {
        WordSO randomWord;
        switch (wordlist)
        {
            case 1:
                {
                    randomWord = WordList1[Random.Range(0, WordList1.Count)];
                    break;
                }
            case 2:
                {
                    randomWord = WordList2[Random.Range(0, WordList2.Count)];
                    break;
                }
            case 3:
                {
                    randomWord = WordList3[Random.Range(0, WordList3.Count)];
                    break;
                }
            case 4:
                {
                    randomWord = WordList4[Random.Range(0, WordList4.Count)];
                    break;
                }
            case 5:
                {
                    randomWord = WordList5[Random.Range(0, WordList5.Count)];
                    break;
                }
            case 6:
                {
                    randomWord = WordList6[Random.Range(0, WordList6.Count)];
                    break;
                }
            case 7:
                {
                    randomWord = WordList7[Random.Range(0, WordList7.Count)];
                    break;
                }
            case 8:
                {
                    randomWord = WordList8[Random.Range(0, WordList8.Count)];
                    break;
                }
            default:
                {
                    randomWord = null;
                    break;
                }

        }
        return randomWord;
    }
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(delayAnswer);
        HideCanvas(Lesson);
        Lesson[currentStage].gameObject.SetActive(true);
    }
}


