using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewController : MonoBehaviour
{
    [SerializeField] private GameObject streamingPanel;
    [SerializeField] private GameObject calibrationPanel;
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private Button streamButton;
    [SerializeField] private Button calibrationButton;
    [SerializeField] private List<Button> backButtons;

    private void Awake()
    {
        streamButton.onClick.AddListener(StreamButtonClicked);
        calibrationButton.onClick.AddListener(CalibrationButtonClicked);

        foreach (Button backButton in backButtons)
        {
            backButton.onClick.AddListener(BackToMenuClicked);
        }
    }

    void Start()
    {
        mainMenuPanel.SetActive(true);
        streamingPanel.SetActive(false);
        calibrationPanel.SetActive(false);
    }

    private void StreamButtonClicked()
    {
        mainMenuPanel.SetActive(false);
        streamingPanel.SetActive(true);
        calibrationPanel.SetActive(false);
        backButtons[0].gameObject.SetActive(true);
    }

    private void CalibrationButtonClicked()
    {
        mainMenuPanel.SetActive(false);
        streamingPanel.SetActive(false);
        calibrationPanel.SetActive(true);
        backButtons[1].gameObject.SetActive(true);
    }

    private void BackToMenuClicked()
    {
        mainMenuPanel.SetActive(true);
        streamingPanel.SetActive(false);
        calibrationPanel.SetActive(false);
        backButtons[0].gameObject.SetActive(false);
        backButtons[1].gameObject.SetActive(false);

    }
}
