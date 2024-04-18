using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;


public class LessonManager : MonoBehaviour
{
    public AudioManager audioManager;
    public GameObject lessenOver;
    public Slider progress;
    public int currentScore;
    public int plusScore = 10;
    public MenuManager menuManager;
    public List<Canvas> Templates;
    public int stages;
    public List<WordSO> WordList6;
    public List<WordSO> WordList7;
    public List<WordSO> WordList8;
    [SerializeField] private List<Canvas> Lesson;
    public int currentStage;
    public void GenerateLesson(int numberwordList)
    {
        currentStage = 0;
        currentScore = 0;
        progress.maxValue = stages * plusScore;
        progress.value = 0;
        menuManager.HideCanvas();
        progress.gameObject.SetActive(true);
        for (int i = 0; i < stages; i++)
        {
            switch (numberwordList)
            {
                case 1:
                {
                    break;
                }
                case 2:
                {
                    break;
                }
                case 3:
                {
                    break;
                }
                case 4:
                {
                    break;
                }
                case 5:
                {
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

    public void NextStage(string text, WordSO word)
    {
        currentStage++;
        if (text == word.translate || text == word.word)
        {
            currentScore += plusScore;
            progress.value = currentScore;
            Debug.Log("Правильно!");
        }
        else
        {
            Debug.Log("Неправильно!");
        }
        if (currentStage == stages)
        {
            HideCanvas(Lesson);
            ShowLessenOver();
            progress.gameObject.SetActive(false);
            Debug.Log("Урок окончен!");
            DelLesson();
            return;
        }
        HideCanvas(Lesson);
        Lesson[currentStage].gameObject.SetActive(true);
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
        Canvas template = Instantiate(Templates[4]);
        List<string> uniqueTranslates = new List<string>();
        List<Button> buttons = new List<Button>();
        foreach (Transform child in template.transform)
        {
            if (child.name == "Explonation")
            {
                child.GetComponent<TextMeshProUGUI>().text = word.explonation;
            }
            if (child.GetComponent<Button>())
            {
                buttons.Add(child.GetComponent<Button>());
                TextMeshProUGUI text = child.GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
                string translate = WordList7[Random.Range(0, WordList7.Count)].word;
                while (translate == word.word || uniqueTranslates.Contains(translate))
                {
                    translate = WordList7[Random.Range(0, WordList7.Count)].word;
                }
                if (!uniqueTranslates.Contains(translate))
                {
                    text.text = translate;
                    uniqueTranslates.Add(text.text);
                }
                child.GetComponent<Button>().onClick.AddListener(() => NextStage(text.text, word));
            }
        }
        Button rightAnswer = buttons[Random.Range(0, buttons.Count)];
        rightAnswer.GetComponentInChildren<TextMeshProUGUI>().text = word.word;
        rightAnswer.onClick.RemoveAllListeners();
        rightAnswer.onClick.AddListener(() => NextStage(word.word, word));

        return template;
    }

    private Canvas GenerateSentence(WordSO word)
    {
        Canvas template = Instantiate(Templates[3]);
        List<string> uniqueTranslates = new List<string>();
        List<Button> buttons = new List<Button>();
        foreach (Transform child in template.transform)
        {
            if (child.name == "Sentences")
            {
                child.GetComponent<TextMeshProUGUI>().text = word.sentences[Random.Range(0, word.sentences.Count)];
            }
            if (child.GetComponent<Button>())
            {
                buttons.Add(child.GetComponent<Button>());
                TextMeshProUGUI text = child.GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
                string translate = WordList7[Random.Range(0, WordList7.Count)].word;
                while (translate == word.word || uniqueTranslates.Contains(translate))
                {
                    translate = WordList7[Random.Range(0, WordList7.Count)].word;
                }
                if (!uniqueTranslates.Contains(translate))
                {
                    text.text = translate;
                    uniqueTranslates.Add(text.text);
                }
                child.GetComponent<Button>().onClick.AddListener(() => NextStage(text.text, word));
            }
        }
        Button rightAnswer = buttons[Random.Range(0, buttons.Count)];
        rightAnswer.GetComponentInChildren<TextMeshProUGUI>().text = word.word;
        rightAnswer.onClick.RemoveAllListeners();
        rightAnswer.onClick.AddListener(() => NextStage(word.word, word));

        return template;
    }

    private Canvas GenerateAudio(WordSO word)
    {
        List<string> uniqueTranslates = new List<string>();
        Canvas template = Instantiate(Templates[2]);
        List<Button> buttons = new List<Button>();
        foreach (Transform child in template.transform)
        {
            if (child.name == "Audio")
            {
                child.GetComponent<Button>().onClick.AddListener(() => audioManager.PlaySound(word));
                continue;
            }
            if (child.GetComponent<Button>())
            {
                buttons.Add(child.GetComponent<Button>());
                TextMeshProUGUI text = child.GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
                string translate = WordList7[Random.Range(0, WordList7.Count)].word;
                while (translate == word.word || uniqueTranslates.Contains(translate))
                {
                    translate = WordList7[Random.Range(0, WordList7.Count)].word;
                }
                if (!uniqueTranslates.Contains(translate))
                {
                    text.text = translate;
                    uniqueTranslates.Add(text.text);
                }
                child.GetComponent<Button>().onClick.AddListener(() => NextStage(text.text, word));
            }
        }
        Button rightAnswer = buttons[Random.Range(0, buttons.Count)];
        rightAnswer.GetComponentInChildren<TextMeshProUGUI>().text = word.word;
        rightAnswer.onClick.RemoveAllListeners();
        rightAnswer.onClick.AddListener(() => NextStage(word.word, word));

        return template;
    }
    private Canvas GenerateTranslateOnRussian(WordSO word)
    {
        List<string> uniqueTranslates = new List<string>();
        Canvas template = Instantiate(Templates[0]);
        List<Button> buttons = new List<Button>();
        foreach (Transform child in template.transform)
        {
            if (child.name == "Translate")
            {
                child.GetComponent<TextMeshProUGUI>().text = "Переведи на русский: " + word.word;
            }
            if (child.GetComponent<Button>())
            {
                buttons.Add(child.GetComponent<Button>());
                TextMeshProUGUI text = child.GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
                string translate = WordList7[Random.Range(0, WordList7.Count)].translate;
                while (translate == word.translate || uniqueTranslates.Contains(translate))
                {
                    translate = WordList7[Random.Range(0, WordList7.Count)].translate;
                }
                if (!uniqueTranslates.Contains(translate))
                {
                    text.text = translate;
                    uniqueTranslates.Add(text.text);
                }
                child.GetComponent<Button>().onClick.AddListener(() => NextStage(text.text, word));
            }
        }
        Button rightAnswer = buttons[Random.Range(0, buttons.Count)];
        rightAnswer.GetComponentInChildren<TextMeshProUGUI>().text = word.translate;
        rightAnswer.onClick.RemoveAllListeners();
        rightAnswer.onClick.AddListener(() => NextStage(word.translate, word));

        return template;
    }
    private Canvas GenerateTranslateOnEnglish(WordSO word)
    {
        List<string> uniqueTranslates = new List<string>();
        Canvas template = Instantiate(Templates[1]);
        List<Button> buttons = new List<Button>();
        foreach (Transform child in template.transform)
        {
            if (child.name == "Translate")
            {
                child.GetComponent<TextMeshProUGUI>().text = "Переведи на английский: " + word.translate;
            }
            if (child.GetComponent<Button>())
            {
                buttons.Add(child.GetComponent<Button>());
                TextMeshProUGUI text = child.GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
                string translate = WordList7[Random.Range(0, WordList7.Count)].word;
                while (translate == word.word || uniqueTranslates.Contains(translate))
                {
                    translate = WordList7[Random.Range(0, WordList7.Count)].word;
                }
                if (!uniqueTranslates.Contains(translate))
                {
                    text.text = translate;
                    uniqueTranslates.Add(text.text);
                }
                child.GetComponent<Button>().onClick.AddListener(() => NextStage(text.text, word));
            }
        }
        Button rightAnswer = buttons[Random.Range(0, buttons.Count)];
        rightAnswer.GetComponentInChildren<TextMeshProUGUI>().text = word.word;
        rightAnswer.onClick.RemoveAllListeners();
        rightAnswer.onClick.AddListener(() => NextStage(word.word, word));

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
}
