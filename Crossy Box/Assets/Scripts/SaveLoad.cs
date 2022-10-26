using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    public static SaveLoad instance;

    public int BestScore;
    public int AllCoins;

    public bool GameStarted = false;
    public Color playerColor;
    public bool playerIsColor;
    public bool playerIsRainbow;
    public string AreaType = "Grass";

    public AudioSource EverAudio;
    public AudioClip menuMusic, gameMusicA, gameMusicB;
    public bool startClick = false;

    public void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        EverAudio = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
        LoadData();
    }

    void Update()
    {
        SaveData();

        if(Input.GetKeyDown(KeyCode.Delete) && !GameStarted)
        {
            EraseData();
        }
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("BestScore", BestScore);
        PlayerPrefs.SetInt("AllCoins", AllCoins);
    }

    public void LoadData()
    {
        BestScore = PlayerPrefs.GetInt("BestScore", 0);
        AllCoins = PlayerPrefs.GetInt("AllCoins", 0);
    }

    public void EraseData()
    {
        GameManager GM = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

        BestScore = 0;
        AllCoins = 0;
        SaveData();
        startClick = true;
        GM.RestartGame();
    }
}
