using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecordingController : MonoBehaviour
{
    public OSC osc;

    [SerializeField] private GameObject RecordingPanel;
    [SerializeField] private GameObject RecordingNameSubPanel;
    [SerializeField] private Button StreamingButton;
    [SerializeField] private Button RecordingButton;
    [SerializeField] private TMP_InputField RecordingInputField;
    [SerializeField] private Button OKButton;
    [SerializeField] private Button CancelButton;
    [SerializeField] private Button BackButton;

    private bool isStreaming = false;
    private bool isRecording = false;

    private void Awake()
    {
        StreamingButton.onClick.AddListener(StreamingOptionClicked);
        RecordingButton.onClick.AddListener(RecordOptionClicked);
        OKButton.onClick.AddListener(OKOptionClicked);
        CancelButton.onClick.AddListener(CancelOptionClicked);

        RecordingInputField.onValueChanged.AddListener(delegate { RecordingNameChanged(); });

        ToggleCanvasGroup(RecordingNameSubPanel.GetComponent<CanvasGroup>(), false);
    }

    public void StreamingOptionClicked()
    {
        OscMessage message = new OscMessage();

        if (isStreaming)
        {
            BackButton.interactable = true;
            message.address = "/stopPipeline";
            StreamingButton.GetComponentInChildren<TMP_Text>().text = "Start Streaming";
            isStreaming = false;
        }
        else
        {
            BackButton.interactable = false;
            message.address = "/startCapturePipeline";
            StreamingButton.GetComponentInChildren<TMP_Text>().text = "Stop Streaming";
            isStreaming = true;
        }

        osc.Send(message);
    }

    private void RecordOptionClicked()
    {
        if (isRecording)
        {
            StreamingButton.interactable = true;
            RecordingPanel.GetComponent<Image>().color = Color.white;

            RecordingButton.GetComponentInChildren<TMP_Text>().text = "Start Recording";

            OscMessage message = new OscMessage();

            message.address = "/endRecording";
            osc.Send(message);

            isRecording = false;
        }
        else
        {
            RecordingPanel.GetComponent<CanvasGroup>().interactable = false;

            ToggleCanvasGroup(RecordingNameSubPanel.GetComponent<CanvasGroup>(), true);

            OKButton.interactable = false;
            RecordingInputField.text = "";

            isRecording = true;
        }
    }

    private void OKOptionClicked()
    {
        StreamingButton.interactable = false;

        RecordingPanel.GetComponent<Image>().color = Color.red;

        RecordingButton.GetComponentInChildren<TMP_Text>().text = "Stop Recording";

        RecordingPanel.GetComponent<CanvasGroup>().interactable = true;

        ToggleCanvasGroup(RecordingNameSubPanel.GetComponent<CanvasGroup>(), false);

        isRecording = true;

        OscMessage message = new OscMessage();
        message.address = "/beginRecording";
        message.values.Add(RecordingInputField.text);
        osc.Send(message);
    }

    private void CancelOptionClicked()
    {
        RecordingPanel.GetComponent<CanvasGroup>().interactable = true;

        ToggleCanvasGroup(RecordingNameSubPanel.GetComponent<CanvasGroup>(), false);
    }

    private void RecordingNameChanged()
    {
        if (RecordingInputField.text != "")
        {
            OKButton.interactable = true;
        }
        else
        {
            OKButton.interactable = false;
        }
    }

    CanvasGroup ToggleCanvasGroup(CanvasGroup canvasGroup, bool isOn)
    {
        if (isOn)
        {
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
        else
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

        return canvasGroup;
    }
}
