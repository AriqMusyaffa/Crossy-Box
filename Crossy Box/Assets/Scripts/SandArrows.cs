using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandArrows : MonoBehaviour
{
    GameManager GM;
    public bool setup;

    public void EnableArrows()
    {
        GameObject arrow = transform.GetChild(0).gameObject;
        arrow.SetActive(true);

        GM = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        if (transform.position.x == (GM.extent + 1) || transform.position.x == -(GM.extent + 1))
        {
            foreach (Transform child in arrow.transform)
            {
                child.GetComponent<Renderer>().material.color *= Color.gray;
            }
        }
    }
}
