using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerSpawner : MonoBehaviour
{
    [SerializeField] GameObject flowerPrefab1;
    [SerializeField] GameObject flowerPrefab2;
    [SerializeField] GameObject flowerPrefab3;
    [SerializeField] GameObject flowerPrefab4;
    [SerializeField] GameObject flowerPrefab5;
    [SerializeField] GameObject flowerPrefab6;
    [SerializeField] TerrainBlock terrain;
    [SerializeField] int count = 1;
    GameManager GM;

    void Start()
    {
        GM = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

        if (GM.areaType != GameManager.AreaType.Dark && GM.areaType != GameManager.AreaType.Autumn)
        {
            if (Random.value > 0.6f)
            {
                List<Vector3> emptyPos = new List<Vector3>();

                for (int x = -terrain.Extent; x <= terrain.Extent; x++)
                {
                    if (transform.position.z == 0 && x == 0)
                    {
                        continue;
                    }

                    emptyPos.Add(transform.position + Vector3.right * x);
                }

                for (int i = 0; i < count; i++)
                {
                    var index = Random.Range(0, emptyPos.Count);
                    var spawnPos = emptyPos[index];

                    GameObject flowerPrefab = flowerPrefab1;
                    int randomGrass = Random.Range(1, 7);
                    switch (randomGrass)
                    {
                        case 1:
                            flowerPrefab = flowerPrefab1;
                            break;
                        case 2:
                            flowerPrefab = flowerPrefab2;
                            break;
                        case 3:
                            flowerPrefab = flowerPrefab3;
                            break;
                        case 4:
                            flowerPrefab = flowerPrefab4;
                            break;
                        case 5:
                            flowerPrefab = flowerPrefab5;
                            break;
                        case 6:
                            flowerPrefab = flowerPrefab6;
                            break;
                    }

                    Instantiate(flowerPrefab, spawnPos, Quaternion.identity, this.transform);

                    emptyPos.RemoveAt(index);

                    Instantiate(flowerPrefab, transform.position + Vector3.right * -(terrain.Extent + 1), Quaternion.identity, this.transform);
                    Instantiate(flowerPrefab, transform.position + Vector3.right * (terrain.Extent + 1), Quaternion.identity, this.transform);
                }
            }
        }
    }
}
