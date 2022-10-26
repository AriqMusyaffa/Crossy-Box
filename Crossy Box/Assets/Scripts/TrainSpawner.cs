using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainSpawner : MonoBehaviour
{
    [SerializeField] GameObject trainPrefab;
    [SerializeField] TerrainBlock terrain;
    [SerializeField] float minSpawnDuration = 3;
    [SerializeField] float maxSpawnDuration = 5;
    [SerializeField] MeshRenderer warningLight1;
    [SerializeField] MeshRenderer warningLight2;
    [SerializeField] Material lightOn;
    [SerializeField] Material lightOff;
    bool isRight;
    float nextTimer = 3;
    bool isWarning;
    float currentTimer;
    float refreshTimer = 0;
    GameObject player;
    AudioSource audioSource;
    bool audioOK = true;

    void Start()
    {
        isRight = Random.value > 0.5f ? true : false;
        nextTimer = Random.Range(minSpawnDuration, maxSpawnDuration);
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if (isWarning)
        {
            if ((currentTimer <= 2f && currentTimer > 1.75f) || (currentTimer <= 1.5f && currentTimer > 1.25f) || (currentTimer <= 1f && currentTimer > 0.75f) || (currentTimer <= 0.5f && currentTimer > 0.25f) || currentTimer <= 0)
            {
                warningLight1.material = lightOn;
                warningLight2.material = lightOff;
            }
            else if ((currentTimer <= 1.75f && currentTimer > 1.5f) || (currentTimer <= 1.25f && currentTimer > 1f) || (currentTimer <= 0.75f && currentTimer > 0.5f) || (currentTimer <= 0.25f && currentTimer > 0f))
            {
                warningLight1.material = lightOff;
                warningLight2.material = lightOn;
            }

            if (currentTimer > 0)
            {
                currentTimer -= Time.deltaTime;

                if (currentTimer <= 0.4f && audioOK && transform.position.z <= (player.transform.position.z + 9))
                {
                    audioSource.Play();
                    audioOK = false;
                }
                return;
            }

            var spawnPos = this.transform.position +
                Vector3.right * (isRight ? -(terrain.Extent + 1) : terrain.Extent + 1);

            var go = Instantiate
            (
                original: trainPrefab,
                position: spawnPos,
                rotation: Quaternion.Euler(0, isRight ? 90 : -90, 0),
                parent: this.transform
            );

            go.transform.localPosition -= new Vector3(0, 0, 0.3f);

            var train = go.GetComponent<Train>();
            train.Setup(terrain.Extent);

            isWarning = false;
            audioOK = true;
        }
        else
        {
            if (nextTimer > 0)
            {
                if (refreshTimer > 0)
                {
                    warningLight1.material = lightOn;
                    warningLight2.material = lightOn;
                    refreshTimer -= Time.deltaTime;
                    return;
                }

                warningLight1.material = lightOff;
                warningLight2.material = lightOff;
                nextTimer -= Time.deltaTime;
                return;
            }

            isWarning = true;
            nextTimer = Random.Range(minSpawnDuration, maxSpawnDuration);
            currentTimer = 2;
            refreshTimer = 0.5f;
        }
    }
}
