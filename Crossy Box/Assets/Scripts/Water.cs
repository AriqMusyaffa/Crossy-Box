using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : TerrainBlock
{
    public void AreaAdapt()
    {
        GameManager GM;
        GM = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

        foreach (Transform child in transform)
        {
            if (child.CompareTag("MainBlock"))
            {
                GameObject waterBlock = child.Find("Water").gameObject;
                GameObject quicksandBlock = child.Find("Quicksand").gameObject;
                GameObject lavaBlock = child.Find("Lava").gameObject;

                if (GM.areaType == GameManager.AreaType.Sand)
                {
                    waterBlock.SetActive(false);
                    quicksandBlock.SetActive(true);
                }
                else if (GM.areaType == GameManager.AreaType.Dark)
                {
                    waterBlock.SetActive(false);
                    lavaBlock.SetActive(true);
                }
            }
        }
    }
}
