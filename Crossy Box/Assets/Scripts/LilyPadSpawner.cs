using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilyPadSpawner : MonoBehaviour
{
    [SerializeField] GameObject lilypadPrefab;
    [SerializeField] TerrainBlock terrain;
    [SerializeField] Water water;
    [SerializeField] int count = 3;
    public List<int> safeXPos = new List<int>();

    void Start()
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

            Instantiate(lilypadPrefab, spawnPos, Quaternion.identity, this.transform);
            safeXPos.Add((int)spawnPos.x);

            emptyPos.RemoveAt(index);

            //Instantiate(lilypadPrefab, transform.position + Vector3.right * -(terrain.Extent + 1), Quaternion.identity, this.transform);
            //Instantiate(lilypadPrefab, transform.position + Vector3.right * (terrain.Extent + 1), Quaternion.identity, this.transform);
        }
    }
}
