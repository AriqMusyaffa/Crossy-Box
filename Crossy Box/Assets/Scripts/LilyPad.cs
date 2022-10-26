using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilyPad : MonoBehaviour
{
    GameManager GM;
    [SerializeField] GameObject grassBlock, sandBlock, snowBlock, darkBlock;

    void Start()
    {
        GM = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

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
        }
    }
}
