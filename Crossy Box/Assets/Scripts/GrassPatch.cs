using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassPatch : MonoBehaviour
{
    GameManager GM;
    [SerializeField] Material matGrass, matSand, matSnow, matDark, matAutumn;

    void Start()
    {
        GM = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

        if (!CompareTag("Flower"))
        {
            switch (GM.areaType)
            {
                case GameManager.AreaType.Grass:
                    GetComponent<Renderer>().material = matGrass;
                    break;
                case GameManager.AreaType.Sand:
                    GetComponent<Renderer>().material = matSand;
                    break;
                case GameManager.AreaType.Snow:
                    GetComponent<Renderer>().material = matSnow;
                    break;
                case GameManager.AreaType.Dark:
                    GetComponent<Renderer>().material = matDark;
                    break;
                case GameManager.AreaType.Autumn:
                    GetComponent<Renderer>().material = matAutumn;
                    break;
            }
        }
    }

    void Update()
    {
        if (Tree.AllPositions.Contains(transform.position) || Rock.AllPositions.Contains(transform.position) || Coin.AllPositions.Contains(transform.position))
        {
            Destroy(gameObject);
        }
    }
}
