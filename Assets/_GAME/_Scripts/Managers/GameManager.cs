using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerController playerController;
    public CameraFollow cameraController;
    public GameObject playerInventory, npcInventory;
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("Game Manager is null");
            return _instance;
        }
    }
    private void Awake()
    {
        Debug.Log("Framerate set");
        Application.targetFrameRate = 60;
        _instance = this;
    }
    private void Start()
    {

    }
    public void Reset()
    {

    }



}
