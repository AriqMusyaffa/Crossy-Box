using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : TerrainBlock
{
    public void AreaAdapt()
    {
        GameManager GM;
        GM = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

        foreach (Transform child in transform)
        {
            if (child.CompareTag("MainBlock"))
            {
                GameObject grassBlock = child.Find("Grass").gameObject;
                GameObject sandBlock = child.Find("Sand").gameObject;
                GameObject snowBlock = child.Find("Snow").gameObject;
                GameObject darkBlock = child.Find("Dark").gameObject;
                GameObject autumnBlock = child.Find("Autumn").gameObject;

                switch (GM.areaType)
                {
                    case GameManager.AreaType.Grass:
                        grassBlock.SetActive(true);
                        break;
                    case GameManager.AreaType.Sand:
                        grassBlock.SetActive(false);
                        sandBlock.SetActive(true);
                        break;
                    case GameManager.AreaType.Snow:
                        grassBlock.SetActive(false);
                        snowBlock.SetActive(true);
                        break;
                    case GameManager.AreaType.Dark:
                        grassBlock.SetActive(false);
                        darkBlock.SetActive(true);
                        break;
                    case GameManager.AreaType.Autumn:
                        grassBlock.SetActive(false);
                        autumnBlock.SetActive(true);
                        break;
                }
            }
        }
    }
}
