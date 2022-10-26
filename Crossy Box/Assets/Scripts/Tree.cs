using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    GameManager GM;
    [SerializeField] GameObject grassTree, sandTree, snowTree, darkTree, autumnTree, leaf;
    [SerializeField] Material winterTree;
    GameObject bestSign;

    // Static akan membuat variable ini shared pada semua Tree
    public static List<Vector3> AllPositions = new List<Vector3>();

    void Start()
    {
        GM = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        bestSign = GameObject.FindWithTag("BestSign");

        switch (GM.areaType)
        {
            case GameManager.AreaType.Grass:
                grassTree.SetActive(true);
                break;
            case GameManager.AreaType.Sand:
                grassTree.SetActive(false);
                sandTree.SetActive(true);
                break;
            case GameManager.AreaType.Snow:
                grassTree.SetActive(false);
                snowTree.SetActive(true);
                leaf.GetComponent<Renderer>().material = winterTree;
                break;
            case GameManager.AreaType.Dark:
                grassTree.SetActive(false);
                darkTree.SetActive(true);
                break;
            case GameManager.AreaType.Autumn:
                grassTree.SetActive(false);
                autumnTree.SetActive(true);
                break;
        }
    }

    private void OnEnable()
    {
        AllPositions.Add(this.transform.position);
    }

    private void OnDisable()
    {
        AllPositions.Remove(this.transform.position);
    }

    void Update()
    {
        if (Rock.AllPositions.Contains(transform.position))
        {
            Destroy(gameObject);
        }

        if (transform.position == bestSign.transform.position)
        {
            AllPositions.Remove(this.transform.position);
            Destroy(this.gameObject);
        }
    }
}
