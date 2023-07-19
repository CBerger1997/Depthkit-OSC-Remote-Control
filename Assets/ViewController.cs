using System.Collections.Generic;
using extOSC;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ViewController : MonoBehaviour
{
    [SerializeField] private GameObject streamingPanel;
    [SerializeField] private GameObject calibrationPanel;
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private Button streamButton;
    [SerializeField] private Button calibrationButton;
    [SerializeField] private Button connectButton;
    [SerializeField] private List<Button> backButtons;
    [SerializeField] private TMP_InputField OutIP;
    [SerializeField] private TMP_InputField OutPort;

    public OSCTransmitter transmitter;


    private void Awake()
    {
        OutIP.text = "127.0.0.1";
        OutPort.text = "57734";

        streamButton.onClick.AddListener(StreamButtonClicked);
        calibrationButton.onClick.AddListener(CalibrationButtonClicked);
        connectButton.onClick.AddListener(ConnectClicked);

        foreach (Button backButton in backButtons)
        {
            backButton.onClick.AddListener(BackToMenuClicked);
        }

        Debug.Log("STARTING CONNECTION");

        transmitter.RemoteHost = OutIP.text;
        transmitter.RemotePort = int.Parse(OutPort.text);

        transmitter.Connect();

        Debug.Log("IS STARTED: " + transmitter.IsStarted);

        Debug.Log("HOST: " + transmitter.RemoteHost);
        Debug.Log("PORT: " + transmitter.RemotePort);

        Debug.Log("CONNECTION: " + transmitter.IsStarted);
    }

    void Start()
    {
        mainMenuPanel.SetActive(true);
        streamingPanel.SetActive(false);
        calibrationPanel.SetActive(false);
    }

    private void BrowseConnections()
    {

    }

    private void ConnectClicked()
    {
        transmitter.RemoteHost = OutIP.text;
        transmitter.RemotePort = int.Parse(OutPort.text);

        transmitter.Connect();
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
