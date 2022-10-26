using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] GameObject coinPrefab;
    [SerializeField] TerrainBlock terrain;
    [SerializeField] int count = 1;

    void Start()
    {
        if (Random.value > 0.5f)
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

                Instantiate(coinPrefab, spawnPos, Quaternion.identity, this.transform);

                emptyPos.RemoveAt(index);
            }
        }
    }
}
