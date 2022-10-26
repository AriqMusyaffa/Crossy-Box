using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    SaveLoad SaveLoad;
    public bool startGame = false;

    public enum AreaType
    {
        Grass,
        Sand, 
        Snow, 
        Dark,
        Autumn,
    }

    public AreaType areaType;

    [SerializeField] Player player;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] GameObject canvasOverlay;
    [SerializeField] GameObject canvasCamera;
    [SerializeField] GameObject playerObject;
    [SerializeField] GameObject playerShadow;
    [SerializeField] GameObject[] playerCustomColor;
    [SerializeField] RainbowColor[] playerRainbowColor;

    [SerializeField] GameObject colorPickerObject;
    [SerializeField] GameObject ColorPreviewObject;

    [SerializeField] GameObject grass;
    [SerializeField] GameObject road;
    [SerializeField] GameObject water;
    [SerializeField] GameObject track;

    public int extent = 5;
    [SerializeField] int frontDistance = 10;
    [SerializeField] int backDistance = -5;
    [SerializeField] int maxSameTerrainRepeat = 3;
    public int coins = 0;
    private int mstrMulti = 10;
    [SerializeField] GameObject darkFilter;

    Dictionary<int, TerrainBlock> map = new Dictionary<int, TerrainBlock>(50);
    public Dictionary<int, GameObject> TerrainType = new Dictionary<int, GameObject>();

    TMP_Text gameOverText;
    [SerializeField] TMP_Text coinText;
    public bool previousWasWater = false;
    public bool startTerrain = false;
    public bool logsGoingRight;

    [SerializeField] GameObject r_restart, esc_mainmenu;
    int currentBestScore;

    AudioSource audioSource;
    [SerializeField] AudioClip click, step, coin, carhorn, squeak, splash, sand, train, eagle;
    [SerializeField] Camera camera;
    Color waterColor = new Color(28f / 255f, 163f / 255f, 236f / 255f);
    Color sandColor = new Color(210f / 255f, 175f / 255f, 80f / 255f);
    Color lavaColor = new Color(200f / 255f, 0f / 255f, 50f / 255f);
    bool isQuit = false;
    float quitF = 0f;

    void Start()
    {
        SaveLoad = GameObject.FindWithTag("SaveLoad").GetComponent<SaveLoad>();

        if (SaveLoad.GameStarted)
        {
            StartGame();
        }
        else
        {
            if (SaveLoad.EverAudio.clip != SaveLoad.menuMusic)
            {
                SaveLoad.EverAudio.clip = SaveLoad.menuMusic;
                SaveLoad.EverAudio.Play();
            }
        }

        switch (SaveLoad.AreaType)
        {
            case "Grass":
                areaType = AreaType.Grass;
                break;
            case "Sand":
                areaType = AreaType.Sand;
                break;
            case "Snow":
                areaType = AreaType.Snow;
                break;
            case "Dark":
                areaType = AreaType.Dark;
                break;
            case "Autumn":
                areaType = AreaType.Autumn;
                break;
        }

        currentBestScore = SaveLoad.BestScore;

        audioSource = GetComponent<AudioSource>();

        if (SaveLoad.startClick)
        {
            SFX_Click();
            SaveLoad.startClick = false;
        }

        // Setup GameOver Panel
        gameOverPanel.SetActive(false);
        gameOverText = gameOverPanel.GetComponentInChildren<TMP_Text>();
    }

    private int playerLastMaxTravel;

    void BeginSetup()
    {
        mainMenuPanel.SetActive(false);

        logsGoingRight = Random.value > 0.5f ? true : false;

        // Belakang
        for (int z = backDistance; z <= 0; z++)
        {
            CreateTerrain(grass, z);
        }

        startTerrain = true;

        // Depan
        for (int z = 1; z <= frontDistance; z++)
        {
            var prefab = GetNextRandomTerrainPrefab(z);

            CreateTerrain(prefab, z);
        }

        player.SetUp(backDistance, extent);

        if (areaType == AreaType.Dark)
        {
            darkFilter.SetActive(true);
        }
    }

    void Update()
    {
        if (startGame)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                RestartGame();
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                GoToMenu();
            }
        }

        //if (SaveLoad.BestScore < 20)
        //{
            if (player.MaxTravel >= 20 && SaveLoad.EverAudio.clip != SaveLoad.gameMusicB)
            {
                SaveLoad.EverAudio.clip = SaveLoad.gameMusicB;
                SaveLoad.EverAudio.Play();
            }
        //}
        //else
        //{
        //    if (player.MaxTravel > currentBestScore && SaveLoad.EverAudio.clip != SaveLoad.gameMusicB)
        //    {
        //        SaveLoad.EverAudio.clip = SaveLoad.gameMusicB;
        //        SaveLoad.EverAudio.Play();
        //    }
        //}

        if (player.IsDie && gameOverPanel.activeInHierarchy == false)
        {
            r_restart.SetActive(false);
            esc_mainmenu.SetActive(false);
        }
        else if (player.IsDie && gameOverPanel.activeInHierarchy == true)
        {
            r_restart.SetActive(true);
            esc_mainmenu.SetActive(true);
        }

        if (startTerrain)
        {
            // Check Player apakah masih hidup atau tidak?
            if (player.IsDie && gameOverPanel.activeInHierarchy == false)
            {
                StartCoroutine(ShowGameOverPanel());
            }

            // Infinite Terrain system
            if (player.MaxTravel == playerLastMaxTravel)
            {
                return;
            }
            playerLastMaxTravel = player.MaxTravel;

            // Instantiate terrain di depan
            var randTbPrefab = GetNextRandomTerrainPrefab(player.MaxTravel + frontDistance);
            CreateTerrain(randTbPrefab, player.MaxTravel + frontDistance);

            // Hapus terrain di belakang
            var lastTB = map[(player.MaxTravel - 1) + backDistance];
            map.Remove((player.MaxTravel - 1) + backDistance);          // Hapus dari daftar
            Destroy(lastTB.gameObject);                                 // Hilangkan dari scene
            player.SetUp(player.MaxTravel + backDistance, extent);      // SetUp lagi agar Player tidak bisa bergerak ke belakang

            // Check coins
            coinText.text = "COINS : " + coins;

            // Increase max same terrain repeat
            if (player.MaxTravel >= mstrMulti)
            {
                maxSameTerrainRepeat++;
                mstrMulti += 15;
            }
        }

        if (player.MaxTravel > SaveLoad.BestScore)
        {
            SaveLoad.BestScore = player.MaxTravel;
        }

        if (isQuit)
        {
            if (quitF < 0.5f)
            {
                quitF += Time.deltaTime;
            }
            else
            {
                Application.Quit();
            }
        }
    }

    IEnumerator ShowGameOverPanel()
    {
        yield return new WaitForSeconds(3);

        //gameOverText.text = "Your Score : " + player.MaxTravel;
        gameOverPanel.SetActive(true);
    }

    private void CreateTerrain(GameObject prefab, int zPos)
    {
        // Instantiate block
        var go = Instantiate(prefab, new Vector3(0, 0, zPos), Quaternion.identity);
        var tb = go.GetComponent<TerrainBlock>();
        tb.Build(extent);

        map.Add(zPos, tb);
        TerrainType.Add(zPos, go);

        // Check apakah terrain Water boleh mengandung Lily Pad
        if (startTerrain)
        {
            if (go.GetComponent<Grass>() != null || go.GetComponent<Road>() != null)
            {
                go.GetComponent<CoinSpawner>().enabled = true;
            }

            if (go.GetComponent<Water>() != null)
            {
                //Debug.Log("Row " + zPos + " : Has water component");
                if (previousWasWater)
                {
                    if (Random.value <= 0.75f)
                    {
                        if (TerrainType[zPos - 1].GetComponent<LilyPadSpawner>().enabled == false)
                        {
                            go.GetComponent<LilyPadSpawner>().enabled = true;
                        }
                        else
                        {
                            go.GetComponent<LogSpawner>().enabled = true;
                            go.GetComponent<LogSpawner>().isRight = logsGoingRight;
                        }
                    }
                    else
                    {
                        go.GetComponent<LogSpawner>().enabled = true;
                        go.GetComponent<LogSpawner>().isRight = logsGoingRight;
                    }
                }
                else
                {
                    go.GetComponent<LogSpawner>().enabled = true;
                    go.GetComponent<LogSpawner>().isRight = logsGoingRight;
                }
                previousWasWater = true;
            }

            logsGoingRight = !logsGoingRight;

            if (go.GetComponent<Water>() == null)
            {
                //Debug.Log("Row " + zPos + " : Doesn't have water component");
                previousWasWater = false;

                if (TerrainType[zPos - 1].GetComponent<Water>() != null)
                {
                    var waterParent = TerrainType[zPos - 1];

                    foreach (Transform child in waterParent.transform)
                    {
                        if (child.tag == "LilyPad")
                        {
                            Destroy(child.gameObject);
                        }
                    }

                    TerrainType[zPos - 1].GetComponent<LilyPadSpawner>().enabled = false;
                    TerrainType[zPos - 1].GetComponent<LogSpawner>().enabled = true;
                    TerrainType[zPos - 1].GetComponent<LogSpawner>().isRight = logsGoingRight;
                }
            }
        }
    }

    private GameObject GetNextRandomTerrainPrefab(int nextPos)
    {
        bool isUniform = true;
        var tbRef = map[nextPos - 1];

        for (int distance = 2; distance <= maxSameTerrainRepeat; distance++)
        {
            if (map[nextPos - distance].GetType() != tbRef.GetType())
            {
                isUniform = false;
                break;
            }
        }

        if (isUniform)
        {
            if (tbRef is Grass)
            {
                if (Random.Range(1, 3) == 1)
                {
                    return road;
                }
                else
                {
                    return water;
                }
            }
            else if (tbRef is Road)
            {
                if (Random.Range(1, 3) == 1)
                {
                    return grass;
                }
                else
                {
                    return water;
                }
            }
            else if (tbRef is Water)
            {
                if (Random.Range(1, 3) == 1)
                {
                    return grass;
                }
                else
                {
                    return road;
                }
            }
            else if (tbRef is Track)
            {
                if (Random.Range(1, 3) == 1)
                {
                    return road;
                }
                else
                {
                    return grass;
                }
            }
        }

        // Menentukan terrain block dengan probabilitas
        //return Random.value > 0.5f ? road : grass;
        float random = Random.value;
        if (random <= 0.35f)
        {
            return grass;
        }
        else if (random <= 0.6f)
        {
            return road;
        }
        else if (random <= 0.85f)
        {
            return water;
        }
        else
        {
            return track;
        }
    }

    public void SFX_Click()
    {
        audioSource.clip = click;
        audioSource.Play();
    }

    public void SFX_Step()
    {
        audioSource.clip = step;
        audioSource.Play();
    }

    public void SFX_Coin()
    {
        audioSource.clip = coin;
        audioSource.Play();
    }

    public void SFX_CarHorn()
    {
        audioSource.clip = carhorn;
        audioSource.Play();
    }

    public void SFX_Squeak()
    {
        audioSource.clip = squeak;
        audioSource.Play();
    }

    public void SFX_Splash()
    {
        audioSource.clip = splash;
        audioSource.Play();
    }

    public void SFX_Sand()
    {
        audioSource.clip = sand;
        audioSource.Play();
    }

    public void SFX_Train()
    {
        audioSource.clip = train;
        audioSource.Play();
    }

    public void SFX_Eagle()
    {
        audioSource.clip = eagle;
        audioSource.Play();
    }

    public void StartButton()
    {
        SFX_Click();
        StartGame();
    }

    public void StartGame()
    {
        switch (SaveLoad.AreaType)
        {
            case "Grass":
                areaType = AreaType.Grass;
                break;
            case "Autumn":
                areaType = AreaType.Autumn;
                break;
            case "Snow":
                areaType = AreaType.Snow;
                break;
            case "Sand":
                areaType = AreaType.Sand;
                break;
            case "Dark":
                areaType = AreaType.Dark;
                break;
        }

        if (SaveLoad.playerIsColor)
        {
            foreach (GameObject go in playerCustomColor)
            {
                go.GetComponent<Renderer>().material.color = SaveLoad.playerColor;
            }
        }
        else if (SaveLoad.playerIsRainbow)
        {
            foreach (RainbowColor rc in playerRainbowColor)
            {
                rc.isRainbow = true;
            }
        }

        colorPickerObject.SetActive(false);
        SaveLoad.GameStarted = true;
        BeginSetup();

        switch (SaveLoad.AreaType)
        {
            case "Grass":
                camera.backgroundColor = waterColor;
                break;
            case "Autumn":
                camera.backgroundColor = waterColor;
                break;
            case "Snow":
                camera.backgroundColor = waterColor;
                break;
            case "Sand":
                camera.backgroundColor = sandColor;
                break;
            case "Dark":
                camera.backgroundColor = lavaColor;
                break;
        }

        playerObject.SetActive(true);
        playerShadow.SetActive(true);
        startGame = true;
        GetComponent<EagleSpawner>().enabled = true;

        if (SaveLoad.EverAudio.clip != SaveLoad.gameMusicA)
        {
            SaveLoad.EverAudio.clip = SaveLoad.gameMusicA;
            SaveLoad.EverAudio.Play();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMenu()
    {
        SaveLoad.GameStarted = false;
        RestartGame();
    }

    public void GoToSettings()
    {
        SFX_Click();
        canvasCamera.SetActive(true);
        ColorPreviewObject.SetActive(true);
        canvasOverlay.SetActive(false);
    }

    public void Back()
    {
        SFX_Click();
        canvasOverlay.SetActive(true);
        canvasCamera.SetActive(false);
        ColorPreviewObject.SetActive(false);
        colorPickerObject.SetActive(false);
    }

    public void QuitGame()
    {
        SFX_Click();
        isQuit = true;
    }
}
