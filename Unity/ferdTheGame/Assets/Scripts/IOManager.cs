using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading;
using System.IO.Ports;
using System.IO;
using System.Management;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IOManager : MonoBehaviour
{
    public string portName;
    public static IOManager instance;

    static SerialPort port = new SerialPort("", 115200); //devicename, bautrate
    static string[] returnValues;

    public static float distanceLeft;
    public static float distanceRight;
    public static float audioLevel;
    [SerializeField] string imagePath;

    AudioClip microphoneInput;
    WebCamTexture camTexture;
    float sensitivity;
    bool microphoneInitialised;

    public string readValue;

    public List<LEDLight> LEDLights = new List<LEDLight>();

    private void Awake()
    {
        portName = portName.ToUpper();
        port.PortName = portName;

        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this);
    }

    private void Start()
    {
        if(Microphone.devices.Length>0)
        {
            Debug.Log("Microphone found");
            microphoneInput = Microphone.Start(Microphone.devices[0], true, 999, 44100);
            microphoneInitialised = true;
        }

        camTexture = new WebCamTexture();
        camTexture.Play();


        //Debug.Log(portThread.ThreadState);
        port.RtsEnable = true;
        port.ReadTimeout = 700;
        port.Open();

        Thread portThread = new Thread(Read);
        if (!portThread.IsAlive)
            portThread.Start();

        StartCoroutine(StartGame());

        Debug.Log(GetScreenBoundsInWorldSpace()[0] + " " + GetScreenBoundsInWorldSpace()[1]);
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < GameManager.instance.lives.Length; i++)
        {
            //Debug.Log("editing light status");
            EditLedStatus(i + 1, false);
            yield return new WaitForSeconds(0.2f);

        }

        yield return new WaitForSeconds(.5f);

        for (int i = 0; i < GameManager.instance.lives.Length; i++)
        {
            //Debug.Log("moving through the lights to enable them");
            EditLedStatus(i + 1, true);
            yield return new WaitForSeconds(0.4f);
        }

        GameManager.instance.gameReady = true;

        InvokeRepeating("LEDRunner", 0f, 0.1f);
        yield return null;
    }

    private void Update()
    {
        if(GameManager.instance.gameReady)
        {
            if (microphoneInitialised)
            {
                int dec = 128;
                float[] waveData = new float[dec];
                int micPosition = Microphone.GetPosition(null) - (dec + 1);
                microphoneInput.GetData(waveData, micPosition);

                float levelMax = 0;
                for (int i = 0; i < dec; i++)
                {
                    float wavePeak = waveData[i] * waveData[i];
                    if (levelMax < wavePeak)
                    {
                        levelMax = wavePeak;
                    }
                }
                audioLevel = (float)Math.Round(Mathf.Sqrt(Mathf.Sqrt(levelMax)) * 100f, 2);
                //Debug.Log(audioLevel);
            }

            if (instance.readValue != "")
            {
                returnValues = readValue.Split(',');
                for (int i = 0; i < returnValues.Length; i++)
                {
                    if (returnValues[i] == "")
                    {
                        Debug.Log("Arduino broke!");
                        return;
                    }
                }

                distanceLeft = float.Parse(returnValues[0]) * -1;
                distanceRight = float.Parse(returnValues[1]);
            }
        }
    }

    public static void EditLedStatus(int index, bool status)
    {
        if(status)
            port.Write(index + ", enable");
        if(!status)
            port.Write(index + ", disable");
    }

    public IEnumerator TakePhoto()
    {

        yield return new WaitForEndOfFrame();

        Texture2D photo = new Texture2D(camTexture.width, camTexture.height);
        photo.SetPixels(camTexture.GetPixels());
        photo.Apply();

        byte[] bytes = photo.EncodeToPNG();
        
        File.WriteAllBytes(imagePath + '/' + DateTime.Now.ToString("HH-mm-ss-fff tt") + ".png", bytes);
    }

    private static void Read(object obj)
    {
        while(true)
        {
            try
            {
                instance.readValue = port.ReadLine();
            }
            catch (Exception e)
            {
                //Debug.LogError("Reading failed");
            }
        }
    }

    private void LEDRunner()
    {
        instance.LEDLights.Clear();
        for (int j = 2; j < returnValues.Length; j++)
        {
            string[] ledValues = returnValues[j].Split('/');

            LEDLight light = new LEDLight();
            light.r = int.Parse(ledValues[0]);
            light.g = int.Parse(ledValues[1]);
            light.b = int.Parse(ledValues[2]);

            instance.LEDLights.Add(light);
            //Debug.Log("Updating Light values!");
        }
    }

    public static int[] GetScreenBoundsInWorldSpace()
    {
        int[] val = { 0, 0 };

        int verticalExtent = (int)Camera.main.orthographicSize;
        int horizontalExtent = verticalExtent * Screen.width / Screen.height;

        val[0] = horizontalExtent;
        val[1] = verticalExtent;

        return val;
    }

    private void OnApplicationQuit()
    {
        port.Close();
    }
}

[Serializable]
public class LEDLight
{
    public int r;
    public int g;
    public int b;
}