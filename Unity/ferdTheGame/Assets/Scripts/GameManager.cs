using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static Player player;
    SerialPort port = new SerialPort("COM10", 9600, Parity.None, 8, StopBits.One);


    float distance;
    public GameObject movePopup;
    public GameObject shootPopup;

    #region Singleton Pattern design
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this);

        DontDestroyOnLoad(gameObject);
    }
    #endregion

    private void OnGUI()
    {
        // There was a comment here, but I no longer know what it said /shrug
        GUI.color = Color.black;
        GUI.Label(new Rect(10, 10, 300, 50), "Right sensor distance: " + IOManager.distanceRight);
        GUI.Label(new Rect(10, 30, 300, 50), "Left sensor distance: " + IOManager.distanceLeft);
        GUI.Label(new Rect(10, 50, 300, 50), "Game Decibel level: " + IOManager.audioLevel);
        GUI.Label(new Rect(10, 70, 300, 50), "Game Time: " + Time.time);

    }
}