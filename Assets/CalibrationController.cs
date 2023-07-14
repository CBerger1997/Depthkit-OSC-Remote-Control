using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CalibrationController : MonoBehaviour
{
    public OSC osc;

    [SerializeField] private Button CalibrationButton;
    [SerializeField] private Button FloorSamplingButton;
    [SerializeField] private Button SamplingButton;
    [SerializeField] private Button BackButton;
    [SerializeField] private TMP_Text FloorSamplingText;
    [SerializeField] private TMP_Text SamplingText;
    [SerializeField] private TMP_Text SamplingCountText;
    [SerializeField] private GameObject MainPanel;

    private bool isCalibrating = false;
    private int sampleCount = 0;

    private void Awake()
    {
        CalibrationButton.onClick.AddListener(OnCalibrationClicked);
        FloorSamplingButton.onClick.AddListener(OnFloorCalibrationSelected);
        SamplingButton.onClick.AddListener(OnSampleCalibrationSelected);
    }

    private void OnCalibrationClicked()
    {
        OscMessage message = new OscMessage();

        if (isCalibrating)
        {
            BackButton.interactable = true;
            message.address = "/stopPipeline";
            CalibrationButton.GetComponentInChildren<TMP_Text>().text = "Start Calibration";
            isCalibrating = false;
        }
        else
        {
            BackButton.interactable = false;
            message.address = "/startCalibrationPipeline";
            CalibrationButton.GetComponentInChildren<TMP_Text>().text = "Stop Calibration";
            isCalibrating = true;
        }

        osc.Send(message);
    }

    private void OnFloorCalibrationSelected()
    {
        StartCoroutine(FloorCalibration());
    }

    IEnumerator FloorCalibration()
    {
        GetComponent<CanvasGroup>().interactable = false;
        MainPanel.GetComponent<Image>().color = Color.red;

        OscMessage message = new OscMessage();
        message.address = "/setFloorSamplingState";
        message.values.Add(1);
        osc.Send(message);

        FloorSamplingText.text = "Sampling.";

        yield return new WaitForSeconds(1);

        FloorSamplingText.text = "Sampling..";

        yield return new WaitForSeconds(1);

        FloorSamplingText.text = "Sampling...";

        yield return new WaitForSeconds(1);

        FloorSamplingText.text = "Done";

        message = new OscMessage();
        message.address = "/setFloorSamplingState";
        message.values.Add(0);
        osc.Send(message);
        GetComponent<CanvasGroup>().interactable = true;
        MainPanel.GetComponent<Image>().color = Color.white;
    }

    private void OnSampleCalibrationSelected()
    {
        StartCoroutine(Sampling());
    }

    IEnumerator Sampling()
    {
        GetComponent<CanvasGroup>().interactable = false;
        MainPanel.GetComponent<Image>().color = Color.red;
        OscMessage message = new OscMessage();
        message.address = "/setExtrinsicSamplingState";
        message.values.Add(1);
        osc.Send(message);

        SamplingText.text = "Sampling.";

        yield return new WaitForSeconds(1);

        SamplingText.text = "Sampling..";

        yield return new WaitForSeconds(1);

        SamplingText.text = "Sampling...";

        yield return new WaitForSeconds(1);

        SamplingText.text = "Done";

        message = new OscMessage();
        message.address = "/setExtrinsicSamplingState";
        message.values.Add(0);
        osc.Send(message);

        sampleCount++;
        SamplingCountText.text = "Sample Count: " + sampleCount.ToString();
        GetComponent<CanvasGroup>().interactable = true;
        MainPanel.GetComponent<Image>().color = Color.white;

    }
}
