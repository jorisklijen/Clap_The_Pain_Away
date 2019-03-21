using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO.Ports;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player player;
    SerialPort port = new SerialPort("COM10", 9600, Parity.None, 8, StopBits.One);
    public Balloon[] lives;
    [SerializeField] Transform lifeHolder;
    public enum GamePhase { DEFAULT, BOSS1, BOSS2 }
    public GamePhase gamePhase;

    float distance;
    public GameObject movePopup;
    public GameObject shootPopup;

    [Header("Camera and Game")]
    [SerializeField] float bossPhase1FOV = 9;
    [SerializeField] float bossPhase2FOV = 9;
    public bool gameReady;

    private Camera cam;
    float fov = 7;

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

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "GarbageScene") // Change for live version
        {
            player = GameObject.Find("Player").GetComponent<Player>();
            lives = lifeHolder.GetComponentsInChildren<Balloon>();
        }

        cam = Camera.main;
        fov = cam.orthographicSize;

        SceneManager.activeSceneChanged += (Scene arg0, Scene arg1) => {
            Debug.Log("Scene changed");
            cam = Camera.main;
            fov = cam.orthographicSize;
        };

        InvokeRepeating("UpdateGamePhase", 0, 0.05f);
    }


    void UpdateGamePhase()
    {
        switch(gamePhase)
        {
            case GamePhase.DEFAULT:
                {
                    cam.orthographicSize = Mathf.MoveTowards(cam.orthographicSize, fov, Time.deltaTime);
                    cam.transform.position = new Vector3(cam.transform.position.x, Mathf.MoveTowards(cam.transform.position.y, 1 ,Time.deltaTime), cam.transform.position.z);
                    break;
                }
            case GamePhase.BOSS1:
                {
                    cam.orthographicSize = Mathf.MoveTowards(cam.orthographicSize, bossPhase1FOV, Time.deltaTime);
                    cam.transform.position = new Vector3(cam.transform.position.x, Mathf.MoveTowards(cam.transform.position.y, 3, Time.deltaTime), cam.transform.position.z);
                    break;
                }
        }
    }

    private void OnGUI()
    {
        // There was a comment here, but I no longer know what it said /shrug
        GUI.color = Color.black;
        GUI.Label(new Rect(10, 10, 300, 50), "Right sensor distance: " + IOManager.distanceRight);
        GUI.Label(new Rect(10, 30, 300, 50), "Left sensor distance: " + IOManager.distanceLeft);
        GUI.Label(new Rect(10, 50, 300, 50), "Game Decibel level: " + IOManager.audioLevel);
        //GUI.Label(new Rect(10, 70, 300, 50), "Game Time: " + Time.time);
        GUI.Label(new Rect(10, 70, 1000, 50), "Receive value:" + IOManager.instance.readValue);
    }

    public static void EnterPhase(int phaseIndex)
    {
        instance.gamePhase = (GamePhase)phaseIndex;
    }
}