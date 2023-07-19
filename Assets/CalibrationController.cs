using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using extOSC;

public class CalibrationController : MonoBehaviour
{
    public OSCTransmitter transmitter;

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
        //EXAMPLE OF MESSAGE
        //https://github.com/Iam1337/extOSC
        //        // Creating a transmitter.
        //var transmitter = gameObject.AddComponent<OSCTransmitter>();

        //        // Set remote host address.
        //        transmitter.RemoteHost = "127.0.0.1";

        //        // Set remote port;
        //        transmitter.RemotePort = 7001;
        //        Or you can simple create OSCTransmitter component in Unity editor, or use Create/ extOSC / OSC Manager in Hierarchy window.

        //Send OSCMessage
        //// Create message
        //        var message = new OSCMessage("/message/address");

        //        // Populate values.
        //        message.AddValue(OSCValue.String("Hello, world!"));
        //        message.AddValue(OSCValue.Float(1337f));

        //        // Send message
        //        transmitter.Send(message);


        OSCMessage message = new OSCMessage("");

        if (isCalibrating)
        {
            BackButton.interactable = true;
            message.Address = "/stopPipeline";
            CalibrationButton.GetComponentInChildren<TMP_Text>().text = "Start Calibration";
            isCalibrating = false;
        }
        else
        {
            BackButton.interactable = false;
            message.Address = "/startCalibrationPipeline";
            CalibrationButton.GetComponentInChildren<TMP_Text>().text = "Stop Calibration";
            isCalibrating = true;
        }

        transmitter.Send(message);
    }

    private void OnFloorCalibrationSelected()
    {
        StartCoroutine(FloorCalibration());
    }

    IEnumerator FloorCalibration()
    {
        GetComponent<CanvasGroup>().interactable = false;
        MainPanel.GetComponent<Image>().color = Color.red;

        OSCMessage message = new OSCMessage("");
        message.Address = "/setFloorSamplingState";
        message.AddValue(OSCValue.Int(1));
        transmitter.Send(message);

        FloorSamplingText.text = "Sampling.";

        yield return new WaitForSeconds(1);

        FloorSamplingText.text = "Sampling..";

        yield return new WaitForSeconds(1);

        FloorSamplingText.text = "Sampling...";

        yield return new WaitForSeconds(1);

        FloorSamplingText.text = "Done";

        message = new OSCMessage("");
        message.Address = "/setFloorSamplingState";
        message.AddValue(OSCValue.Int(0));
        transmitter.Send(message);
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

        OSCMessage message = new OSCMessage("");
        message.Address = "/setExtrinsicSamplingState";
        message.AddValue(OSCValue.Int(1));
        transmitter.Send(message);

        SamplingText.text = "Sampling.";

        yield return new WaitForSeconds(1);

        SamplingText.text = "Sampling..";

        yield return new WaitForSeconds(1);

        SamplingText.text = "Sampling...";

        yield return new WaitForSeconds(1);

        SamplingText.text = "Done";

        message = new OSCMessage("");
        message.Address = "/setExtrinsicSamplingState";
        message.AddValue(OSCValue.Int(0));
        transmitter.Send(message);

        sampleCount++;
        SamplingCountText.text = "Sample Count: " + sampleCount.ToString();
        GetComponent<CanvasGroup>().interactable = true;
        MainPanel.GetComponent<Image>().color = Color.white;

    }
}
