using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderRecolor : MonoBehaviour
{
    public List<Color> orange = new List<Color>();
    public List<Color> pink = new List<Color>();
    public List<Color> blue = new List<Color>();
    private UnityEngine.UI.Image background;
    private UnityEngine.UI.Image fill;
    private UnityEngine.UI.Image handle;
    private List<Slider> allSliders = new List<Slider>();

    private void RecolorSliders(Skin skin, Slider slider)
    {
        background = slider.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>();
        fill = slider.fillRect.GetComponent<UnityEngine.UI.Image>();
        if (slider.handleRect != null)
        {
            handle = slider.handleRect.GetComponent<UnityEngine.UI.Image>();;
        }

        switch (skin)
        {
            case Skin.Orange:
                background.color = orange[0];
                fill.color = orange[1];
                if (handle != null)
                {
                    handle.color = orange[2];
                }
                break;
            case Skin.Pink:
                background.color = pink[0];
                fill.color = pink[1];
                if (handle != null)
                {
                    handle.color = pink[2];
                }
                break;
            case Skin.Blue:
                background.color = blue[0];
                fill.color = blue[1];
                if (handle != null)
                {
                    handle.color = blue[2];
                }
                break;
        }
    }
    public void RecolorAllSliders(Skin skin)
    {
        Canvas[] allCanvases = FindObjectsOfType<Canvas>(true);
        foreach (Canvas canvas in allCanvases)
        {
            Slider[] slider = canvas.GetComponentsInChildren<Slider>(true);
            allSliders.AddRange(slider);
        }
        foreach (Slider slider in allSliders)
        {
            RecolorSliders(skin, slider);
        }
    }
}
