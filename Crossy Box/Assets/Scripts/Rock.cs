using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    GameManager GM;
    [SerializeField] GameObject snow;
    [SerializeField] Material darkRock;
    GameObject bestSign;

    // Static akan membuat variable ini shared pada semua Rock
    public static List<Vector3> AllPositions = new List<Vector3>();

    void Start()
    {
        GM = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        bestSign = GameObject.FindWithTag("BestSign");

        if (GM.areaType == GameManager.AreaType.Snow)
        {
            snow.SetActive(true);
        }
        if (GM.areaType == GameManager.AreaType.Dark)
        {
            Transform mainChild = transform.Find("Main"); ;
            foreach (Transform child in mainChild.transform)
            {
                if (child.gameObject.activeInHierarchy)
                {
                    child.GetComponent<Renderer>().material = darkRock;
                }
            }
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
        if (transform.position == bestSign.transform.position)
        {
            AllPositions.Remove(this.transform.position);
            Destroy(this.gameObject);
        }
    }
}
