using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : TerrainBlock
{
    void Start()
    {
        GameManager GM = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

        BoxCollider box = GetComponent<BoxCollider>();
        box.size = new Vector3((GM.extent * 2) + 1, box.size.y, box.size.z);
    }

    void Update()
    {
        
    }
}
