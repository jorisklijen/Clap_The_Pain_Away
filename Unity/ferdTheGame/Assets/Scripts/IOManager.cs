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
    Thread portThread = new Thread(Read);

    static SerialPort port = new SerialPort("", 115200); //devicename, bautrate
    static string[] vec3;

    public static float distanceLeft;
    public static float distanceRight;
    public static float audioLevel;

    AudioClip microphoneInput;
    float sensitivity;
    bool microphoneInitialised;

    string readValue;

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

        //Debug.Log(portThread.ThreadState);
        port.RtsEnable = true;
        port.ReadTimeout = 700;
        port.Open();

        if (!portThread.IsAlive)
            portThread.Start();
    }

    private void LateUpdate()
    {
        if(microphoneInitialised)
        {
            int dec = 128;
            float[] waveData = new float[dec];
            int micPosition = Microphone.GetPosition(null) - (dec + 1);
            microphoneInput.GetData(waveData, micPosition);

            float levelMax = 0;
            for (int i = 0; i < dec; i++)
            {
                float wavePeak = waveData[i] * waveData[i];
                if(levelMax < wavePeak)
                {
                    levelMax = wavePeak;
                }
            }
            audioLevel = (float)Math.Round(Mathf.Sqrt(Mathf.Sqrt(levelMax)) * 100f, 2);
            //Debug.Log(audioLevel);
        }

        if(instance.readValue != "")
        {
            vec3 = readValue.Split(',');
            for (int i = 0; i < vec3.Length; i++)
            {
                if (vec3[i] == "")
                {
                    Debug.Log("Arduino broke!");
                    return;
                }
            }

            distanceLeft = float.Parse(vec3[0]) * -1;
            distanceRight = float.Parse(vec3[1]);
        }
    }

    private static void Read(object obj)
    {
        while(true)
        {
            try
            {
                instance.readValue = port.ReadLine();
                //Debug.Log(instance.readValue);
            }
            catch (Exception)
            {
                //Debug.LogError("Reading failed");
            }
        }
    }

    private void OnApplicationQuit()
    {
        port.Close();
    }
}