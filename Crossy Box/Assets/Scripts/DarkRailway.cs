using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkRailway : MonoBehaviour
{
    [SerializeField] Material dark;

    private void OnEnable()
    {
        GameManager GM;
        GM = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

        if (GM.areaType == GameManager.AreaType.Dark)
        {
            foreach (Transform child in transform)
            {
                child.GetComponent<Renderer>().material = dark;
            }
        }
    }
}
