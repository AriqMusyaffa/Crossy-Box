using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogSpawner : MonoBehaviour
{
    [SerializeField] GameObject logPrefab_S, logPrefab_M, logPrefab_L;
    [SerializeField] TerrainBlock terrain;
    [SerializeField] Water water;
    [SerializeField] float minSpawnDuration = 2;
    [SerializeField] float maxSpawnDuration = 4;
    public bool isRight;
    float timer = 3;
    public List<int> safeXPos = new List<int>();
    char whichLog;
    GameManager GM;
    public bool isSand;

    void Start()
    {
        GM = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        if (GM.areaType == GameManager.AreaType.Sand)
        {
            isSand = true;
        }

        timer = Random.Range(minSpawnDuration, maxSpawnDuration);
        RandomLog();
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            return;
        }

        timer = Random.Range(minSpawnDuration, maxSpawnDuration);

        var spawnPos = this.transform.position +
            Vector3.right * (isRight ? -(terrain.Extent + 1) : terrain.Extent + 1);

        GameObject chosenLog = logPrefab_S;

        switch (whichLog)
        {
            case 'S':
                chosenLog = logPrefab_S;
                break;
            case 'M':
                chosenLog = logPrefab_M;
                break;
            case 'L':
                chosenLog = logPrefab_L;
                break;
        }

        var go = Instantiate
        (
            original: chosenLog,
            position: spawnPos,
            rotation: Quaternion.Euler(0, isRight ? 90 : -90, 0),
            parent: this.transform
        );

        var log = go.GetComponent<Log>();
        log.Setup(terrain.Extent);
        log.isRight = isRight;

        RandomLog();

        /*if (isSand)
        {
            foreach (Transform child in transform)
            {
                if (child.GetComponent<SandArrows>() != null)
                {
                    SandArrows sandArrows = child.GetComponent<SandArrows>();
                    sandArrows.EnableArrows();

                    if (!sandArrows.setup)
                    {
                        if (isRight)
                        {
                            child.Rotate(0, 180, 0);
                        }

                        sandArrows.setup = true;
                    }
                }
            }
        }*/
    }

    private void RandomLog()
    {
        float randomLog = Random.value;

        if (randomLog <= 0.33f)
        {
            whichLog = 'S';
        }
        else if (randomLog <= 0.66f)
        {
            whichLog = 'M';
        }
        else
        {
            whichLog = 'L';
        }
    }
}
