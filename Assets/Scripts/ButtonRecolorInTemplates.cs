using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonRecolorInTemplates : MonoBehaviour
{
    public Color orange;
    public Color pink;
    public Color blue;
    void Start()
    {
        switch (PlayerPrefs.GetInt("skin"))
        {
            case 0:
                GetComponent<Image>().color = orange;
                break;
            case 1:
                GetComponent<Image>().color = pink;
                break;
            case 2:
                GetComponent<Image>().color = blue;
                break;
        }
    }
}
