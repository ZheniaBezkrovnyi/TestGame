using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public GameObject panel;
    private Color colorPanel;
    private void Start()
    {
        colorPanel = panel.GetComponent<Image>().color;
    }
    public void Pause()
    {
        panel.GetComponent<Image>().color = colorPanel;
        Time.timeScale = 0f;
        panel.GetComponent<Image>().color += new Color32(0, 0, 0, 100);

    }
    public void Continue()
    {
        panel.GetComponent<Image>().color = colorPanel;
        Time.timeScale = 1f;
    }
}
