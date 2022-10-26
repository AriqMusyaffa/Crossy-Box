using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkenBorderRepeat : MonoBehaviour
{
    GameManager GM;
    bool isBorder;

    private void OnEnable()
    {
        GM = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (!isBorder)
        {
            if (transform.position.x == (GM.extent + 1) || transform.position.x == -(GM.extent + 1))
            {
                foreach (Transform child in transform)
                {
                    child.GetComponent<Renderer>().material.color *= Color.gray;
                }

                isBorder = true;
            }
        }
    }
}
