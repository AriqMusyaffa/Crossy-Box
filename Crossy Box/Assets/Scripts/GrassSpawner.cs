using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassSpawner : MonoBehaviour
{
    [SerializeField] GameObject grassPrefab1;
    [SerializeField] GameObject grassPrefab2;
    [SerializeField] GameObject grassPrefab3;
    [SerializeField] GameObject grassPrefab4;
    [SerializeField] TerrainBlock terrain;
    [SerializeField] int count = 5;

    void Start()
    {
        GameManager GM = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

        count = GM.areaType == GameManager.AreaType.Autumn ? (GM.extent * 2) : 5;

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

                GameObject grassPrefab = grassPrefab1;
                int randomGrass = Random.Range(1, 3);
                switch (randomGrass)
                {
                    case 1:
                        grassPrefab = GM.areaType == GameManager.AreaType.Autumn ? grassPrefab4 : grassPrefab1;
                        break;
                    case 2:
                        grassPrefab = GM.areaType == GameManager.AreaType.Autumn ? grassPrefab4 : grassPrefab2;
                        break;
                }

                Instantiate(grassPrefab, spawnPos, Quaternion.identity, this.transform);

                emptyPos.RemoveAt(index);

                Instantiate(grassPrefab, transform.position + Vector3.right * -(terrain.Extent + 1), Quaternion.identity, this.transform);
                Instantiate(grassPrefab, transform.position + Vector3.right * (terrain.Extent + 1), Quaternion.identity, this.transform);
            }
        }
    }
}
